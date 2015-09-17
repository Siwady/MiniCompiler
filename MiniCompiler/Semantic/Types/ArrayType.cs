using System.Collections.Generic;
using MiniCompiler.Interpretar.Values;

namespace MiniCompiler.Semantic.Types
{
    public class ArrayType:Type
    {
        public Type OfType;
        public  List<int> Dimensions;

        public ArrayType(Type ofType, List<int> dim )
        {
            OfType = ofType;
            Dimensions = dim;
        }

        public override InterpreteValue GetDefaultValue()
        {
            return new ArrayValue();
        }
    }
}