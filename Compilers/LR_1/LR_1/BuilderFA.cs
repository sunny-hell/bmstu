using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_1
{
    abstract class BuilderFA
    {
        abstract public FiniteAutomate build(string regExpr);
    }
}
