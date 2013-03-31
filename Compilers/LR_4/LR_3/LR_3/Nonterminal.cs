using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_4
{
    class Nonterminal: Symbol
    {
        public Nonterminal() { }
        public Nonterminal(string sym)
        {
            _sym = sym;
        }

        public Nonterminal(Nonterminal nt)
        {
            _sym = nt.Sym;
        }
    }
}
