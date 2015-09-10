using System.Collections.Generic;
using MiniCompiler.Semantic;
using MiniCompiler.Semantic.Types;

namespace MiniCompiler
{
    public class VaribleArrayNode : ExpressionNode
    {
        public VaribleArrayNode(ExpressionNode expressionNode, List<ExpressionNode> expL)
        {
            Variable = expressionNode;
            Dimension = expL;
        }

        public List<ExpressionNode> Dimension { get; set; }

        public ExpressionNode Variable { get; set; }

        public override string ToXML()
        {
            throw new System.NotImplementedException();
        }

        public override Type ValidateSemantic()
        {
            Type VarType = Variable.ValidateSemantic();
            if (!(VarType is ArrayType))
            {
                throw new SemanticException("Se esperaba una variable de tipo arreglo");
            }

            
            if(((ArrayType)VarType).Dimensions.Count != Dimension.Count)
            {
                throw new SemanticException("Se esperaba una dimension de: "+((ArrayType)VarType).Dimensions.Count);
            }

            foreach (var expression in Dimension)
            {
                if (!(expression.ValidateSemantic() is IntType))
                {
                    throw new SemanticException("Se esperaba expresion de tipo entero");
                }
            }
            return ((ArrayType)VarType).OfType;
        }
    }
}