using System;

namespace MiniCompiler
{
    internal abstract class StatementNode
    {
        public abstract void Interpretar();
        public abstract string ToXML();
    }
}