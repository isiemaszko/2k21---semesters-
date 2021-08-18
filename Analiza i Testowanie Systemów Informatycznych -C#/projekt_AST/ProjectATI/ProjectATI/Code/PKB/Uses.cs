using ProjectATI.Code.Parser;
using ProjectATI.Code.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.PKB
{
    public class Uses
    {
        public List<Node> usesList;
        public List<List<Node>> usedByList;

        public Uses()
        {
            this.usedByList = new List<List<Node>>();
            this.usesList = new List<Node>();
        }

        public Uses(List<Node> usesList, List<List<Node>> usedByList)
        {
            this.usesList = new List<Node>(usesList);
            this.usedByList = new List<List<Node>>(usedByList);
        }

        public Node SetUses(Node statement, Variable variable)
        {
            if (!this.usesList.Contains(statement))
            {
                this.usesList.Add(statement);
                this.usedByList.Add(new List<Node>());
            }
            var nameInList = this.usedByList[this.usesList.IndexOf(statement)].Where(item => item.GetVariableName() == variable.Token.Value).FirstOrDefault();
            if (nameInList == null)
            {
                this.usedByList[this.usesList.IndexOf(statement)].Add(variable);
            }

            return statement;
        }


        public List<Node> GetUsing(Node statement)
        {
            if(statement.GetType() == typeof(CallsStatement))
            {
                Node parent = statement.Parent;
                while (true)
                {
                    parent = parent.Parent;
                    if(parent.GetType() == typeof(ProgramStatement))
                    {
                        break;
                    }
                }

                foreach(Node child in parent.GetChildList())
                {
                    if(child.Name == statement.Name)
                    {
                        if (this.usesList.Contains(child))
                            return this.usedByList[this.usesList.IndexOf(child)];

                    }
                }
            }

            if (this.usesList.Contains(statement))
                return this.usedByList[this.usesList.IndexOf(statement)];

            return new List<Node>();
        }

        public List<Node> GetUsedBy(Node variable)
        {
            var stmtList = new List<Node>();
            foreach (var statement in this.usedByList)
            {
                if (statement.Any(item => ((Variable)item).Token.Value == ((Variable)variable).Token.Value))
                {
                    stmtList.Add(this.usesList[this.usedByList.IndexOf(statement)]);
                }
            }
            return stmtList;
        }
    }
}
