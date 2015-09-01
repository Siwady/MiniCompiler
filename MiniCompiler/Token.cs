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
        Sum,
        Id,
        Mult,
        Sub,
        Equal,
        Eos,
        Left_parent,
        Right_parent,
        Div,
        print,
        read,
        String,
        Int,
        Bool,
        Float,
        String_Literal,
        Int_Literal,
        Float_Literal,
        If,
        Then,
        While,
        End,
        Else,
        Do,
        For,
        To,
        Comma,
        Array,
        LeftBracket,
        RightBracket
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
