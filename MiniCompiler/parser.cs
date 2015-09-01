using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Dynamic;
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

        public void Parse()
        {
            DeclarationList();
            StatementList();
            //var statement_list=StatementList();
            if (CurrentToken.Type != TokenType.EOF)
            {
                throw new ParserException("se esperaba EOF");
            }
            //return statement_list;
        }

        private void StatementList()
        {
            if (CurrentToken.Type == TokenType.Id || CurrentToken.Type == TokenType.If || CurrentToken.Type == TokenType.While|| CurrentToken.Type == TokenType.For || CurrentToken.Type == TokenType.print || CurrentToken.Type == TokenType.read)
            {
                Statement();
                StatementList();
               // var statement=Statement();
                //var statement_list=StatementList();
               // statement_list.Insert(0,statement);
                //return statement_list;
            }
            else
            {
                //return new List<StatementNode>();
            }
        }

        private void Statement()
        {
            if (CurrentToken.Type==TokenType.Id)
            {
               /* string id_lexema = CurrentToken.Lexeme;
                ConsumeToken();
                if (CurrentToken.Type != TokenType.Equal)
                    throw new ParserException("se esperaba =");
                
                ConsumeToken();
                Expr();
                //var evalor=Expr();
                
                if (CurrentToken.Type!=TokenType.Eos)
                    throw new ParserException("se esperaba ;");
                
                ConsumeToken();
                //return new AssignmentNode(new IdNode(id_lexema),evalor);*/
                Assigment();

            }else if (CurrentToken.Type == TokenType.print)
            {
               /* ConsumeToken();
                Expr();
                //var evalor=Expr();
                if (CurrentToken.Type != TokenType.Eos)
                    throw new ParserException("se esperaba EOS");

                ConsumeToken();
              //  return new PrintNode(evalor);*/
              PrintStatement();
            }
            else if (CurrentToken.Type == TokenType.read)
            {
                ConsumeToken();
               /* if (CurrentToken.Type!=TokenType.Id)
                    throw new ParserException("se esperaba ID");
                string id_lexema = CurrentToken.Lexeme;
                
                ConsumeToken*/
                Variable();
                if (CurrentToken.Type != TokenType.Eos)
                    throw new ParserException("se esperaba EOS");

                ConsumeToken();
               // return new ReadNode(new IdNode(id_lexema));
            }else if (CurrentToken.Type == TokenType.If)
            {
                IfStatement();
            }else if (CurrentToken.Type == TokenType.While)
            {
                WhileStatement();
            }else if (CurrentToken.Type == TokenType.For)
            {
                ForStatement();
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
            //var fvalor=Factor();
            //return ExprP(fvalor);

        }

        private void ExprP()//ExpressionNode param)
        {
            if (CurrentToken.Type == TokenType.Sum)
            {
                ConsumeToken();
                Factor();
                ExprP();
                // var fvalor=Factor();
                //  return ExprP(new SumNode(param,fvalor));
            }
            else if (CurrentToken.Type == TokenType.Sub)
            {
                ConsumeToken();
                Factor();
                ExprP();
                // var fvalor = Factor();
                // return ExprP(new SubstractNode(param , fvalor));
            }
            else
            {
                //Epislon
            }
                //return  param;
            
        }

        private void Factor()
        {
          //  var tvalor=Term();
            Term();
            FactorP();
            //return FactorP(tvalor);

        }

        private void FactorP()//ExpressionNode param)
        {
            if (CurrentToken.Type == TokenType.Mult)
            {
                ConsumeToken();
               // var tvalor=Term();
               Term();
               FactorP();
                //return FactorP(new MultiplicationNode(param,tvalor));
            }
            else if (CurrentToken.Type == TokenType.Div)
            {
                ConsumeToken();
                Term();
                FactorP();
                // var tvalor = Term();
                // return FactorP(new DivisionNode(param ,tvalor));
            }
            else
            {
              //epsilon  
            }
            //    return param;
            
        }

        private void Term()
        {
           /* if(CurrentToken.Type==TokenType.Number)
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
            }*/
            if (CurrentToken.Type == TokenType.Id )
            {
                Variable();
            }else if (CurrentToken.Type == TokenType.Left_parent)
            {
                ConsumeToken();
                Expr();
                if (CurrentToken.Type != TokenType.Right_parent)
                    throw new ParserException("Se esperaba ) ");
                ConsumeToken();
            }else if (CurrentToken.Type == TokenType.Float_Literal || CurrentToken.Type == TokenType.Int_Literal ||
               CurrentToken.Type == TokenType.True ||
               CurrentToken.Type == TokenType.False || CurrentToken.Type == TokenType.String_Literal)
            {
                Literal();
            }
            else
            {
                throw  new ParserException("Se esperaba termino");
            }
        }

        private void DeclarationList()
        {
            if (CurrentToken.Type == TokenType.Int || CurrentToken.Type == TokenType.Float ||
                CurrentToken.Type == TokenType.String || CurrentToken.Type == TokenType.Bool ||
                CurrentToken.Type == TokenType.Array)
            {
                Declaration();
                DeclarationList();
            }
            else
            {
                //Epsilon
            }
        }
        private void Declaration()
        {
            VariableType();
            if (CurrentToken.Type != TokenType.Id)
                throw new ParserException("Se esperaba Id ");
            ConsumeToken();
            IdentifierList();
            if (CurrentToken.Type != TokenType.Eos)
                throw new ParserException("Se esperaba Eos ");
            ConsumeToken();
        }

        private void IdentifierList()
        {
            if (CurrentToken.Type == TokenType.Comma)
            {
                ConsumeToken();
                if (CurrentToken.Type != TokenType.Id)
                    throw new ParserException("Se esperaba Id ");
                ConsumeToken();
                IdentifierList();
            }
            else
            {
                //Epislon
            }
        }

        private void VariableType()
        {
            if (CurrentToken.Type == TokenType.Int || CurrentToken.Type == TokenType.Float ||
                CurrentToken.Type == TokenType.String || CurrentToken.Type == TokenType.Bool)
            {
                PrimitiveType();
            }else if (CurrentToken.Type == TokenType.Array)
            {
                ConsumeToken();
                if (CurrentToken.Type != TokenType.LeftBracket)
                    throw new ParserException("Se esperaba [ ");
                ConsumeToken();
                IntList();
                if (CurrentToken.Type != TokenType.RightBracket)
                    throw new ParserException("Se esperaba ] ");
                ConsumeToken();
                if (CurrentToken.Type != TokenType.Of)
                    throw new ParserException("Se esperaba Of ");
                ConsumeToken();
                VariableType();
            }
            else
            {
                throw new ParserException("Se esperaba Tipo de Dato");
            }
        }

        private void IntList()
        {
            if (CurrentToken.Type == TokenType.Int_Literal)
            {
                ConsumeToken();
                IntListP();
            }
            else
            {
                throw new ParserException("Se esperaba un literal Entero");
            }
        }

        private void IntListP()
        {
            if (CurrentToken.Type == TokenType.Comma)
            {
                ConsumeToken();
                IntListP();

            }
            else
            {
                //
            }
        }

        private void PrimitiveType()
        {
            if (CurrentToken.Type == TokenType.Bool  ||
                CurrentToken.Type == TokenType.Float || CurrentToken.Type == TokenType.Int ||
                CurrentToken.Type == TokenType.String)
            {
                ConsumeToken();
            }
            else
            {
                throw new ParserException("Se esperaba Tipo de Dato");
            }
        }

        private void Literal()
        {
            if (CurrentToken.Type == TokenType.Float_Literal || CurrentToken.Type == TokenType.Int_Literal ||
               CurrentToken.Type == TokenType.True  ||
               CurrentToken.Type == TokenType.False || CurrentToken.Type == TokenType.String_Literal)
            {
                ConsumeToken();
            }
            else
            {
                throw new ParserException("Se esperaba una Literal");
            }
        }

        private void Variable()
        {
            if (CurrentToken.Type != TokenType.Id)
                throw new ParserException("Se esperaba Id ");
            ConsumeToken();
            VariableP();
        }

        private void VariableP()
        {
            if (CurrentToken.Type == TokenType.LeftBracket)
            {
                ConsumeToken();
                ExprList();
                if (CurrentToken.Type != TokenType.RightBracket)
                    throw new ParserException("Se esperaba ] ");
                ConsumeToken();
                VariableP();
            }
            else
            {
                //Epsilon
            }
           

        }

        private void Assigment()
        {
            Variable();
            if (CurrentToken.Type != TokenType.Equal)
                throw new ParserException("Se esperaba = ");
            ConsumeToken();
            Expr();
            if (CurrentToken.Type != TokenType.Eos)
                throw new ParserException("Se esperaba Eos ");
            ConsumeToken();

        }

        private void PrintStatement()
        {
            if (CurrentToken.Type == TokenType.print)
            {
                ConsumeToken();
                Expr();
                if (CurrentToken.Type != TokenType.Eos)
                    throw new ParserException("se esperaba EOS");

                ConsumeToken();

            }
            else
            {
                throw new ParserException("se esperaba Print");
            }
        }

        private void IfStatement()
        {
            if (CurrentToken.Type == TokenType.If)
            {
                ConsumeToken();
                Expr();
                if (CurrentToken.Type != TokenType.Then)
                    throw new ParserException("se esperaba then");
                ConsumeToken();
                StatementList();
                IfStatementP();
            }
            else
            {
                throw new ParserException("se esperaba IF");
            }
        }

        private void IfStatementP()
        {
            if (CurrentToken.Type == TokenType.End)
            {
                ConsumeToken();
            }else if (CurrentToken.Type == TokenType.Else)
            {
                ConsumeToken();
                StatementList();
                if (CurrentToken.Type != TokenType.End)
                    throw new ParserException("se esperaba End");
                ConsumeToken();
            }
            else
            {
                throw new ParserException("se esperaba End o Else");
            }
        }

        private void WhileStatement()
        {
            if (CurrentToken.Type == TokenType.While)
            {
                ConsumeToken();
                Expr();
                if (CurrentToken.Type != TokenType.Do)
                    throw new ParserException("se esperaba Do");
                ConsumeToken();
                StatementList();
                if (CurrentToken.Type != TokenType.End)
                    throw new ParserException("se esperaba End");
                ConsumeToken();
            }
            else
            {
                throw new ParserException("se esperaba WHILE");
            }
        }

        private void ForStatement()
        {
            if (CurrentToken.Type == TokenType.For)
            {
                ConsumeToken();
                Variable();
                if (CurrentToken.Type != TokenType.Equal)
                    throw new ParserException("se esperaba =");
                ConsumeToken();
                Expr();
                if (CurrentToken.Type != TokenType.To)
                    throw new ParserException("se esperaba To");
                ConsumeToken();
                Expr();
                StatementList();
                if (CurrentToken.Type != TokenType.End)
                    throw new ParserException("se esperaba end");
                ConsumeToken();
            }
            else
            {
                throw new ParserException("se esperaba For");
            }
        }
        private void ExprList()
        {
            Expr();
            ExprListP();
        }

        private void ExprListP()
        {
            if (CurrentToken.Type == TokenType.Comma)
            {
                ConsumeToken();
                Expr();
                ExprListP();
            }
            else
            {
                //
            }
        }

        private void ConsumeToken()
        {
            CurrentToken = Lex.GetToken();
        }

        
    }
}
