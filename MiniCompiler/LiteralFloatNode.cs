using System.Globalization;

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
    }
}