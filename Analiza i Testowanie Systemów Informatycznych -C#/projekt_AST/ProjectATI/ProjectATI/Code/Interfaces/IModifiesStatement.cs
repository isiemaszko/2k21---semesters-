using ProjectATI.Code.Parser;
using ProjectATI.Code.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.Interfaces
{
    interface IModifiesStatement
    {
        void SetModifies(Node statement, Variable variable);
        List<Variable> GetModified(Node statement);
        List<Node> GetStatementsModify(Variable variable);
        bool IsModified(Variable variable, Node statement);
    }
}
