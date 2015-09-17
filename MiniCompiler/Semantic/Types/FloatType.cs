﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniCompiler.Interpretar.Values;

namespace MiniCompiler.Semantic.Types
{
    public class FloatType:Type 
    {
        public override InterpreteValue GetDefaultValue()
        {
            return new FloatValue(0);
        }
    }
}
