using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_4
{
    abstract class GrammarReader
    {
        abstract public Grammar read(string fname);
    }
}
