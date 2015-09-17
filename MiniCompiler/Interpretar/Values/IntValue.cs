using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCompiler.Interpretar.Values
{
    class IntValue:InterpreteValue
    {
        public int Value { set; get; }

        public IntValue(int value)
        {
            Value = value;
        }

        public override string TU_CADENA()
        {
            return Value.ToString();
        }
    }
}
