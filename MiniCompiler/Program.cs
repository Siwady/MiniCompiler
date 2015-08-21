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
            Lexer lex=new Lexer(@"read A;
read B;
C=10;
print (A+B/3)*C/20;");
            /*Token currentToken = lex.GetToken();
            while (currentToken.Type != TokenType.EOF)
            {
                Console.WriteLine(currentToken);
                currentToken = lex.GetToken();
            }
            Console.WriteLine(currentToken);*/
            Parser parser = new Parser(lex);
            string XML = "";
            var par=parser.Parse();
            foreach (var statementNode in par)
            {
                statementNode.Interpretar();
                XML += statementNode.ToXML()+"\n";
            }
            Console.WriteLine(XML);
            Console.ReadKey();
        }
    }
}
