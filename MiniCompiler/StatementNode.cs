using System;

namespace MiniCompiler
{
    public abstract class StatementNode
    {
       
        public abstract string ToXML();
        public abstract void ValidateSemantic();
        public abstract void Interpret();
    }
}