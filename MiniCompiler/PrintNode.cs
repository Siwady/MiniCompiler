using System;

namespace MiniCompiler
{
    internal class PrintNode : StatementNode
    {
        public PrintNode(ExpressionNode value)
        {

            Value = value;
        }

        public ExpressionNode Value { get; set; }
        public override void Interpretar()
        {
            //Console.WriteLine(Value.Evaluate());
        }

        public override string ToXML()
        {
            return String.Format("<Print>{0}</Print>",Value.ToXML());
        }
    }
}