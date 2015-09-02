using System.Collections.Generic;

namespace MiniCompiler
{
    internal class WhileNode:StatementNode
    {
        public WhileNode(ExpressionNode expression, List<StatementNode> statementList)
        {
            Expr = expression;
            StatementList = statementList;
        }

        public List<StatementNode> StatementList { get; set; }

        public ExpressionNode Expr { get; set; }
        public override void Interpretar()
        {
            throw new System.NotImplementedException();
        }

        public override string ToXML()
        {
            throw new System.NotImplementedException();
        }
    }
}