using MiniCompiler.Interpretar.Values;

namespace MiniCompiler.Semantic.Types
{
    public class BooleanType:Type
    {
        public override InterpreteValue GetDefaultValue()
        {
            return new BooleanValue(false);
        }
    }
}