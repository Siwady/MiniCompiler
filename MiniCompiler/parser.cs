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

        private List<StatementNode> StatementList()
        {
            if (CurrentToken.Type == TokenType.Id || CurrentToken.Type == TokenType.If || CurrentToken.Type == TokenType.While|| CurrentToken.Type == TokenType.For || CurrentToken.Type == TokenType.print || CurrentToken.Type == TokenType.read)
            {

                var statement =Statement();
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
                //Expr();
                var evalor=Expr();
                
                if (CurrentToken.Type!=TokenType.Eos)
                    throw new ParserException("se esperaba ;");
                
                ConsumeToken();
                return new AssignmentNode(new IdNode(id_lexema),evalor);
                //return Assigment();

            }else if (CurrentToken.Type == TokenType.print)
            {
                ConsumeToken();
                //Expr();
                var evalor=Expr();
                if (CurrentToken.Type != TokenType.Eos)
                    throw new ParserException("se esperaba EOS");

                ConsumeToken();
              return new PrintNode(evalor);
              //return PrintStatement();
            }
            else if (CurrentToken.Type == TokenType.read)
            {
                ConsumeToken();
                if (CurrentToken.Type!=TokenType.Id)
                    throw new ParserException("se esperaba ID");
                string id_lexema = CurrentToken.Lexeme;
                
                ConsumeToken();
                Variable();
                if (CurrentToken.Type != TokenType.Eos)
                    throw new ParserException("se esperaba EOS");

                ConsumeToken();
                return new ReadNode(new IdNode(id_lexema));
            }else if (CurrentToken.Type == TokenType.If)
            {
               return IfStatement();
            }else if (CurrentToken.Type == TokenType.While)
            {
               return  WhileStatement();
            }else if (CurrentToken.Type == TokenType.For)
            {
                return ForStatement();
            }
            else
            {
                throw new ParserException("se esperaba una sentencia");
            }
        }

        private ExpressionNode Expr()
        {
            //Factor();
            //ExprP();
            var fvalor=Factor();
            return ExprP(fvalor);

        }

        private ExpressionNode ExprP(ExpressionNode param)//ExpressionNode param)
        {
            if (CurrentToken.Type == TokenType.Sum)
            {
                ConsumeToken();
                //Factor();
                //ExprP();
                var fvalor=Factor();
                return ExprP(new SumNode(param,fvalor));
            }
            else if (CurrentToken.Type == TokenType.Sub)
            {
                ConsumeToken();
                //Factor();
                //ExprP();
                var fvalor = Factor();
                return ExprP(new SubstractNode(param , fvalor));
            }
            else
            {
                //Epislon
            }
                return  param;
            
        }

        private ExpressionNode Factor()
        {
            var tvalor=Term();
            //Term();
            //FactorP(tvalor);
            return FactorP(tvalor);

        }

        private ExpressionNode FactorP(ExpressionNode param)
        {
            if (CurrentToken.Type == TokenType.Mult)
            {
                ConsumeToken();
                var tvalor =Term();
             
               
                return FactorP(new MultiplicationNode(param,tvalor));
            }
            else if (CurrentToken.Type == TokenType.Div)
            {
                ConsumeToken();
              
                 var tvalor = Term();
                 return FactorP(new DivisionNode(param ,tvalor));
            }
            else
            {
                return param;
            }
            
            
        }

        private ExpressionNode Term()
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
                return Variable();
            }else if (CurrentToken.Type == TokenType.Left_parent)
            {
                ConsumeToken();
                var evalor = Expr();
                if (CurrentToken.Type != TokenType.Right_parent)
                    throw new ParserException("Se esperaba ) ");
                ConsumeToken();
                return evalor;
            }else if (CurrentToken.Type == TokenType.Float_Literal || CurrentToken.Type == TokenType.Int_Literal ||
               CurrentToken.Type == TokenType.True ||
               CurrentToken.Type == TokenType.False || CurrentToken.Type == TokenType.String_Literal)
            {
               return Literal();
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

        private ExpressionNode Literal()
        {
            if (CurrentToken.Type == TokenType.Float_Literal || CurrentToken.Type == TokenType.Int_Literal ||
               CurrentToken.Type == TokenType.True  ||
               CurrentToken.Type == TokenType.False || CurrentToken.Type == TokenType.String_Literal)
            {
                if (CurrentToken.Type == TokenType.Float_Literal)
                {
                    return new LiteralFloatNode(Convert.ToSingle(CurrentToken.Lexeme));
                }
                else if (CurrentToken.Type == TokenType.Int_Literal)
                {
                    return new LiteralIntNode(Convert.ToInt32(CurrentToken.Lexeme));
                }
                ConsumeToken();
                
            }
            else
            {
                throw new ParserException("Se esperaba una Literal");
            }
        }

        private ExpressionNode Variable()
        {
            if (CurrentToken.Type != TokenType.Id)
                throw new ParserException("Se esperaba Id ");
            string id = CurrentToken.Lexeme;
            ConsumeToken();
            ExpressionNode e=VariableP(id);
            if (e != null)
                return e;
            else
                return new SimpleVariable(id);
        }

        private ExpressionNode VariableP(string id)
        {
            if (CurrentToken.Type == TokenType.LeftBracket)
            {
                ConsumeToken();
                ExprList();
                if (CurrentToken.Type != TokenType.RightBracket)
                    throw new ParserException("Se esperaba ] ");
                ConsumeToken();
                VariableP(id);
            }
            else
            {
                //Epsilon
            }
           

        }

        private StatementNode Assigment()
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

        /*private StatementNode PrintStatement()
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
        }*/

        private StatementNode IfStatement()
        {
            if (CurrentToken.Type == TokenType.If)
            {
                ConsumeToken();
                Expr();
                if (CurrentToken.Type != TokenType.Then)
                    throw new ParserException("se esperaba then");
                ConsumeToken();
               var statementList= StatementList();
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

        private StatementNode WhileStatement()
        {
            if (CurrentToken.Type == TokenType.While)
            {
                ConsumeToken();
                ExpressionNode e=Expr();
                if (CurrentToken.Type != TokenType.Do)
                    throw new ParserException("se esperaba Do");
                ConsumeToken();
                List<StatementNode> ls=StatementList();
                if (CurrentToken.Type != TokenType.End)
                    throw new ParserException("se esperaba End");
                ConsumeToken();
                WhileNode wh=new WhileNode(e,ls);
                return wh;
            }
            else
            {
                throw new ParserException("se esperaba WHILE");
            }
        }

        private StatementNode ForStatement()
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

    internal class LiteralIntNode : ExpressionNode
    {
        public LiteralIntNode(int integer)
        {
            Num = integer;
        }

        public int Num { get; set; }
        public override string ToXML()
        {
            throw new NotImplementedException();
        }
    }

    public class LiteralFloatNode : ExpressionNode
    {
        private float _value;
        public LiteralFloatNode(float value)
        {
            this._value = value;
        }

        

        public override string ToXML()
        {
            return _value.ToString();
        }
    }
}
