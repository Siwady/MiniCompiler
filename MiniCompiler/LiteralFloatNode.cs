using System.Globalization;
using MiniCompiler.Interpretar.Values;
using MiniCompiler.Semantic.Types;

namespace MiniCompiler
{
    public class LiteralFloatNode : ExpressionNode
    {
        private float _value;
        public LiteralFloatNode(float value)
        {
            _value = value;
        }

       

        public override string ToXML()
        {
            return "<LiteralFloat>"+_value.ToString(CultureInfo.InvariantCulture) + "</LiteralFloat>";
        }

        public override Type ValidateSemantic()
        {
            return new FloatType();
        }

        public override InterpreteValue Evaluate()
        {
            return new FloatValue(_value);
        }
    }
}