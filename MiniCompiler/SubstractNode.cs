using MiniCompiler.Semantic.Types;

namespace MiniCompiler
{
    internal class SubstractNode:BinaryOperationNode
    {
        public SubstractNode(ExpressionNode leftNode, ExpressionNode rightNode) : base(leftNode,rightNode)
        {
            Rules.Add("IntTypexIntType", new IntType());
            Rules.Add("FloatTypexFloatType", new FloatType());
            Rules.Add("IntTypexFloatType", new FloatType());
            Rules.Add("FloatTypexIntType", new FloatType());
        }

       

       
    }
}