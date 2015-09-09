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
array[10] of array[10] of int arreglo2;
bool e;
string j;
if false then
    j=""10""; 
end

while true do
    read a;
end

for x=3 to 5
    
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
            Console.WriteLine(XML);
            Console.ReadKey();
        }
    }
}
