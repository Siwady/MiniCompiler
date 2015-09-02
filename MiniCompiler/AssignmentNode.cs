using System;

namespace MiniCompiler
{
    internal class AssignmentNode : StatementNode
    {
        public AssignmentNode(ExpressionNode variable, ExpressionNode value)
        {
            Variable = variable;
            Value = value;
        }

        public ExpressionNode Variable { get; set; }

        public ExpressionNode Value { get; set; }
      

        public override string ToXML()
        {
            return String.Format("<Id>{0}{1}</Id>", Variable.ToXML(),Value.ToXML());
        }
    }
}