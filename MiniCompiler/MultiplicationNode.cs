using MiniCompiler.Semantic.Types;

namespace MiniCompiler
{
    internal class MultiplicationNode : BinaryOperationNode
    {
        public MultiplicationNode(ExpressionNode leftNode, ExpressionNode rightNode) : base(leftNode, rightNode)
        {
            Rules.Add("IntTypexIntType", new IntType());
            Rules.Add("BooleanTypexBooleanType", new BooleanType());
            Rules.Add("FloatTypexFloatType", new FloatType());
            Rules.Add("IntTypexFloatType", new FloatType());
            Rules.Add("FloatTypexIntType", new FloatType());
        }

       

        
    }
}