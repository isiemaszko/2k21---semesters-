using ProjectATI.Code.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.Parser
{
    public class Node
    {
        public NodeTypeEnum Type { get; set; }
        public List<Node> ChildList { get; set; }
        public Node LeftSide { get; set; }
        public Node RightSide { get; set; }
        public Node Parent { get; set; }
        public string Name { get; set; }
        public int number;
        public bool IsEvaluated { get; set; }

        public virtual List<Node> GetChildList()
        {
            throw new NotImplementedException();
        }

        public virtual List<Node> GetThenList()
        {
            throw new NotImplementedException();
        }

        public virtual List<Node> GetElseList()
        {
            throw new NotImplementedException();
        }

        public virtual string GetVariableName()
        {
            throw new NotImplementedException();
        }
    }

    public class NoOp : Node
    {
    }

}
