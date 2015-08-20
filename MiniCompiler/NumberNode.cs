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
    }
}