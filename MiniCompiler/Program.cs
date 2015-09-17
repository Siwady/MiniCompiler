using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniCompiler.Semantic;

namespace MiniCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Lexer lex=new Lexer(@"int a,b,c;
float d;
string a2;
array[10,10] of int arreglo;
bool e;
string j;

for a=3 to 5
    print a;
end");
            /*Token currentToken = lex.GetToken();
            while (currentToken.Type != TokenType.EOF)
            {
                Console.WriteLine(currentToken);
                currentToken = lex.GetToken();
            }
            Console.WriteLine(currentToken);*/
            Parser parser = new Parser(lex);
            string XML = "";
            var par =parser.Parse();
            
            var table = SymbolTable.Instance;
            Console.WriteLine("Works!");
            foreach (var statementNode in par)
            {
                statementNode.ValidateSemantic();
                
            }
            foreach (var statementNode in par)
            {
                statementNode.Interpret();

            }
            Console.WriteLine(XML);
            Console.ReadKey();
        }
    }
}
