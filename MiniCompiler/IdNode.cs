using System;
using System.Collections.Generic;

namespace MiniCompiler
{
    internal class IdNode : ExpressionNode
    {
        public IdNode(string lexem)
        {
            Name = lexem;
        }

        public string Name { get; set; }
        public override double Evaluate()
        {
            return Variables.Instance.GetValue(Name);
        }

        public override string ToXML()
        {
            return String.Format("<Id>{0}</Id>",Name);
        }
    }

    internal class Variables
    {
        public static Variables Instance {
            get
            {
                if(_instance==null)
                    _instance=new Variables();
                return _instance;
            } }
        private static Variables _instance = null;
        private Dictionary<string, double> _values; 
        private Variables()
        {
            _values=new Dictionary<string, double>();
        }

        public double GetValue(string name)
        {
            return _values[name];
        }

        public void SetValue(string name,double value)
        {
            _values[name] = value;
        }
    }
}