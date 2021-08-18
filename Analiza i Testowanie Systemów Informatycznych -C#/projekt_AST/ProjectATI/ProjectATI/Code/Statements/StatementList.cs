using ProjectATI.Code.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.Statements
{
    public class StatementList : Node
    {
        private List<Node> statementList;

        public StatementList(List<Node> statementList)
        {
            this.statementList = new List<Node>();

            foreach (Node node in statementList)
            {
                if (node is NoOp)
                    continue;

                this.statementList.Add(node);
            }
        }

        public override List<Node> GetChildList()
        {
            return this.statementList;
        }
    }
}
