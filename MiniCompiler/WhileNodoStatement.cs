using System.Collections.Generic;

namespace MiniCompiler
{
    public class WhileNodoStatement : StatementNode
    {
        public WhileNodoStatement(ExpressionNode expr, List<StatementNode> list)
        {
            ExpressionN = expr;
            ExpressionListN = list;
        }

        public List<StatementNode> ExpressionListN { get; set; }

        public ExpressionNode ExpressionN { get; set; }
        public override string ToXML()
        {
            throw new System.NotImplementedException();
        }
    }
}