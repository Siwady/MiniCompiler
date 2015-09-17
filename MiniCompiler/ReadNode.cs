using System;
using MiniCompiler.Semantic;
using MiniCompiler.Semantic.Types;

namespace MiniCompiler
{
    internal class ReadNode : StatementNode
    {
        public ReadNode(ExpressionNode variable)
        {

            Variable = variable;
        }

        public ExpressionNode Variable { get; set; }
        

        public override string ToXML()
        {
            return String.Format("<Read>{0}</Read>",Variable.ToXML());
        }

        public override void ValidateSemantic()
        {
            if (Variable.ValidateSemantic() is ArrayType)
                throw new SemanticException("Tipo incompatible");
        }

        public override void Interpret()
        {
            throw new NotImplementedException();
        }
    }
}