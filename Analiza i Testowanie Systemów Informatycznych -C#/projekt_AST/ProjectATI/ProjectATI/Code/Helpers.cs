using ProjectATI.Code.Parser;
using ProjectATI.Code.Statements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code
{
    public class Helpers
    {
        public static Dictionary<string, int> operatorsAndPriority = new Dictionary<string, int>()
        {
            {"+", 1 }, {"-", 1 }, {"*", 2}, {"/", 2 }, {"(", 0}, {")", 0}
        };


        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        public static bool CheckIfHaveChildList(Node n)
        {
            if (n is WhileStatement || n is IfStatement || n is ProcedureStatement || n is ProgramStatement)
                return true;
            return false;
        }

        public static bool CheckIfStatement(Node n)
        {
            if (n is WhileStatement || n is IfStatement || n is AssignStatement || n is CallsStatement)
                return true;
            return false;
        }
    }
}
