using MiniCompiler.Interpretar.Values;

namespace MiniCompiler.Semantic.Types
{
    public class StringType:Type 
    {
        public override InterpreteValue GetDefaultValue()
        {
            return new StringValue("");
        }
    }
}