using ProjectATI.Code.Enums;
using ProjectATI.Code.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;

namespace ProjectATI.Code.Statements
{
    public class AssignStatement : Node
    {
        public Variable left { get; set; }
        private Node right { get; set; }
        public string RPN { get; private set; }

        public AssignStatement(Variable left, Token right, int number)
        {
            this.left = left;
            this.number = number;
            this.right = ParseCalcToNode(right);
            PKB.Pkb.GetPkbInstance().SetModifies(this, this.left);
            PKB.Pkb.GetPkbInstance().AddStatement(this);
        }

        public Node ParseCalcToNode(Token calc)
        {
            Node root = new Node();
            bool changes = true;
            string ops = string.Join("", Helpers.operatorsAndPriority.Select(item => item.Key));
            List<Node> rpnNodes = new List<Node>();
            var rpn = ConvertToRPN(calc);
            this.RPN = rpn;
            if(rpn.IndexOfAny(ops.ToCharArray()) == -1)
            {
                bool variable = true;
                foreach(var c in rpn)
                {
                    if (char.IsDigit(c))
                        variable = false;
                }
                if (variable)
                {
                    Token t = new Token(TokenType.Variable, rpn);
                    Variable nodeToAdd = new Variable(t);
                    PKB.Pkb.GetPkbInstance().SetUses(this, nodeToAdd);
                    return nodeToAdd;
                }
                else
                {
                    PKB.Pkb.GetPkbInstance().SetNumericValue(Convert.ToInt32(rpn));
                    return new Node()
                    {
                        Name = rpn
                    };
                }
            }
            for(int i = 0; i < rpn.Length; i++)
            {
                var name = "";
                if (char.IsDigit(rpn[i]))
                {
                    name += rpn[i];
                    while (char.IsDigit(rpn[i + 1]))
                    {
                        name += rpn[i + 1];
                        i++;
                    }
                    Node nodeToAdd = new Node()
                    {
                        Name = name,
                        IsEvaluated = false,
                    };
                    PKB.Pkb.GetPkbInstance().SetNumericValue(Convert.ToInt32(name));
                    rpnNodes.Add(nodeToAdd);
                }
                else if (char.IsLetter(rpn[i]))
                {
                    name += rpn[i];
                    Variable nodeToAdd = new Variable(new Token(TokenType.Variable, name))
                    {
                        Name = name,
                        IsEvaluated = false,
                    };
                    rpnNodes.Add(nodeToAdd);
                    PKB.Pkb.GetPkbInstance().SetUses(this, nodeToAdd);

                }
                else
                {
                    name = rpn[i].ToString();
                    Node nodeToAdd = new Node()
                    {
                        Name = name,
                        IsEvaluated = false,
                    };
                    rpnNodes.Add(nodeToAdd);
                }  
            }
            SingleRPNIteration(ref rpnNodes, changes, ref root, true);
            while (changes)
            {
                changes = SingleRPNIteration(ref rpnNodes, changes, ref root, false);
            }
            return root;
        }

        public string ConvertToRPN(Token calc)
        {
            List<string> operators = new List<string>();
            List<string> signs = new List<string>();
            string s = calc.Value.Replace(" ", "");
            string result = "";
            for(int i = 0; i < s.Length; i++)
            {
                if (Helpers.operatorsAndPriority.ContainsKey(s[i].ToString()))
                {
                    if (s[i] == '(')
                    {
                        operators.Add(s[i].ToString());
                        continue;
                    }
                    else if (s[i] == ')')
                    {
                        while (operators[operators.Count - 1] != "(")
                        {
                            result += operators[operators.Count - 1];
                            operators.RemoveAt(operators.Count - 1);
                        }
                        operators.RemoveAt(operators.Count - 1);
                        continue;
                    }
                    if (operators.Count != 0)
                    {
                        var lastOperator = operators[operators.Count - 1].ToString();
                        var lastOperatorPriority = Helpers.operatorsAndPriority.Where(item => item.Key == lastOperator).FirstOrDefault().Value;
                        var newOperatorPriority = Helpers.operatorsAndPriority.Where(item => item.Key == s[i].ToString()).FirstOrDefault().Value;
                        while (operators.Count != 0 && lastOperatorPriority >= newOperatorPriority && lastOperatorPriority != 0)
                        {
                            result += operators[operators.Count - 1];
                            operators.RemoveAt(operators.Count - 1);
                            if (operators.Count != 0)
                            {
                                lastOperator = operators[operators.Count - 1].ToString();
                                lastOperatorPriority = Helpers.operatorsAndPriority.Where(item => item.Key == lastOperator).FirstOrDefault().Value;
                            }
                        }
                    }
                    operators.Add(s[i].ToString());
                }
                else
                    result += s[i];

            }
            for(int i = operators.Count - 1; i >= 0; i--)
            {
                result += operators[i];
            }
            return result;
        }

        private bool SingleRPNIteration(ref List<Node> rpnNodes, bool changes, ref Node root, bool isFirst = false)
        {
            int curridx = 0;
            List<Node> tmp = new List<Node>();
            for (int i = 2; i < rpnNodes.Count;)
            {
                string current = rpnNodes[i].Name;
                if ((isFirst ? Char.IsLetterOrDigit(rpnNodes[i - 2].Name[0]) : (Char.IsLetterOrDigit(rpnNodes[i - 2].Name[0]) || Helpers.operatorsAndPriority.Any(item => item.Key == current))) &&
                    (isFirst ? Char.IsLetterOrDigit(rpnNodes[i - 1].Name[0]) : (Char.IsLetterOrDigit(rpnNodes[i - 1].Name[0]) || Helpers.operatorsAndPriority.Any(item => item.Key == current))) &&
                    Helpers.operatorsAndPriority.Any(item => item.Key == current) && ((isFirst ? true : rpnNodes[i - 2].IsEvaluated) || (isFirst ? true : rpnNodes[i - 1].IsEvaluated)))
                {
                    Node nodeToAdd = new Node()
                    {
                        Name = rpnNodes[i].Name,
                        LeftSide = rpnNodes[i - 2],
                        RightSide = rpnNodes[i - 1],
                        IsEvaluated = true
                    };
                    rpnNodes[i - 2].Parent = nodeToAdd;
                    rpnNodes[i - 1].Parent = nodeToAdd;
                    tmp.Add(nodeToAdd);
                    curridx = i;
                    i += 3;
                }
                else
                {
                    tmp.Add(rpnNodes[i - 2]);
                    curridx = i - 2;
                    i += 1;
                }
            }
            //if (curridx < rpnNodes.Count - 1)
            //{
            //    for (int i = curridx + 1; i < rpnNodes.Count; i++)
            //    {
            //        tmp.Add(rpnNodes[i]);
            //    }
            //}
            while(curridx++ < rpnNodes.Count - 1)
            {
                tmp.Add(rpnNodes[curridx]);
            }
            if (tmp.Except(rpnNodes).ToList().Count == 0) changes = false;
            else changes = true;

            if (tmp.Count == 0)
                root = rpnNodes[0];
            rpnNodes = tmp;

            return changes;
        }
    }
}
