using MiniCompiler.Interpretar.Values;
using MiniCompiler.Semantic.Types;

namespace MiniCompiler
{
    public class LiteralStringNode : ExpressionNode
    {
        private string _value;
        public LiteralStringNode(string lexeme)
        {
            _value = lexeme;
        }

        public override string ToXML()
        {
            return "<LiteralString>" +_value +"</LiteralString>";
        }

        public override Type ValidateSemantic()
        {
            return new StringType();
        }

        public override InterpreteValue Evaluate()
        {
            return new StringValue(_value);
        }
    }
}