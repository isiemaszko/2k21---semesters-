using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.Enums
{
    public enum DictionaryEnum
    {
        [Description("procedure")]
        Procedure,
        [Description("assign")]
        Assign,
        [Description("if")]
        If,
        [Description("else")]
        Else,
        [Description("while")]
        While,
        [Description("call")]
        Call,


    }
}
