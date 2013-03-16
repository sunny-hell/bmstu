using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_2
{
    class Terminal: Symbol
    {
        public Terminal() { }
        public Terminal(string sym)
        {
            _sym = sym;
        }

    }
}
