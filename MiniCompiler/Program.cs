using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Lexer lex=new Lexer(@"int a,b,c;
float d;
bool e;
for  while end to  if then else  array[10]  do");
            Token currentToken = lex.GetToken();
            while (currentToken.Type != TokenType.EOF)
            {
                Console.WriteLine(currentToken);
                currentToken = lex.GetToken();
            }
            Console.WriteLine(currentToken);
            /*Parser parser = new Parser(lex);
            string XML = "";
            var par=parser.Parse();
            foreach (var statementNode in par)
            {
                statementNode.Interpretar();
                XML += statementNode.ToXML()+"\n";
            }
            Console.WriteLine(XML);*/
            Console.ReadKey();
        }
    }
}
