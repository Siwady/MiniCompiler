using MiniCompiler.Semantic.Types;

namespace MiniCompiler
{
    internal class SumNode:BinaryOperationNode
    {
        
        public SumNode(ExpressionNode leftNode, ExpressionNode rightNode) : base(leftNode,rightNode)
        {

        }


        public override Type ValidateSemantic()
        {
            var leftType=LeftNode.ValidateSemantic();
            var rightType = RightNode.ValidateSemantic();
        }
    }
}