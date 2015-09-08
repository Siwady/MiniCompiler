using System;
using System.Collections.Generic;
using MiniCompiler.Semantic;
using Type = MiniCompiler.Semantic.Types.Type;


namespace MiniCompiler
{
    internal abstract class BinaryOperationNode : ExpressionNode
    {
        public BinaryOperationNode(ExpressionNode leftNode, ExpressionNode rightNode)
        {
            LeftNode = leftNode;
            RightNode = rightNode;
        }

        public Dictionary<string, Type> Rules = new Dictionary<string, Type>();
     
        public ExpressionNode RightNode { get; set; }

        public ExpressionNode LeftNode { get; set; }

        public override string ToXML()
        {
            return String.Format("<{0}>"+LeftNode.ToXML()+RightNode.ToXML()+"</{0}>",GetType().Name);
        }

        public override Type ValidateSemantic()
        {
            var rightType = RightNode.ValidateSemantic();
            var leftType = LeftNode.ValidateSemantic();
            var pair = leftType.GetType().Name+"x" + rightType.GetType().Name;
            if(Rules.ContainsKey(pair))
            {
                return Rules[pair];
            }
            else
            {
                throw  new SemanticException("Tipo"+this.GetType().Name+" es incompatible con pair "+ pair);
            }
        }
    }
}