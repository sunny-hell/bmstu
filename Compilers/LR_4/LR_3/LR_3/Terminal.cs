﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_4
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
