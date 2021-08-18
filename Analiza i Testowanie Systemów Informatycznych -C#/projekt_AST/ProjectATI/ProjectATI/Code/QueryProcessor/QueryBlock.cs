using ProjectATI.Code.Enums;
using ProjectATI.Code.Parser;
using ProjectATI.Code.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.QueryProcessor
{
    public class QueryBlock
    {

    }

    class SuchThatAndBlock : QueryBlock
    {
        
        public RelationToken relation;
        public SuchThatAndBlock(RelationToken relation)
        {
            
            this.relation = relation;
        }
    }

    class WithBlock : QueryBlock
    {
        public AtributeNameToken relation;
        public QueryVariable parameter;
        public AtributeNameToken relationParam;

        public WithBlock(AtributeNameToken relation, QueryVariable parameter)
        {
            this.relation = relation;
            this.parameter = parameter;
        }

        public WithBlock(AtributeNameToken relation, AtributeNameToken relationParam)
        {
            this.relation = relation;
            this.relationParam = relationParam;
        }

    }

    class PatternBlock : QueryBlock
    {
        public QueryVariable synonym;
        public QueryVariable firstParam;
        public PatternSecondParam secondParam;
        public PatternBlock(QueryVariable synonym,QueryVariable firstParam, PatternSecondParam secondParam)
        {
            this.synonym = synonym;
            this.firstParam = firstParam;
            this.secondParam = secondParam;
        }

    }

    class PatternSecondParam
    {
        public QueryVariable first = null;
        public string second = null;
        public QueryVariable third = null;

        public PatternSecondParam(QueryVariable first , string second , QueryVariable third)
        {
            this.first = first;
            this.second = second;
            this.third = third;
        }
    }
}
