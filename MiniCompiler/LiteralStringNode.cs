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
    }
}