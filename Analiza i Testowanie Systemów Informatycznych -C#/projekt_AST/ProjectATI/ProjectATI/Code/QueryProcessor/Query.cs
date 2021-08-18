using ProjectATI.Code.Enums;
using ProjectATI.Code.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.QueryProcessor
{
    public class Query
    {
        public QueryVariable EvaluatedVariable { get; set; }
        public List<QueryVariable> AllVariables { get; set; }
        public List<QueryBlock> QueryBlocks { get; set; }
        public Query(QueryVariable evaluatedVariable, List<QueryVariable> allVariables, List<QueryBlock> queryBlocks)
        {
            EvaluatedVariable = evaluatedVariable;
            AllVariables = allVariables;
            QueryBlocks = queryBlocks;
        }
    }
}
