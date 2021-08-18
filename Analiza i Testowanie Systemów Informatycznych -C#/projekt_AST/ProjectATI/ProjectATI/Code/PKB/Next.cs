using ProjectATI.Code.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectATI.Code.Statements;

namespace ProjectATI.Code.PKB
{
    class Next
    {
        private List<Node> prev;
        private List<List<Node>> next;

        public Next()
        {
            this.prev = new List<Node>();
            this.next = new List<List<Node>>();
        }

        public Node SetNext(Node first, Node second)
        {
            if (!this.prev.Contains(first))
            {
                this.prev.Add(first);
                this.next.Add(new List<Node>());
            }

            if (!this.next[this.prev.IndexOf(first)].Contains(second))
            {
                this.next[this.prev.IndexOf(first)].Add(second);
            }

            return first;
        }

        public List<Node> GetPrev(Node second)
        {
            List<Node> returnList = new List<Node>();
            for (int i = 0; i < next.Count; i++)
            {
                if (next[i].Contains(second))
                    // return this.prev[i];
                    returnList.Add(this.prev[i]);
            }
            if (returnList.Any())
                return returnList;
            else
                return null;
        }

        public List<Node> GetNext(Node first)
        {
            if (this.prev.Contains(first))
            {
                return this.next[prev.IndexOf(first)];
            }
            else
            {
                return null;
            }
        }

        public List<Node> GetPrevStar(Node second)
        {
            List<Node> returnList = new List<Node>();
            while (true)
            {
                var prevs = GetPrev(second);
                if (prevs == null)
                    break;
                
                if(second is WhileStatement)
                {
                    if (returnList.Contains(prevs[1]))
                    {
                        if (!returnList.Contains(prevs[0]))
                        {
                            returnList.Add(prevs[0]);
                        }
                        second = prevs[0];
                    }
                    else
                    {
                        returnList.Add(prevs[1]);
                        second = prevs[1];
                    }
                }
                else if(prevs.Count == 2)
                {
                    foreach (var node in prevs)
                    {
                        returnList.Add(node);
                        var toadd = GetPrevStar(node);
                        foreach (var n in toadd)
                        {
                            if (!returnList.Contains(n))
                                returnList.Add(n);
                        }
                    }
                    second = prevs[0];
                }
                else
                {
                    if (!returnList.Contains(prevs[0]))
                    {
                        returnList.Add(prevs[0]);
                        second = prevs[0];
                    }
                    else
                    {
                        second = prevs[0];
                    }
                }

            }
            return returnList;
        }

        public List<Node> GetNextStar(Node first)
        {
            List<Node> returnList = new List<Node>();
            while (true)
            {
                var nexts = GetNext(first);
                if (nexts == null)
                    break;
                if(first is WhileStatement)
                {
                    if (returnList.Contains(nexts[0]))
                    {
                        returnList.Add(nexts[1]);
                        first = nexts[1];
                    }
                    else
                    {
                        returnList.Add(nexts[0]);
                        first = nexts[0];
                    }
                }
                else if(first is IfStatement)
                {
                    foreach (var node in nexts){
                        returnList.Add(node);
                        var toadd = GetNextStar(node);
                        foreach(var n in toadd)
                        {
                            if (!returnList.Contains(n))
                                returnList.Add(n);
                        }
                    }
                    first = nexts[0];
                }
                else
                {
                    if (!returnList.Contains(nexts[0]))
                    {
                        returnList.Add(nexts[0]);
                        first = nexts[0];
                    }
                    else
                    {
                        first = nexts[0];
                    }
                }

            }
            return returnList;
        }
    }
}
