using System;
using MiniCompiler.Semantic;
using MiniCompiler.Semantic.Types;

namespace MiniCompiler
{
    internal class PrintNode : StatementNode
    {
        public PrintNode(ExpressionNode value)
        {

            Value = value;
        }

        public ExpressionNode Value { get; set; }
       
        public override string ToXML()
        {
            return String.Format("<Print>{0}</Print>",Value.ToXML());
        }

        public override void ValidateSemantic()
        {
            if(Value.ValidateSemantic() is ArrayType)
                throw new SemanticException("Tipo incompatible");
        }
    }
}