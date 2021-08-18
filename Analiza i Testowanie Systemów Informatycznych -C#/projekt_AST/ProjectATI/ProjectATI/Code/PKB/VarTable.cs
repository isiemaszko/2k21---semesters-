using ProjectATI.Code.Interfaces;
using ProjectATI.Code.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.PKB
{
    class VarTable : IVarTable
    {
        public List<Variable> varList { get; set; }

        public VarTable()
        {
            this.varList = new List<Variable>();
        }

        public int InsertVar(Variable var)
        {
            var isInList = varList.Where(x => x.Token.Value.Equals(var.Token.Value)).FirstOrDefault() != null;
            if (isInList)
                return this.varList.IndexOf(var);

            this.varList.Add(var);
            return this.varList.IndexOf(var);
        }

        public string GetVarNameByIndex(int varIndex)
        {
            return this.varList[varIndex].Name;
        }

        public int GetVarIndexByName(string varName)
        {
            var variable = varList.Where(x => x.Name.Equals(varName)).FirstOrDefault();
            return this.varList.IndexOf(variable);
        }

        public int GetSize()
        {
            return this.varList.Count();
        }

        public List<Variable> GetAll()
        {
            return this.varList;
        }
    }
}
