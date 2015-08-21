namespace MiniCompiler
{
    internal class MultiplicationNode : BinaryOperationNode
    {
        public MultiplicationNode(ExpressionNode leftNode, ExpressionNode rightNode) : base(leftNode, rightNode)
        {
        }

        public override double Evaluate()
        {
            return LeftNode.Evaluate() * RightNode.Evaluate();
        }

        
    }
}