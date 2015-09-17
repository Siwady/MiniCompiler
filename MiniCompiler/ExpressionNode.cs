using MiniCompiler.Interpretar.Values;
using MiniCompiler.Semantic.Types;

namespace MiniCompiler
{
    public abstract class ExpressionNode
    {
       
        public abstract string ToXML();
        public abstract Type ValidateSemantic();
        public abstract InterpreteValue Evaluate();
    }
}