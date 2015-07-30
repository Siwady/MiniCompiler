using System;

namespace MiniCompiler
{
    internal class ParserException : Exception
    {
        public ParserException(string message):base(message)
        {
            
        }
    }
}