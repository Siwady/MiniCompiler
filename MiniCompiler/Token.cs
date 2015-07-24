using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MiniCompiler
{
    internal enum TokenType
    {
        EOF,
        Number,
        Sum
    }

    class Token
    {
        public override string ToString()
        {
            return String.Format("Type: {0} , Lexeme: {1}, Column: {2}, Row: {3}",Type,Lexeme,Column,Row);
        }

        public TokenType Type { set; get; }
        public string Lexeme { set; get; }
        public int Column { set; get; }
        public int Row { set; get; }
    }
}
