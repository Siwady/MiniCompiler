using System;
using System.Collections.Generic;
using MiniCompiler.Interpretar.Values;
using MiniCompiler.Semantic;
using Type = MiniCompiler.Semantic.Types.Type;

namespace MiniCompiler
{
    internal class IdNode : ExpressionNode
    {
        public IdNode(string lexem)
        {
            Name = lexem;
        }

        public string Name { get; set; }
       
        public override string ToXML()
        {
            return String.Format("<Id>{0}</Id>",Name);
        }

        public override Type ValidateSemantic()
        {
            return SymbolTable.Instance.GetVariableType(Name);
        }

        public override InterpreteValue Evaluate()
        {
            return SymbolTable.Instance.GetVariableValue(Name);
        }

        public void SetValue(InterpreteValue value)
        {
            SymbolTable.Instance.SetVariableValue(Name,value);
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