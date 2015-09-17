using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCompiler.Interpretar.Values
{
    public class ArrayValue:InterpreteValue
    {
        public override string TU_CADENA()
        {
            return "[]";
        }
    }
}
