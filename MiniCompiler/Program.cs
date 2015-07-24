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
            Lexer lex=new Lexer(@"12
+  123");
            Token currentToken = lex.GetToken();
            while (currentToken.Type != TokenType.EOF)
            {
                Console.WriteLine(currentToken);
                currentToken = lex.GetToken();
            }
            Console.WriteLine(currentToken);
            Console.ReadKey();
        }
    }
}
