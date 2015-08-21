using System;

namespace MiniCompiler
{
    internal abstract class BinaryOperationNode : ExpressionNode
    {
        public BinaryOperationNode(ExpressionNode leftNode, ExpressionNode rightNode)
        {
            LeftNode = leftNode;
            RightNode = rightNode;
        }

        public ExpressionNode RightNode { get; set; }

        public ExpressionNode LeftNode { get; set; }

        public override string ToXML()
        {
            return String.Format("<{0}>"+LeftNode.ToXML()+RightNode.ToXML()+"</{0}>",GetType().Name);
        }
    }
}