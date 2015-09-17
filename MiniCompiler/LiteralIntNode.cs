using MiniCompiler.Interpretar.Values;
using MiniCompiler.Semantic.Types;

namespace MiniCompiler
{
    public class LiteralIntNode : ExpressionNode
    {
        private int _value;
        public LiteralIntNode(int value)
        {
            _value = value;
        }

        public override string ToXML()
        {
            return "<LiteralInt>" +_value+"</LiteralInt>";
        }

        public override Type ValidateSemantic()
        {
            return new IntType();
        }

        public override InterpreteValue Evaluate()
        {
            return new IntValue(_value);
        }
    }
}