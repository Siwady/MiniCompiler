namespace MiniCompiler
{
    public class LiteralFalseNode : ExpressionNode
    {
        public override string ToXML()
        {
            return "<False></False>";
        }
    }
}