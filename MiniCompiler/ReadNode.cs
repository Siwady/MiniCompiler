using System;

namespace MiniCompiler
{
    internal class ReadNode : StatementNode
    {
        public ReadNode(IdNode idNode)
        {

            ID_Node = idNode;
        }

        public IdNode ID_Node { get; set; }
        public override void Interpretar()
        {
            double value=Convert.ToDouble(Console.ReadLine());
            Variables.Instance.SetValue(ID_Node.Name,value);
        }

        public override string ToXML()
        {
            return String.Format("<Read>{0}</Read>",ID_Node.ToXML());
        }
    }
}