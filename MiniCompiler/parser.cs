using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCompiler
{
    internal class Parser
    {
        public Parser(Lexer lex)
        {
            Lex=lex;
            ConsumeToken();
            
        }

        public Lexer Lex { set; get; }
        public Token CurrentToken { set; get; }
        private Dictionary<string, double> _variables=new Dictionary<string, double>();

        public void Parse()
        {
            StatementList();
            if (CurrentToken.Type != TokenType.EOF)
            {
                throw new ParserException("se esperaba EOF");
            }
        }

        private void StatementList()
        {
            if (CurrentToken.Type == TokenType.Id || CurrentToken.Type == TokenType.print || CurrentToken.Type == TokenType.read)
            {
                Statement();
                StatementList();
            }
            else
            {
                //epsilon
            }
        }

        private void Statement()
        {
            if (CurrentToken.Type==TokenType.Id)
            {
                string id_lexema = CurrentToken.Lexeme;
                ConsumeToken();
                if (CurrentToken.Type != TokenType.Equal)
                    throw new ParserException("se esperaba =");
                
                ConsumeToken();
                double evalor=Expr();
                
                if (CurrentToken.Type!=TokenType.Eos)
                    throw new ParserException("se esperaba ;");
                
                ConsumeToken();
                _variables[id_lexema] = evalor;

            }else if (CurrentToken.Type == TokenType.print)
            {
                ConsumeToken();
                double evalor=Expr();
                Console.WriteLine(evalor);
                if (CurrentToken.Type != TokenType.Eos)
                    throw new ParserException("se esperaba EOS");

                ConsumeToken();
            }
            else if (CurrentToken.Type == TokenType.read)
            {
                ConsumeToken();
                if (CurrentToken.Type!=TokenType.Id)
                    throw new ParserException("se esperaba ID");
                string id_lexema = CurrentToken.Lexeme;
                
                ConsumeToken();
                if (CurrentToken.Type != TokenType.Eos)
                    throw new ParserException("se esperaba EOS");

                ConsumeToken();
                ||double valor=Double.Parse(Console.ReadLine());
                _variables[id_lexema] = valor;
            }
            else
            {
                throw new ParserException("se esperaba una sentencia");
            }
        }

        private double Expr()
        {
            double valor = 0;
            double fvalor=Factor();
            valor=ExprP(fvalor);
            return valor;
        }

        private double ExprP(double param)
        {
            double valor=0;
            if (CurrentToken.Type == TokenType.Sum)
            {
                ConsumeToken();
                double fvalor=Factor();
                valor=ExprP(param+fvalor);
            }
            else if (CurrentToken.Type == TokenType.Sub)
            {
                ConsumeToken();
                double fvalor = Factor();
                valor = ExprP(param - fvalor);
            }
            else
                valor = param;
            return valor;
        }

        private double Factor()
        {
            double valor = 0;
            double tvalor=Term();
            valor=FactorP(tvalor);
            return valor;
        }

        private double FactorP(double param)
        {
            double valor = 0;
            if (CurrentToken.Type == TokenType.Mult)
            {
                ConsumeToken();
                double tvalor=Term();
                valor=FactorP(param*tvalor);
            }
            else if (CurrentToken.Type == TokenType.Div)
            {
                ConsumeToken();
                double tvalor = Term();
                valor = FactorP(param / tvalor);
            }
            else
                valor = param;
            
            return valor;
        }

        private double Term()
        {
            double valor = 0;
            if(CurrentToken.Type==TokenType.Number)
            {
                valor=Double.Parse(CurrentToken.Lexeme);
                ConsumeToken();
            }
            else if (CurrentToken.Type == TokenType.Id)
            {
                if (_variables.ContainsKey(CurrentToken.Lexeme))
                    valor = _variables[CurrentToken.Lexeme];
                
                ConsumeToken();
            }
            else if (CurrentToken.Type == TokenType.Left_parent)
            {
                ConsumeToken();
                double evalor=Expr();
                if (CurrentToken.Type != TokenType.Right_parent)
                    throw new ParserException("Se esperaba ) ");
                ConsumeToken();
                valor = evalor;
            }
            else
            {
                throw  new ParserException("Se esperaba termino");
            }
            return valor;
        }

        private void ConsumeToken()
        {
            CurrentToken = Lex.GetToken();
        }

    }
}
