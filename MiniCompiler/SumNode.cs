namespace MiniCompiler
{
    internal class SumNode:BinaryOperationNode
    {
        
        public SumNode(ExpressionNode leftNode, ExpressionNode rightNode) : base(leftNode,rightNode)
        {

        }

        public override double Evaluate()
        {
            return LeftNode.Evaluate() + RightNode.Evaluate();
        }

        
    }
}