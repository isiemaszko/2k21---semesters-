using ProjectATI.Code.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.Interfaces
{
    interface IVarTable
    {
        int InsertVar(Variable var);

        string GetVarNameByIndex(int varIndex);

        int GetVarIndexByName(string varName);

        int GetSize();
    }
}
