using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCompiler.Interpretar.Values
{
    public class FloatValue: InterpreteValue
    {
        public float Value { set; get; }

        public FloatValue(float value)
        {
            Value = value;
        }

        public override string TU_CADENA()
        {
            return Value.ToString();
        }
    }
}
