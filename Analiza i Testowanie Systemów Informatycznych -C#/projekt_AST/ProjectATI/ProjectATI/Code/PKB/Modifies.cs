using ProjectATI.Code.Parser;
using ProjectATI.Code.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.PKB
{
    public class Modifies
    {
        public List<Node> modifiesList;
        public List<List<Node>> modifiesByList;


        public Modifies()
        {
            this.modifiesByList = new List<List<Node>>();
            this.modifiesList = new List<Node>();
        }

        public Modifies(List<Node> modifiesList, List<List<Node>> modifiesByList)
        {
            this.modifiesList = new List<Node>(modifiesList);
            this.modifiesByList = new List<List<Node>>(modifiesByList);
        }
        
        public Node SetModifies(Node statement, Variable variable)
        {
            if (!this.modifiesList.Contains(statement))
            {
                this.modifiesList.Add(statement);
                this.modifiesByList.Add(new List<Node>());
            }
            var nameInList = this.modifiesByList[this.modifiesList.IndexOf(statement)].Where(item => item.GetVariableName() == variable.Token.Value).FirstOrDefault();
            if (nameInList == null)
            {
                this.modifiesByList[this.modifiesList.IndexOf(statement)].Add(variable);
            }

            return statement;
        }


        public List<Node> GetModifies(Node statement)
        {
            if (statement.GetType() == typeof(CallsStatement))
            {
                Node parent = statement.Parent;
                while (true)
                {
                    parent = parent.Parent;
                    if (parent.GetType() == typeof(ProgramStatement))
                    {
                        break;
                    }
                }

                foreach (Node child in parent.GetChildList())
                {
                    if (child.Name == statement.Name)
                    {
                        if (this.modifiesList.Contains(child))
                            return this.modifiesByList[this.modifiesList.IndexOf(child)];

                    }
                }
            }

            if (this.modifiesList.Contains(statement))
                return this.modifiesByList[this.modifiesList.IndexOf(statement)];

            return new List<Node>();
        }

        public List<Node> GetModifiesBy(Node variable)
        {
            var stmtList = new List<Node>();
            foreach (var statement in this.modifiesByList)
            {
                if (statement.Any(item => ((Variable)item).Token.Value == ((Variable)variable).Token.Value))
                {
                    stmtList.Add(this.modifiesList[this.modifiesByList.IndexOf(statement)]);
                }
            }
            return stmtList;
        }
    }
}
