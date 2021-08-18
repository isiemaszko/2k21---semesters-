using ProjectATI.Code.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.Statements
{
    public class CallsStatement : Node
    {
        private string v;
        private List<Node> nodes;

        public CallsStatement(string name, int number)
        {
            this.Name = name;
            this.number = number;
            PKB.Pkb.GetPkbInstance().AddStatement(this);
            PKB.Pkb.GetPkbInstance().AddCallsListsTmp(this);
        }

        public CallsStatement(string v, List<Node> nodes)
        {
            this.v = v;
            this.nodes = nodes;
        }
    }
}
