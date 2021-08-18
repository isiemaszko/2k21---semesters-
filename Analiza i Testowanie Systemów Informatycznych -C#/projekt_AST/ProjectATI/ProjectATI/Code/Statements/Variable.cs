using ProjectATI.Code.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.Statements
{
    public class Variable : Node
    {
        public Token Token { get; private set; }

        public Variable(Token token)
        {
            Token = token;
            PKB.Pkb.GetPkbInstance().InsertVar(this);
        }

        public override string GetVariableName()
        {
            return this.Token.Value;
        }

        public static explicit operator Variable(List<Node> v)
        {
            throw new NotImplementedException();
        }
    }
}
