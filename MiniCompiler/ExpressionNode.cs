namespace MiniCompiler
{
    internal abstract class ExpressionNode
    {
        public abstract double Evaluate();
        public abstract string ToXML();
    }
}