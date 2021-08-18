using ProjectATI.Code.Parser;
using ProjectATI.Code.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.PKB
{
    class Parent
    {
        private List<Node> parents;
        private List<List<Node>> childrens;

        public Parent()
        {
            this.parents = new List<Node>();
            this.childrens = new List<List<Node>>();
        }

        public Node SetParent(Node parent, Node children)
        {
            if (!this.parents.Contains(parent))
            {
                this.parents.Add(parent);
                this.childrens.Add(new List<Node>());
            }

            if (!this.childrens[this.parents.IndexOf(parent)].Contains(children))
            {
                this.childrens[this.parents.IndexOf(parent)].Add(children);
            }

            return parent;
        }

        public Node GetParent(Node statement)
        {
            for(int i = 0; i<childrens.Count; i++)
            {
                if(childrens[i].Contains(statement))
                    return this.parents[i];
            }
            return null;
        }

        public List<Node> GetChildren(Node statement)
        {
            if (this.parents.Contains(statement))
            {
                return this.childrens[parents.IndexOf(statement)];
            }
            else
            {
                return null;
            }
        }

        public List<Node> GetParentStar(Node statement)
        {
            Node tmp = statement;
            List<Node> returnList = new List<Node>();
            while(tmp != null)
            {
                tmp = GetParent(tmp);
                if (tmp != null)
                    returnList.Add(tmp);
            }

            return returnList;
        }

        public List<Node> GetChildrenStar(Node statement)
        {
            List<Node> returnList = new List<Node>();

            var childs = GetChildren(statement);
            if(childs != null)
                returnList.AddRange(childs);
            else
            {
                return returnList;
            }
            foreach(var child in childs)
            {
                var temp = GetChildrenStar(child);
                if(temp != null)
                {
                    returnList.AddRange(temp);
                }
            }
            return returnList;
        }
    }
}
