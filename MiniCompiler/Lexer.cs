using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace MiniCompiler
{
    class Lexer
    {
        private string _input;
        private int _position = 0;
        private int _column= 0;
        private int _row=0;

        public Lexer(string input)
        {
            _input = input;
        }

        public char GetCurrentSymbol()
        {
            if (_position < _input.Length)
            {
                return _input[_position];
            }
            
            return '\0';
        }

        public char GetNextSymbol()
        {
            _position++;
            char symbol = GetCurrentSymbol();
            if (symbol == '\n')
            {
                _row++;
                _column = 0;
            }
            else
            {
                _column++;
            }
            return symbol;
        }

        public Token GetToken()
        {
            int state = 0;
            char symbol = GetCurrentSymbol();
            string lexeme = "";
            int col = _column;
            while (true)
            {
                switch (state)
                {
                    case 0:
                        if(symbol=='\0')
                            return new Token(){Type = TokenType.EOF};
                        else if (Char.IsDigit(symbol))
                        {
                            lexeme += symbol;
                            symbol = GetNextSymbol();
                            state = 1;
                        }
                        else if (symbol == '+' || symbol == '*' || symbol == '-' || symbol == '='
                            || symbol == ';' || symbol == '(' || symbol == ')' || symbol == '/' || symbol == '[' || symbol == ']')
                        {
                            lexeme += symbol;
                            symbol = GetNextSymbol();
                            state = 5;
                        }
                        else if (symbol == '\"')
                        {
                            lexeme += symbol;
                            symbol = GetNextSymbol();
                            state = 6;
                        }
                        else if (symbol == ',')
                        {
                            lexeme += symbol;
                            symbol = GetNextSymbol();
                            return new Token() { Type = TokenType.Comma, Lexeme = lexeme, Column = col, Row = _row };
                        }
                        else if (Char.IsWhiteSpace(symbol))
                        {
                            symbol = GetNextSymbol();
                            col = _column;
                        }else if (Char.IsLetter(symbol))
                        {
                            lexeme += symbol;
                            symbol = GetNextSymbol();
                            state = 4;
                        }
                        else
                        {
                            throw new LexicalException("current Symbol: " + symbol + " is not accepted");
                        }
                        break;
                    case 1:
                        if(Char.IsDigit(symbol))
                        {
                            lexeme += symbol;
                            symbol = GetNextSymbol();
                        }else if(symbol=='.')
                        {
                            lexeme += symbol;
                            symbol = GetNextSymbol();
                            state = 2;
                        }
                        else
                            return new Token(){Type = TokenType.Int_Literal, Lexeme = lexeme,Column = col,Row = _row};
                        break;

                    case 2:
                        if (Char.IsDigit(symbol))
                        {
                            lexeme += symbol;
                            symbol = GetNextSymbol();
                            state = 3;
                        }
                        else
                            throw new LexicalException("Se esperaba digito");
                        break;

                    case 3:
                        if (Char.IsDigit(symbol))
                        {
                            lexeme += symbol;
                            symbol = GetNextSymbol();
                        }
                        else
                            return new Token() { Type = TokenType.Float_Literal, Lexeme = lexeme, Column = col, Row = _row };
                        break;

                    case 4:
                        if (Char.IsLetter(symbol) || Char.IsDigit(symbol))
                        {
                            lexeme += symbol;
                            symbol = GetNextSymbol();
                        }
                        else
                        {
                            if(lexeme.ToLower().Equals("print"))
                            {
                                return new Token() { Type = TokenType.print, Lexeme = lexeme, Column = col, Row = _row };
                            }
                            else if (lexeme.ToLower().Equals("read"))
                            {
                                return new Token() { Type = TokenType.read, Lexeme = lexeme, Column = col, Row = _row };
                            }
                            else if (lexeme.ToLower().Equals("string"))
                            {
                                return new Token() { Type = TokenType.String, Lexeme = lexeme, Column = col, Row = _row };
                            }
                            else if (lexeme.ToLower().Equals("int"))
                            {
                                return new Token() { Type = TokenType.Int, Lexeme = lexeme, Column = col, Row = _row };
                            }
                            else if (lexeme.ToLower().Equals("bool"))
                            {
                                return new Token() { Type = TokenType.Bool, Lexeme = lexeme, Column = col, Row = _row };
                            }
                            else if (lexeme.ToLower().Equals("float"))
                            {
                                return new Token() { Type = TokenType.Float, Lexeme = lexeme, Column = col, Row = _row };
                            }
                            else if (lexeme.ToLower().Equals("if"))
                            {
                                return new Token() { Type = TokenType.If, Lexeme = lexeme, Column = col, Row = _row };
                            }
                            else if (lexeme.ToLower().Equals("then"))
                            {
                                return new Token() { Type = TokenType.Then, Lexeme = lexeme, Column = col, Row = _row };
                            }
                            else if (lexeme.ToLower().Equals("while"))
                            {
                                return new Token() { Type = TokenType.While, Lexeme = lexeme, Column = col, Row = _row };
                            }
                            else if (lexeme.ToLower().Equals("end"))
                            {
                                return new Token() { Type = TokenType.End, Lexeme = lexeme, Column = col, Row = _row };
                            }
                            else if (lexeme.ToLower().Equals("else"))
                            {
                                return new Token() { Type = TokenType.Else, Lexeme = lexeme, Column = col, Row = _row };
                            }
                            else if (lexeme.ToLower().Equals("do"))
                            {
                                return new Token() { Type = TokenType.Do, Lexeme = lexeme, Column = col, Row = _row };
                            }
                            else if (lexeme.ToLower().Equals("for"))
                            {
                                return new Token() { Type = TokenType.For, Lexeme = lexeme, Column = col, Row = _row };
                            }
                            else if (lexeme.ToLower().Equals("to"))
                            {
                                return new Token() { Type = TokenType.To, Lexeme = lexeme, Column = col, Row = _row };
                            }
                            else if (lexeme.ToLower().Equals("array"))
                            {
                                return new Token() { Type = TokenType.Array, Lexeme = lexeme, Column = col, Row = _row };
                            }
                            else
                                return new Token() {Type = TokenType.Id, Lexeme = lexeme, Column = col, Row = _row};
                        }
                        break;
                    case 5:
                        if (lexeme.Equals("+"))
                            return new Token() { Type = TokenType.Sum, Lexeme = lexeme, Column = col, Row = _row };
                        else if (lexeme.Equals("*"))
                            return new Token() { Type = TokenType.Mult, Lexeme = lexeme, Column = col, Row = _row };
                        else if (lexeme.Equals("-"))
                            return new Token() { Type = TokenType.Sub, Lexeme = lexeme, Column = col, Row = _row };
                        else if (lexeme.Equals("="))
                            return new Token() { Type = TokenType.Equal, Lexeme = lexeme, Column = col, Row = _row };
                        else if (lexeme.Equals(";"))
                            return new Token() { Type = TokenType.Eos, Lexeme = lexeme, Column = col, Row = _row };
                        else if (lexeme.Equals("("))
                            return new Token() { Type = TokenType.Left_parent, Lexeme = lexeme, Column = col, Row = _row };
                        else if (lexeme.Equals(")"))
                            return new Token() { Type = TokenType.Right_parent, Lexeme = lexeme, Column = col, Row = _row };
                        else if (lexeme.Equals("/"))
                            return new Token() { Type = TokenType.Div, Lexeme = lexeme, Column = col, Row = _row };
                        else if (lexeme.Equals("["))
                            return new Token() { Type = TokenType.LeftBracket, Lexeme = lexeme, Column = col, Row = _row };
                        else if (lexeme.Equals("]"))
                            return new Token() { Type = TokenType.RightBracket, Lexeme = lexeme, Column = col, Row = _row };
                        break;

                    case 6:
                        if (symbol != '\"')
                        {
                            lexeme += symbol;
                            symbol = GetNextSymbol();
                        }
                        else if(symbol=='\"')
                        {
                            lexeme += symbol;
                            return new Token() { Type = TokenType.String_Literal, Lexeme = lexeme, Column = col, Row = _row };
                        }
                        break;
                }
            }
            
        }
    }

    internal class LexicalException : Exception
    {
        public LexicalException(string message):base(message)
        {
        }
    }
}
