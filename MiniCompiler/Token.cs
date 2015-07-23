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

    }

    class Token
    {
        public TokenType Type { set; get; }
        public string Lexeme { set; get; }
        public int Column { set; get; }
        public int Row { set; get; }
    }
}
