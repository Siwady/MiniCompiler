namespace MiniCompiler.Interpretar.Values
{
    public class BooleanValue: InterpreteValue
    {
        public bool Value { set; get; }

        public BooleanValue(bool value)
        {
            Value = value;
        }

        public override string TU_CADENA()
        {
            return Value.ToString();
        }
    }
}