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
            if (CurrentToken.Type == TokenType.Id || CurrentToken.Type == TokenType.print)
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
                ConsumeToken();
                if (CurrentToken.Type != TokenType.Equal)
                    throw new ParserException("se esperaba =");
 
                ConsumeToken();
                Expr();
                
                if (CurrentToken.Type!=TokenType.Eos)
                    throw new ParserException("se esperaba ;");
                
                ConsumeToken();

            }else if (CurrentToken.Type == TokenType.print)
            {
                ConsumeToken();
                Expr();
                if (CurrentToken.Type != TokenType.Eos)
                    throw new ParserException("se esperaba EOS");

                ConsumeToken();
            }
            else
            {
                throw new ParserException("se esperaba una sentencia");
            }
        }

        private void Expr()
        {
            Factor();
            ExprP();
        }

        private void ExprP()
        {
            if (CurrentToken.Type == TokenType.Sum)
            {
                ConsumeToken();
                Factor();
                ExprP();
            }
            else if (CurrentToken.Type == TokenType.Sub)
            {
                ConsumeToken();
                Factor();
                ExprP();
            }
            else
            {
                //epsilon       
            }

        }

        private void Factor()
        {
            Term();
            FactorP();
        }

        private void FactorP()
        {
            if (CurrentToken.Type == TokenType.Mult)
            {
                ConsumeToken();
                Term();
                FactorP();
            }
            else if (CurrentToken.Type == TokenType.Div)
            {
                ConsumeToken();
                Term();
                FactorP();
            }
            else
            {
                //Epsilon
            }
        }

        private void Term()
        {
            if(CurrentToken.Type==TokenType.Number)
            {
                ConsumeToken();
            }
            else if (CurrentToken.Type == TokenType.Id)
            {
                ConsumeToken();
            }
            else if (CurrentToken.Type == TokenType.Left_parent)
            {
                ConsumeToken();
                Expr();
                if (CurrentToken.Type != TokenType.Right_parent)
                    throw new ParserException("Se esperaba ) ");
                ConsumeToken();
            }
            else
            {
                throw  new ParserException("Se esperaba termino");
            }
        }

        private void ConsumeToken()
        {
            CurrentToken = Lex.GetToken();
        }

    }
}
