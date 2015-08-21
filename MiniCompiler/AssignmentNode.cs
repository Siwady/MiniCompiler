using System;

namespace MiniCompiler
{
    internal class AssignmentNode : StatementNode
    {
        public AssignmentNode(IdNode idNode, ExpressionNode value)
        {
            ID_Node = idNode;
            Value = value;
        }

        public IdNode ID_Node { get; set; }

        public ExpressionNode Value { get; set; }
        public override void Interpretar()
        {
            Variables.Instance.SetValue(ID_Node.Name, Value.Evaluate());
        }

        public override string ToXML()
        {
            return String.Format("<Id>{0}{1}</Id>", ID_Node.ToXML(),Value.ToXML());
        }
    }
}