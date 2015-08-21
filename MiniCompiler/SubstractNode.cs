namespace MiniCompiler
{
    internal class SubstractNode:BinaryOperationNode
    {
        public SubstractNode(ExpressionNode leftNode, ExpressionNode rightNode) : base(leftNode,rightNode)
        {

        }

        public override double Evaluate()
        {
            return LeftNode.Evaluate() - RightNode.Evaluate();
        }

       
    }
}