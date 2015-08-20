namespace MiniCompiler
{
    internal class IdNode : ExpressionNode
    {
        public IdNode(string lexem)
        {
            Name = lexem;
        }

        public string Name { get; set; }
    }
}