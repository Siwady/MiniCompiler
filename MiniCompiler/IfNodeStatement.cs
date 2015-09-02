using System;
using System.Collections.Generic;

namespace MiniCompiler
{
    public class IfNodeStatement : StatementNode
    {
        
        public IfNodeStatement(ExpressionNode expr, List<StatementNode> statementList, List<StatementNode> elseStatement)
        {
            Expr = expr;
            StateList = statementList;
            ElseStatement = elseStatement;
        }

        public List<StatementNode> StateList { get; set; }

        public List<StatementNode> ElseStatement { get; set; }

        public ExpressionNode Expr { get; set; }

        public override string ToXML()
        {
            throw new NotImplementedException();
        }
    }
}