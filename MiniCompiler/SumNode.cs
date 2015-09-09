using MiniCompiler.Semantic.Types;

namespace MiniCompiler
{
    internal class SumNode:BinaryOperationNode
    {
        
        public SumNode(ExpressionNode leftNode, ExpressionNode rightNode) : base(leftNode,rightNode)
        {
            Rules.Add("IntTypexIntType",new IntType());
            Rules.Add("StringTypexStringType", new StringType());
            Rules.Add("StringTypexIntType", new StringType());
            Rules.Add("BooleanTypexBooleanType", new BooleanType());
            Rules.Add("FloatTypexFloatType", new FloatType());
            Rules.Add("IntTypexFloatType", new FloatType());
            Rules.Add("FloatTypexIntType", new FloatType());

        }
    }
}