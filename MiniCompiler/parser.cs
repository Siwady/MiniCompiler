using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Dynamic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using MiniCompiler.Semantic;
using MiniCompiler.Semantic.Types;

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
        private Dictionary<TokenType, Type> _primitivetypes = new Dictionary<TokenType, Type>()
        {
            {TokenType.Float, new FloatType()}, 
            {TokenType.Int, new IntType()},
            {TokenType.String, new StringType()},
            {TokenType.Bool, new BooleanType()}
        };


        public List<StatementNode> Parse()
        {
            DeclarationList();
            
            var statementList=StatementList();
            if (CurrentToken.Type != TokenType.EOF)
            {
                throw new ParserException("se esperaba EOF");
            }
            return statementList;
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
                return Assigment();

            }else if (CurrentToken.Type == TokenType.print)
            {
            
              return PrintStatement();
            }
            else if (CurrentToken.Type == TokenType.read)
            {
                ConsumeToken();
                var var= Variable();
                if (CurrentToken.Type != TokenType.Eos)
                    throw new ParserException("se esperaba EOS");

                ConsumeToken();
                return new ReadNode(var);
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
            {
                return  param;
            }


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
            Type type=VariableType();
            if (CurrentToken.Type != TokenType.Id)
                throw new ParserException("Se esperaba Id ");
            string id = CurrentToken.Lexeme;
            ConsumeToken();
            List<string> identifier=IdentifierList();
            identifier.Insert(0,id);
            if (CurrentToken.Type != TokenType.Eos)
                throw new ParserException("Se esperaba Eos ");
            ConsumeToken();
            foreach (var ide in identifier)
            {
                SymbolTable.Instance.DeclareVariable(ide,type);
            }
        }

        private List<string> IdentifierList()
        {
            if (CurrentToken.Type == TokenType.Comma)
            {
                ConsumeToken();
                if (CurrentToken.Type != TokenType.Id)
                    throw new ParserException("Se esperaba Id ");
                string id = CurrentToken.Lexeme;
                ConsumeToken();

                List<string> identifier=IdentifierList();
                identifier.Insert(0,id);
                return identifier;
            }
            else
            {
                return new List<string>(); //Epislon
            }
        }

        private Type VariableType()
        {
            if (CurrentToken.Type == TokenType.Int || CurrentToken.Type == TokenType.Float ||
                CurrentToken.Type == TokenType.String || CurrentToken.Type == TokenType.Bool)
            {
                return PrimitiveType();
            }else if (CurrentToken.Type == TokenType.Array)
            {
                ConsumeToken();
                if (CurrentToken.Type != TokenType.LeftBracket)
                    throw new ParserException("Se esperaba [ ");
                ConsumeToken();
                List<int> Dimensions=IntList();
                if (CurrentToken.Type != TokenType.RightBracket)
                    throw new ParserException("Se esperaba ] ");
                ConsumeToken();
                if (CurrentToken.Type != TokenType.Of)
                    throw new ParserException("Se esperaba Of ");
                ConsumeToken();
                Type type=VariableType();
                return new ArrayType(type,Dimensions);
            }
            else
            {
                throw new ParserException("Se esperaba Tipo de Dato");
            }
        }

        private List<int> IntList()
        {
            if (CurrentToken.Type == TokenType.Int_Literal)
            {
                int Int = int.Parse(CurrentToken.Lexeme);
                ConsumeToken();
                List<int> int_list=IntListP();
                int_list.Insert(0,Int);
                return int_list;
            }
            else
            {
                throw new ParserException("Se esperaba un literal Entero");
            }
        }

        private List<int> IntListP()
        {
            if (CurrentToken.Type == TokenType.Comma)
            {
                ConsumeToken();
                if(CurrentToken.Type!=TokenType.Int_Literal)
                    throw new ParserException("Se esperaba un entero");
                int Int = int.Parse(CurrentToken.Lexeme);
                ConsumeToken();
                List <int> int_list=IntListP();
                int_list.Insert(0,Int);
                return int_list;
            }
            else
            {
                return new List<int>();//ep
            }
        }

        private Type PrimitiveType()
        {
            if (CurrentToken.Type == TokenType.Bool  ||
                CurrentToken.Type == TokenType.Float || CurrentToken.Type == TokenType.Int ||
                CurrentToken.Type == TokenType.String)
            {
                Type primitivetype=_primitivetypes[CurrentToken.Type];
                ConsumeToken();
                return primitivetype;
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
                var token = CurrentToken;
                ConsumeToken();
                if (token.Type == TokenType.Float_Literal)
                {
                    return new LiteralFloatNode(float.Parse(token.Lexeme));
                }
                if (token.Type == TokenType.Int_Literal)
                {
                    return new LiteralIntNode(int.Parse(token.Lexeme));
                }
                if (token.Type == TokenType.True)
                {
                    return new LiteralTrueNode();
                }
                if (token.Type == TokenType.False)
                {
                    return new LiteralFalseNode();
                }
                else 
                {
                    return new LiteralStringNode(token.Lexeme);
                }
               
                
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
            string idValue = CurrentToken.Lexeme;
            ConsumeToken();
            var arrayNode = VariableP(new IdNode(idValue));
           return  arrayNode ?? new IdNode(idValue) ;
        }

        private ExpressionNode VariableP(ExpressionNode param)
        {
            if (CurrentToken.Type == TokenType.LeftBracket)
            {
                ConsumeToken();
                var expL =ExprList();
                if (CurrentToken.Type != TokenType.RightBracket)
                    throw new ParserException("Se esperaba ] ");
                ConsumeToken();
                return VariableP(new VaribleArrayNode(param,expL));
            }
            else
            {
                return param;
            }
           

        }

        private StatementNode Assigment()
        {
           var variable=  Variable();
            if (CurrentToken.Type != TokenType.Equal)
                throw new ParserException("Se esperaba = ");
            ConsumeToken();
            var expr = Expr();
            if (CurrentToken.Type != TokenType.Eos)
                throw new ParserException("Se esperaba Eos ");
            ConsumeToken();
            return new AssignmentNode(variable,expr);
        }

        private StatementNode PrintStatement()
        {
            if (CurrentToken.Type == TokenType.print)
            {
                ConsumeToken();
               var expr= Expr();
                if (CurrentToken.Type != TokenType.Eos)
                    throw new ParserException("se esperaba EOS");

                ConsumeToken();
                return new PrintNode(expr);
            }
            else
            {
                throw new ParserException("se esperaba Print");
            }
        }

        private StatementNode IfStatement()
        {
            if (CurrentToken.Type == TokenType.If)
            {
                ConsumeToken();
               var expr = Expr();
                if (CurrentToken.Type != TokenType.Then)
                    throw new ParserException("se esperaba then");
                ConsumeToken();
               var statementList= StatementList();
               var elseStatement= IfStatementP();
                return new IfNodeStatement(expr, statementList, elseStatement);
            }
            else
            {
                throw new ParserException("se esperaba IF");
            }
        }

        private List<StatementNode> IfStatementP()
        {
            if (CurrentToken.Type == TokenType.End)
            {
                ConsumeToken();
                return null;
            }else if (CurrentToken.Type == TokenType.Else)
            {
                ConsumeToken();
               var statementList= StatementList();
                if (CurrentToken.Type != TokenType.End)
                    throw new ParserException("se esperaba End");
                ConsumeToken();
                return StatementList();
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
                var expr =Expr();
                if (CurrentToken.Type != TokenType.Do)
                    throw new ParserException("se esperaba Do");
                ConsumeToken();
                var list =StatementList();
                if (CurrentToken.Type != TokenType.End)
                    throw new ParserException("se esperaba End");
                ConsumeToken();
                return new WhileNodoStatement(expr, list);
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
                var variable =Variable();
                if (CurrentToken.Type != TokenType.Equal)
                    throw new ParserException("se esperaba =");
                ConsumeToken();
               var expr= Expr();
                if (CurrentToken.Type != TokenType.To)
                    throw new ParserException("se esperaba To");
                ConsumeToken();
               var expr2= Expr();
               var list= StatementList();
                if (CurrentToken.Type != TokenType.End)
                    throw new ParserException("se esperaba end");
                ConsumeToken();
                return new ForStatementNode(variable, expr, expr2, list);
            }
            else
            {
                throw new ParserException("se esperaba For");
            }
        }
        private List<ExpressionNode> ExprList()
        {
            var exp =Expr();
            List<ExpressionNode> exprL =  ExprListP();
            exprL.Insert(0,exp);
            return exprL;
        }

        private List<ExpressionNode> ExprListP()
        {
            if (CurrentToken.Type == TokenType.Comma)
            {
                ConsumeToken();
                var exp =Expr();
                List<ExpressionNode> exprL= ExprListP();
                return exprL;
            }
            else
            {
                return new List<ExpressionNode>();
            }
        }

        private void ConsumeToken()
        {
            CurrentToken = Lex.GetToken();
        }

        
    }

    public class VaribleArrayNode : ExpressionNode
    {
        public VaribleArrayNode(ExpressionNode expressionNode, List<ExpressionNode> expL)
        {
            ExpressionN = expressionNode;
            ExprL = expL;
        }

        public List<ExpressionNode> ExprL { get; set; }

        public ExpressionNode ExpressionN { get; set; }

        public override string ToXML()
        {
            throw new System.NotImplementedException();
        }

        public override Type ValidateSemantic()
        {
            throw new System.NotImplementedException();
        }
    }
}
