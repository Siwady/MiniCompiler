using System;

namespace MiniCompiler
{
    internal class SimpleVariable : ExpressionNode
    {
        public SimpleVariable(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public override string ToXML()
        {
            throw new NotImplementedException();
        }
    }
}