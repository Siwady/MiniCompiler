using MiniCompiler.Interpretar.Values;
using MiniCompiler.Semantic.Types;

namespace MiniCompiler
{
    public class LiteralTrueNode : ExpressionNode
    {
        public override string ToXML()
        {
            return "<True></True>";
        }

        public override Type ValidateSemantic()
        {
            return new BooleanType();
        }

        public override InterpreteValue Evaluate()
        {
            return new BooleanValue(true);
        }
    }
}