using System.Collections.Generic;
using MiniCompiler.Semantic;
using MiniCompiler.Semantic.Types;

namespace MiniCompiler
{
    public class WhileNodoStatement : StatementNode
    {
        public WhileNodoStatement(ExpressionNode expr, List<StatementNode> list)
        {
            Condition = expr;
            Code = list;
        }

        public List<StatementNode> Code { get; set; }

        public ExpressionNode Condition { get; set; }
        public override string ToXML()
        {
            throw new System.NotImplementedException();
        }

        public override void ValidateSemantic()
        {
            if(!(Condition.ValidateSemantic() is BooleanType))
                throw new SemanticException("Se esperaba expresion booleana en la sentencia while");

            foreach (var statement in Code)
            {
                statement.ValidateSemantic();
            }
        }

        public override void Interpret()
        {
            throw new System.NotImplementedException();
        }
    }
}