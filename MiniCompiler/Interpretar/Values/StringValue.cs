using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCompiler.Interpretar.Values
{
    class StringValue:InterpreteValue
    {
        public string Value { set; get; }

        public StringValue(string value)
        {
            Value = value;
        }

        public override string TU_CADENA()
        {
            return Value;
        }
    }
}
