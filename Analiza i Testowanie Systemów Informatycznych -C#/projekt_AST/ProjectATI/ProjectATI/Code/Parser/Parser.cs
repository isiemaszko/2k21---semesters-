using ProjectATI.Code.Parser;
using ProjectATI.Code.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProjectATI.Code.Parser
{
    public class Parser
    {
        private Token curToken;
        private int curPos;
        private int charCount;
        private char curChar;
        private int instrNum;
        public string text { get; private set; }
        public Parser(string text)
        {
            var tmp = text.Split(new string[] { "\r\n" }, System.StringSplitOptions.None).Select(item => item.Trim()).ToList();
            for (int i = 0; i < tmp.Count; i++)
            {
                if (tmp[i].Contains("="))
                {
                    tmp[i] = "assign " + tmp[i];
                }
            }
            string tmpString = String.Join("\r\n", tmp);
            tmpString = "program p {\r\n" + tmpString + "\r\n}";
            this.text = string.IsNullOrEmpty(tmpString) ? string.Empty : tmpString;
            this.charCount = this.text.Length;
            this.curToken = Token.None();
            this.instrNum = 0;
            this.curPos = -1;
            this.Advance();
        }

        public Node Parse()
        {
            this.NextToken();
            Node node = this.GetProgramStatement();
            return node;
        }

        private Node GetProgramStatement()
        {
            this.CheckToken(TokenType.Program);
            string name = this.curToken.ToString();
            this.NextToken();
            this.CheckToken(TokenType.BeginStmt);
            List<Node> statementList = this.GetStatementList();
            this.CheckToken(TokenType.EndStmt);

            return new ProgramStatement(name, statementList);
        }

        private Node GetStatement()
        {
            Node statement;


            if (this.curToken.Type == TokenType.Procedure)
            {
                statement = this.GetProcedureStatement();
            }
            else if (this.curToken.Type == TokenType.While)
            {
                statement = this.GetWhileStatement();
            }
            else if (this.curToken.Type == TokenType.If)
            {
                statement = this.GetIfStatement();
            }
            else if (this.curToken.Type == TokenType.Calls)
            {
                statement = this.GetCallsStatement();
            }
            else if (this.curToken.Type == TokenType.Assignment)
            {
                statement = this.GetAssignStatement();
            }
            else
            {
                statement = new NoOp();
            }

            return statement;
        }
        private List<Node> GetStatementList()
        {
            List<Node> statementList = new List<Node>();
            Node statement = this.GetStatement();
            statementList.Add(statement);

            while (this.curToken.Type != TokenType.EndStmt)
            {
                if (this.curToken.Type == TokenType.Procedure)
                {
                    statement = this.GetStatement();
                    statementList.Add(statement);
                }
                if (this.curToken.Type == TokenType.While)
                {

                    statement = this.GetStatement();
                    statementList.Add(statement);
                }
                if (this.curToken.Type == TokenType.Calls)
                {
                    statement = this.GetStatement();
                    statementList.Add(statement);
                }
                if (this.curToken.Type == TokenType.If)
                {
                    statement = this.GetStatement();
                    statementList.Add(statement);
                }
                if (this.curToken.Type == TokenType.Assignment)
                {
                    statement = this.GetStatement();
                    statementList.Add(statement);
                }
                if (this.curToken.Type == TokenType.Variable)
                {
                    throw new InvalidSyntaxException(string.Format("Invalid syntax at position {0}. ", this.curToken));
                }
            }

            return statementList;
        }

        private Node GetProcedureStatement()
        {
            this.CheckToken(TokenType.Procedure);
            string name = this.curToken.ToString();
            this.NextToken();
            this.CheckToken(TokenType.BeginStmt);
            List<Node> statementList = this.GetStatementList();
            this.CheckToken(TokenType.EndStmt);

            return new ProcedureStatement(name,statementList);
        }
        private Node GetWhileStatement()
        {
            int instr = this.GetInstructionNumber();
            this.CheckToken(TokenType.While);
            Token varToken = this.CheckToken(TokenType.Variable);
            this.CheckToken(TokenType.BeginStmt);
            List<Node> statementList = this.GetStatementList();
            this.CheckToken(TokenType.EndStmt);

            return new WhileStatement(new Variable(varToken), statementList, instr);
        }

        private Node GetIfStatement()
        {
            int instr = this.GetInstructionNumber();
            this.CheckToken(TokenType.If);
            Token varToken = this.CheckToken(TokenType.Variable);
            this.CheckToken(TokenType.Then);
            this.CheckToken(TokenType.BeginStmt);

            List<Node> statementListThen = this.GetStatementList();
            this.CheckToken(TokenType.EndStmt);

            if (this.curToken.Type == TokenType.Else)
            {
                this.CheckToken(TokenType.Else);
                this.CheckToken(TokenType.BeginStmt);

                List<Node> statementListElse = this.GetStatementList();
                this.CheckToken(TokenType.EndStmt);
                return new IfStatement(new Variable(varToken), statementListThen, statementListElse, instr);
            }
            else
            {
                return new IfStatement(new Variable(varToken), statementListThen, instr);
            }

        }

        private Node GetCallsStatement()
        {
            this.CheckToken(TokenType.Calls);
            int instr = this.GetInstructionNumber();
            string name = this.curToken.ToString();
            this.NextToken();
            this.CheckToken(TokenType.Semi);

            return new CallsStatement(name, instr);
        }

        private Node GetAssignStatement()
        {
            this.CheckToken(TokenType.Assignment);
            int instr = this.GetInstructionNumber();
            Token left = this.CheckToken(TokenType.Variable);
            this.CheckToken(TokenType.Equals, true);
            Token right = this.CheckToken(TokenType.Calc);
            this.CheckToken(TokenType.Semi);

            return new AssignStatement(new Variable(left), right, instr);
        }

        private Token CheckToken(TokenType tokenType, bool isAssign = false)
        {
            if (this.curToken.Type == tokenType)
            {
                Token token = this.curToken;
                this.NextToken(isAssign);
                return token;
            }

            else
            {
                throw new InvalidSyntaxException(string.Format("Invalid syntax at position {0}. Expected {1} but {2} is given.", this.curPos, tokenType, this.curToken.Type.ToString()));
            }
        }
        private void NextToken(bool isAssign = false)
        {
            //znak nowej linii \r\n
            if (this.curChar.Equals('\r'))
            {
                this.Advance();
                this.Advance();
            }

            if (this.curChar == char.MinValue)
            {
                this.curToken = Token.None();
                return;
            }
            
            if (this.curChar == ' ')
            {
                while (this.curChar != char.MinValue && this.curChar == ' ' && !char.IsSymbol(this.curChar))
                {
                    this.Advance();
                }

                if (this.curChar == char.MinValue)
                {
                    this.curToken = Token.None();
                    return;
                }
            }

            if ((this.curChar >= 'a' && this.curChar <= 'z') || this.curChar >= 'A' && this.curChar <= 'Z' || (this.curChar >= '0' && this.curChar <= '9') || this.curChar == '(' || this.curChar == ')')
            {
                string word = string.Empty;
                word += this.curChar;
                this.Advance();

                if ((this.curChar >= 'a' && this.curChar <= 'z') || (this.curChar >= 'A' && this.curChar <= 'Z') || this.curChar == '_' || (this.curChar >= '0' && this.curChar <= '9') || char.IsSymbol(this.curChar) || this.curChar == '(' || this.curChar == ')')
                {
                    while ((this.curChar >= 'a' && this.curChar <= 'z') || (this.curChar >= 'A' && this.curChar <= 'Z') || this.curChar == '_' || (this.curChar >= '0' && this.curChar <= '9') || char.IsSymbol(this.curChar) || this.curChar == '(' || this.curChar == ')')
                    {
                        word += this.curChar.ToString();
                        this.Advance();
                    }
                }

                if (string.Compare(word, "procedure", true) == 0)
                {
                    //string procName = getName();
                    this.curToken = new Token(TokenType.Procedure);
                }
                else if (string.Compare(word, "program", true) == 0)
                {
                   // string progName = getName();
                    this.curToken = new Token(TokenType.Program);
                }
                else if (string.Compare(word, "while", true) == 0)
                {

                    this.curToken = new Token(TokenType.While);
                }
                else if (string.Compare(word, "if", true) == 0)
                {

                    this.curToken = new Token(TokenType.If);
                }
                else if (string.Compare(word, "then", true) == 0)
                {

                    this.curToken = new Token(TokenType.Then);
                }
                else if (string.Compare(word, "else", true) == 0)
                {

                    this.curToken = new Token(TokenType.Else);
                }
                else if (string.Compare(word, "call", true) == 0)
                {
                    this.curToken = new Token(TokenType.Calls);
                }
                else if (string.Compare(word, "assign", true) == 0)
                {
                    this.curToken = new Token(TokenType.Assignment);
                }
                else
                {
                    if (isAssign)
                    {
                        while (this.curChar.ToString() != ";")
                        {
                            word += this.curChar.ToString();
                            this.Advance();
                        }
                        this.curToken = new Token(TokenType.Calc, word);
                    }
                    else
                    {
                        this.curToken = new Token(TokenType.Variable, word);
                    }
                }

                return;
            }

            if (this.curChar == '{')
            {
                this.curToken = new Token(TokenType.BeginStmt, "{");
                this.Advance();
                return;
            }

            if (this.curChar == '}')
            {
                this.curToken = new Token(TokenType.EndStmt, "}");
                this.Advance();
                return;
            }

            if (this.curChar == ';')
            {
                this.curToken = new Token(TokenType.Semi, ";");
                this.Advance();
                return;
            }

            if(this.curChar == '=')
            {
                this.curToken = new Token(TokenType.Equals, "=");
                this.Advance();
                return;
            }

            throw new InvalidSyntaxException(string.Format("Invalid syntax at position {0}. Unexpected symbol {1}", this.curPos + 1, this.curChar));
        }

        private String GetName()
        {
            if (this.curChar == ' ')
            {
                while (this.curChar != char.MinValue && this.curChar == ' ')
                {
                    this.Advance();
                }

                if (this.curChar == char.MinValue)
                {
                    this.curToken = Token.None();
                    return null;
                }
            }

            if ((this.curChar >= 'a' && this.curChar <= 'z') || this.curChar >= 'A' && this.curChar <= 'Z')
            {
                string word = string.Empty;
                word += this.curChar;
                this.Advance();

                if ((this.curChar >= 'a' && this.curChar <= 'z') || (this.curChar >= 'A' && this.curChar <= 'Z') || this.curChar == '_' || (this.curChar >= '0' && this.curChar <= '9'))
                {
                    while ((this.curChar >= 'a' && this.curChar <= 'z') || (this.curChar >= 'A' && this.curChar <= 'Z') || this.curChar == '_' || (this.curChar >= '0' && this.curChar <= '9'))
                    {
                        word += this.curChar.ToString();
                        this.Advance();
                    }
                }

                return word;
            }
            return null;
        }
        private void Advance()
        {
            this.curPos += 1;

            if (this.curPos < this.charCount)
            {
                this.curChar = this.text[this.curPos];
            }
            else
            {
                this.curChar = char.MinValue;
            }
        }

        private int GetInstructionNumber()
        {
            this.instrNum += 1;
            return this.instrNum;
        }
    }
}