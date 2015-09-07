using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniCompiler.Semantic.Types;
using Type = MiniCompiler.Semantic.Types.Type;

namespace MiniCompiler.Semantic
{
    public class SymbolTable
    {
        public static SymbolTable Instance {
            get
            {
                if(_instance==null)
                    _instance=new SymbolTable();
                return _instance;
            } }
        private static SymbolTable _instance = null;
        private Dictionary<string, Type> _variables; 
        private SymbolTable()
        {
            _variables=new Dictionary<string, Type>();
        }

        public Type GetVariableType(string name)
        {
            if (!_variables.ContainsKey(name))
                throw new SemanticException("Variable "+name+" doesn't exist");
            return _variables[name];
        }

        public void DeclareVariable(string name,Type value)
        {
            if(_variables.ContainsKey(name))
                throw new SemanticException("Variable " + name + " already exist");
            _variables[name] = value;
        }

    }
}
