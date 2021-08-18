using ProjectATI.Code.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.QueryProcessor
{
    internal class RelationResult
    {
        internal QueryVariable LeftVariable { get; private set; }
        internal QueryVariable RightVariable { get; private set; }
        internal List<Tuple<Node, Node>> Result { get; private set; }

        internal RelationResult(QueryVariable leftVariable, QueryVariable rightVariable, List<Tuple<Node, Node>> result)
        {
            LeftVariable = leftVariable;
            RightVariable = rightVariable;
            Result = result;
        }

        internal bool Contains(Node leftVariable, Node rightVariable)
        {
            foreach (Tuple<Node, Node> result in Result)
                if (result.Item1 == leftVariable && result.Item2 == rightVariable)
                    return true;

            return false;
        }
    }
}
