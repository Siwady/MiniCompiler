using System.Collections.Generic;

namespace MiniCompiler
{
    public class ForStatementNode : StatementNode
    {
        public ForStatementNode(ExpressionNode variable, ExpressionNode expr, ExpressionNode expr2, List<StatementNode> list)
        {
            Variable = variable;
            Expr = expr;
            Expr2 = expr2;
            ListState = list;
        }

        public List<StatementNode> ListState { get; set; }

        public ExpressionNode Expr2 { get; set; }

        public ExpressionNode Expr { get; set; }

        public ExpressionNode Variable { get; set; }

        public override string ToXML()
        {
            throw new System.NotImplementedException();
        }
    }
}