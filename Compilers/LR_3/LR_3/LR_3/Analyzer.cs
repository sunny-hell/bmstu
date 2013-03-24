using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_3
{
    abstract class Analyzer
    {
        protected Grammar _grammar;

        public Analyzer(Grammar g)
        {
            _grammar = g;
        }

        abstract public bool analyzeSymbolString(List<Symbol> symbolString);

    }
}
