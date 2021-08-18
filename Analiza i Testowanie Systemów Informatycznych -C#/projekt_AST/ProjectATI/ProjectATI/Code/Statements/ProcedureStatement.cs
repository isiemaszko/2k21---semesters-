using ProjectATI.Code.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.Statements
{
    public class ProcedureStatement : Node
    {
        private List<Node> statementList { get; set; }

        public ProcedureStatement(string name, List<Node> statementList)
        {
            this.Name = name;
            this.statementList = new List<Node>();

            foreach (Node node in statementList)
            {
                if (node is NoOp)
                    continue;

                node.Parent = this;
                this.statementList.Add(node);
            }
            PKB.Pkb.GetPkbInstance().AddStatement(this);
        }

        public override List<Node> GetChildList()
        {
            return this.statementList;
        }
    }
}
