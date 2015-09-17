using MiniCompiler.Interpretar.Values;
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

        public override InterpreteValue Evaluate()
        {
            InterpreteValue leftType = LeftNode.Evaluate();
            InterpreteValue righType = RightNode.Evaluate();
            if (leftType is IntValue && righType is IntValue)
            {
                return new IntValue(((IntValue)leftType).Value + ((IntValue)righType).Value);
            }
            else if (leftType is FloatValue && righType is FloatValue)
            {
                return new FloatValue(((FloatValue)leftType).Value + ((FloatValue)righType).Value);
            }
            else if (leftType is StringValue && righType is StringValue)
            {
                return new StringValue(((StringValue)leftType).Value + ((StringValue)righType).Value);
            }
            return null;
        }
    }
}