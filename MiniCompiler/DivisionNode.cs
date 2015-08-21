namespace MiniCompiler
{
    internal class DivisionNode : BinaryOperationNode
    {
        public DivisionNode(ExpressionNode leftNode, ExpressionNode rightNode) : base(leftNode,rightNode)
        {

        }

        public override double Evaluate()
        {
            return LeftNode.Evaluate() / RightNode.Evaluate();
        }

       
    }
}