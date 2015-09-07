using System;

namespace MiniCompiler.Semantic
{
    public class SemanticException : Exception
    {
        public SemanticException(string message): base(message)
        {
        }
    }
}