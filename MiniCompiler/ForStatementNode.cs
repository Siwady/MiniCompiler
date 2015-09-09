using System.Collections.Generic;
using MiniCompiler.Semantic;
using MiniCompiler.Semantic.Types;

namespace MiniCompiler
{
    public class ForStatementNode : StatementNode
    {
        public ForStatementNode(ExpressionNode variable, ExpressionNode initialValue, ExpressionNode finalValue, List<StatementNode> list)
        {
            Variable = variable;
            InitialValue = initialValue;
            FinalValue = finalValue;
            Code = list;
        }

        public List<StatementNode> Code { get; set; }

        public ExpressionNode FinalValue { get; set; }

        public ExpressionNode InitialValue { get; set; }

        public ExpressionNode Variable { get; set; }

        public override string ToXML()
        {
            throw new System.NotImplementedException();
        }

        public override void ValidateSemantic()
        {
            if(!(Variable.ValidateSemantic() is IntType))
            {
                throw new SemanticException("Se esperaba  variable entero");
            }
            if (!(InitialValue.ValidateSemantic() is IntType))
            {
                throw new SemanticException("Se esperaba  Expresion inicial entero");
            }
            if (!(FinalValue.ValidateSemantic() is IntType))
            {
                throw new SemanticException("Se esperaba  Expresion final entero");
            }

            foreach (var statement in Code)
            {
                statement.ValidateSemantic();
            }
        }
    }
}