using MiniCompiler.Interpretar.Values;
using MiniCompiler.Semantic.Types;

namespace MiniCompiler
{
    public class LiteralFalseNode : ExpressionNode
    {
        public override string ToXML()
        {
            return "<False></False>";
        }

        public override Type ValidateSemantic()
        {
            return new BooleanType();
        }

        public override InterpreteValue Evaluate()
        {
            return new BooleanValue(false);
        }
    }
}