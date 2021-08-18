using ProjectATI.Code.Parser;
using ProjectATI.Code.QueryProcessor;
using ProjectATI.Code.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.Enums
{
    internal class RelationToken
    {
        public RelationEnum Type { get; private set; }
        public QueryVariable firstParam { get; private set; }
        public QueryVariable secondParam { get; private set; }
        public RelationToken(RelationEnum type, QueryVariable firstParam, QueryVariable secondParam)
        {
            this.Type = type;
            this.firstParam = firstParam;
            this.secondParam = secondParam;
        }
    }

    public enum  RelationEnum
    {
        Parent=0,
        ParentStar=1,
        Follows=2,
        FollowsStar=3,
        Modifies=4,
        Uses = 5,
        Calls =6,
        CallsStar=7
    }
}
