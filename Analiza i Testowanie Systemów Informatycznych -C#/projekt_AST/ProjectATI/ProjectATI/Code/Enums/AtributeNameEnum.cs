using ProjectATI.Code.QueryProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.Enums
{
    class AtributeNameToken
    {
        public AtributeNameEnum Type { get; private set; }
        public QueryVariable Variable;

        public AtributeNameToken(AtributeNameEnum type, QueryVariable var)
        {
            Type = type;
            Variable = var;
        }
    }
    internal enum AtributeNameEnum
    {
        StmtLine = 0,
        varName = 1,
        value=2,
        procName=3,
    }
}
