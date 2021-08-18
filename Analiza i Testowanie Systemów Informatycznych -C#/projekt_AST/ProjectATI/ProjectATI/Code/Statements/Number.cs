using ProjectATI.Code.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.Statements
{
    public class Number :Node
    {
        public Token Token { get; private set; }

        public Number(Token token)
        {
            this.Token = token;
        }
    }
}
