using MiniCompiler.Semantic.Types;

namespace MiniCompiler
{
    internal class DivisionNode : BinaryOperationNode
    {
        public DivisionNode(ExpressionNode leftNode, ExpressionNode rightNode) : base(leftNode,rightNode)
        {
            Rules.Add("IntTypexIntType", new IntType());
            Rules.Add("FloatTypexFloatType", new FloatType());
            Rules.Add("IntTypexFloatType", new FloatType());
            Rules.Add("FloatTypexIntType", new FloatType());
        }
    }
}