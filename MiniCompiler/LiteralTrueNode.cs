namespace MiniCompiler
{
    public class LiteralTrueNode : ExpressionNode
    {
        public override string ToXML()
        {
            return "<True></True>";
        }
    }
}