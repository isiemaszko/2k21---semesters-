using ProjectATI.Code.Parser;
using ProjectATI.Code.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.QueryProcessor
{
    public class QueryResultProjector
    {
        private readonly QueryVariable evaluatedVariable;
        private readonly QueryResults queryResults;

        public QueryResultProjector(QueryVariable evaluatedVariable, QueryResults queryResults)
        {
            this.evaluatedVariable = evaluatedVariable;
            this.queryResults = queryResults;
        }

        public string Project()
        {
            string text = "";
            if (evaluatedVariable.Type.Type == TokenType.BOOLEAN)
            {
                if (queryResults.IsEmpty())
                    return "false";
                return "true";
            }

            List<Node> nodes = queryResults.ReturnColumn(evaluatedVariable).Distinct().ToList();
            List<string> result;
            switch (evaluatedVariable.Type.Type)
            {
                case TokenType.Procedure:
                    result = nodes.Select(node => (node as ProcedureStatement).Name).ToList();
                    break;

                case TokenType.Variable:
                    result = nodes.Select(node => (node as Variable).Token.Value).ToList();
                    break;

                default:
                    result = nodes.Select(node => node.number.ToString()).ToList();
                    break;
            }

            foreach (var item in result)
            {
                text += item + ", " ;
            }
            if (text.Length > 1)
                text = text.Remove(text.Length - 2);
            if (string.IsNullOrEmpty(text)) text = "none";
            return text;
        }
    }
}
