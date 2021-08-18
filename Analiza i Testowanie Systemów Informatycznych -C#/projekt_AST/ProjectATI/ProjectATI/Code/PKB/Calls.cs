using ProjectATI.Code.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.PKB
{
    public class Calls
    {

        public List<Node> callerList;
        public List<List<Node>> calledList;

        public Calls()
        {
            this.callerList = new List<Node>();
            this.calledList = new List<List<Node>>();
        }

        public Node SetCalls(Node caller, Node called)
        {
            if(!callerList.Contains(caller))
            {
                this.callerList.Add(caller);
                this.calledList.Add(new List<Node>());
            }

            if (!this.calledList[this.callerList.IndexOf(caller)].Contains(called))
            {
                this.calledList[this.callerList.IndexOf(caller)].Add(called);
            }

            return caller;
        }

        public Node GetCaller(Node statement)
        {
            for (int i = 0; i < calledList.Count; i++)
            {
                if (calledList[i].Contains(statement))
                    return this.callerList[i];
            }
            return null;
        }

        public List<Node> GetCallers(Node statement)
        {
            List<Node> res = new List<Node>();
            for (int i = 0; i < calledList.Count; i++)
            {
                if (calledList[i].Contains(statement))
                    res.Add(this.callerList[i]);
            }
            return res.Count > 0 ? res : null;
        }

        public List<Node> GetCalled(Node statement)
        {
            if (this.callerList.Contains(statement))
            {
                return this.calledList[callerList.IndexOf(statement)];
            }
            else
            {
                return null;
            }
        }

        public List<Node> GetCallerStar(Node statement)
        {
            var tmp = statement;
            List<Node> returnList = new List<Node>();

            var callers = GetCallers(statement);
            returnList.AddRange(callers);
            int i = 0;
            while (true)
            {
                callers = GetCallers(returnList[i++]);
                if (callers != null)
                {
                    foreach (var call in callers)
                    {
                        if (!returnList.Contains(call))
                            returnList.Add(call);
                    }
                }
                if (i == returnList.Count)
                    break;

            }

            return returnList;
        }


        public List<Node> GetCalledStar(Node statement)
        {
            List<Node> returnList = new List<Node>();

            var calleds = GetCalled(statement);
            if (calleds != null)
                returnList.AddRange(calleds);
            else
            {
                return returnList;
            }
            foreach (var called in calleds)
            {
                var temp = GetCalledStar(called);
                if (temp != null)
                {
                    returnList.AddRange(temp);
                }
            }
            return returnList;
        }


        public List<(Node, Node)> GetCallsPairs()
        {
            List<(Node, Node)> pairs = new List<(Node, Node)>();
            foreach (Node caller in callerList)
            {
                foreach (Node called in calledList[callerList.IndexOf(caller)])
                {
                    pairs.Add((caller, called));
                }
            }
            return pairs;
        }



    }
}
