using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.Parser
{
    public class Token
    {
        public TokenType Type { get; private set; }
        public string Value { get; private set; }
        
        public Token(TokenType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public Token(TokenType type)
        {
            this.Type = type;
        }

        internal static Token None()
        {
            return new Token(TokenType.None, "");
        }

        public override string ToString()
        {
            return this.Value;
        }
    }

    public enum TokenType
    {
        None,
        Number,
        Variable,
        Assignment,
        Procedure,
        BeginStmt,
        EndStmt,
        Semi,
        Program,
        While,
        If,
        Then,
        Else,
        Calls,
        Statement,
        Constant,
        Prog_line,
        varName,
        procName,
        Equals,
        Calc,
        BOOLEAN,
        Any,
        _Underscore
    }
}
