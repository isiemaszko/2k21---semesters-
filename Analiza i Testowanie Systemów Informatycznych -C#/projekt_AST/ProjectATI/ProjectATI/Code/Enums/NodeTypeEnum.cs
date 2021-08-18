using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.Enums
{
    public enum NodeTypeEnum
    {
        Assign = 1,
        Call = 2,
        If = 3,
        Else = 4,
        While = 5,
        StmtList = 6,
        Procedure = 7,
        Variable=8,
    }
}
