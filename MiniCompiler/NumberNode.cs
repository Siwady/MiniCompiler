using System;

namespace MiniCompiler
{
    internal class NumberNode : ExpressionNode
    {
        public NumberNode(double valor)
        {
            Valor = valor;
        }

        public double Valor { get; set; }
        public override double Evaluate()
        {
            return Valor;
        }

        public override string ToXML()
        {
            return String.Format("<Number>{0}</Number>", Valor);
        }
    }
}