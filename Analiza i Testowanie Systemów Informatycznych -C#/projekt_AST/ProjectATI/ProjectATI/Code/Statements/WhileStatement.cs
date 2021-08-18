using ProjectATI.Code.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.Statements
{
    public class WhileStatement:Node
    {
        public Variable variable;
        private List<Node> statementList;

        public WhileStatement(Variable var,List<Node> statementList, int number)
        {
            this.variable = var;
            this.statementList = new List<Node>();
            this.number = number;

            PKB.Pkb.GetPkbInstance().SetUses(this, var);

            foreach (Node node in statementList)
            {
                if (node is NoOp)
                    continue;

                node.Parent = this;
                PopUses(node);
                PopModifies(node);
                this.statementList.Add(node);
            }
            PKB.Pkb.GetPkbInstance().AddStatement(this);
        }

        public WhileStatement(Variable var, List<Node> statementList)
        {
            this.variable = var;
            this.statementList = new List<Node>();

            PKB.Pkb.GetPkbInstance().SetUses(this, var);
            foreach (Node node in statementList)
            {
                if (node is NoOp)
                    continue;
                node.Parent = this;
                PopUses(node);
                PopModifies(node);
                this.statementList.Add(node);
            }
        }

        private void PopUses(Node node)
        {
            if (node is AssignStatement)
            {
                var varlst = PKB.Pkb.GetPkbInstance().GetUsing(node);
                foreach (Variable vari in varlst)
                {
                    PKB.Pkb.GetPkbInstance().SetUses(this, vari);
                }
            }
            if (node is IfStatement)
            {
                foreach (var nd in node.GetThenList())
                {
                    PopUses(nd);
                }
                foreach (var nd in node.GetElseList())
                {
                    PopUses(nd);
                }
            }
            if (node is WhileStatement)
            {
                foreach (var nd in node.GetChildList())
                {
                    PopUses(nd);
                }
            }
        }

        private void PopModifies(Node node)
        {
            if(node is AssignStatement)
            {
                PKB.Pkb.GetPkbInstance().SetModifies(this, (node as AssignStatement).left);
            }
            if(node is IfStatement)
            {
                foreach(var nd in node.GetThenList())
                {
                    PopModifies(nd);
                }
                foreach (var nd in node.GetElseList())
                {
                    PopModifies(nd);
                }
            }
            if(node is WhileStatement)
            {
                foreach (var nd in node.GetChildList())
                {
                    PopModifies(nd);
                }
            }
        }

        public override List<Node> GetChildList()
        {
            return this.statementList;
        }
    }
}
