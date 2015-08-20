namespace MiniCompiler
{
    internal class BinaryOperationNode : ExpressionNode
    {
        public BinaryOperationNode(ExpressionNode leftNode, ExpressionNode rightNode)
        {
            LeftNode = leftNode;
            RightNode = rightNode;
        }

        public ExpressionNode RightNode { get; set; }

        public ExpressionNode LeftNode { get; set; }
    }
}