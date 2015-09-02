using System;

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
    }
}