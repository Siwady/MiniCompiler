using System;
using System.Collections.Generic;
using MiniCompiler.Semantic;
using MiniCompiler.Semantic.Types;

namespace MiniCompiler
{
    public class IfNodeStatement : StatementNode
    {
        
        public IfNodeStatement(ExpressionNode condition, List<StatementNode> statementList, List<StatementNode> falseCode)
        {
            Condition = condition;
            TrueCode = statementList;
            FalseCode = falseCode;
        }

        public List<StatementNode> TrueCode { get; set; }

        public List<StatementNode> FalseCode { get; set; }

        public ExpressionNode Condition { get; set; }

        public override string ToXML()
        {
            throw new NotImplementedException();
        }

        public override void ValidateSemantic()
        {
            if (!(Condition.ValidateSemantic() is BooleanType))
                throw new SemanticException("Se esperaba expresion booleana en la sentencia IF");

            foreach (var statement in TrueCode)
            {
                statement.ValidateSemantic();
            }
            if (FalseCode != null)
            {
                foreach (var statement in FalseCode)
                {
                    statement.ValidateSemantic();
                }
            }
        }

        public override void Interpret()
        {
            throw new NotImplementedException();
        }
    }
}