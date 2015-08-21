using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
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

        public List<StatementNode> Parse()
        {
            var statement_list=StatementList();
            if (CurrentToken.Type != TokenType.EOF)
            {
                throw new ParserException("se esperaba EOF");
            }
            return statement_list;
        }

        private List<StatementNode> StatementList()
        {
            if (CurrentToken.Type == TokenType.Id || CurrentToken.Type == TokenType.print || CurrentToken.Type == TokenType.read)
            {
                var statement=Statement();
                var statement_list=StatementList();
                statement_list.Insert(0,statement);
                return statement_list;
            }
            else
            {
                return new List<StatementNode>();
            }
        }

        private StatementNode Statement()
        {
            if (CurrentToken.Type==TokenType.Id)
            {
                string id_lexema = CurrentToken.Lexeme;
                ConsumeToken();
                if (CurrentToken.Type != TokenType.Equal)
                    throw new ParserException("se esperaba =");
                
                ConsumeToken();
                var evalor=Expr();
                
                if (CurrentToken.Type!=TokenType.Eos)
                    throw new ParserException("se esperaba ;");
                
                ConsumeToken();
                return new AssignmentNode(new IdNode(id_lexema),evalor);

            }else if (CurrentToken.Type == TokenType.print)
            {
                ConsumeToken();
                var evalor=Expr();
                if (CurrentToken.Type != TokenType.Eos)
                    throw new ParserException("se esperaba EOS");

                ConsumeToken();
                return new PrintNode(evalor);
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
                return new ReadNode(new IdNode(id_lexema));
            }
            else
            {
                throw new ParserException("se esperaba una sentencia");
            }
        }

        private ExpressionNode Expr()
        {
            var fvalor=Factor();
            return ExprP(fvalor);
            
        }

        private ExpressionNode ExprP(ExpressionNode param)
        {
            if (CurrentToken.Type == TokenType.Sum)
            {
                ConsumeToken();
                var fvalor=Factor();
                return ExprP(new SumNode(param,fvalor));
            }
            else if (CurrentToken.Type == TokenType.Sub)
            {
                ConsumeToken();
                var fvalor = Factor();
                return ExprP(new SubstractNode(param , fvalor));
            }
            else
                return  param;
            
        }

        private ExpressionNode Factor()
        {
            var tvalor=Term();
            return FactorP(tvalor);
            
        }

        private ExpressionNode FactorP(ExpressionNode param)
        {
            if (CurrentToken.Type == TokenType.Mult)
            {
                ConsumeToken();
                var tvalor=Term();
                return FactorP(new MultiplicationNode(param,tvalor));
            }
            else if (CurrentToken.Type == TokenType.Div)
            {
                ConsumeToken();
                var tvalor = Term();
                return FactorP(new DivisionNode(param ,tvalor));
            }
            else
                return param;
            
        }

        private ExpressionNode Term()
        {
            if(CurrentToken.Type==TokenType.Number)
            {
                double valor=Double.Parse(CurrentToken.Lexeme);
                ConsumeToken();
                return new NumberNode(valor);
            }
            else if (CurrentToken.Type == TokenType.Id)
            {
                string lexem = CurrentToken.Lexeme;
                ConsumeToken();
                return new IdNode(lexem);
            }
            else if (CurrentToken.Type == TokenType.Left_parent)
            {
                ConsumeToken();
                var evalor=Expr();
                if (CurrentToken.Type != TokenType.Right_parent)
                    throw new ParserException("Se esperaba ) ");
                ConsumeToken();
                return evalor;
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
