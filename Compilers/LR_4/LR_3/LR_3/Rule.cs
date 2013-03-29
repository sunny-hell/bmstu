using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_4
{
    class Rule
    {
        private Nonterminal _left;
        private List<Symbol> _right;

        public Rule() 
        {
            _left = new Nonterminal();
            _right = new List<Symbol>();
        }
        public Rule(Nonterminal left, List<Symbol> right)
        {
            _left = new Nonterminal(left);
            _right = new List<Symbol>(right);
        }

        public Rule(Rule r)
        {
            _left = new Nonterminal(r.Left);
            _right = new List<Symbol>(r.Right);
        }

        public Nonterminal Left
        {
            get { return _left; }
            set { _left = value; }
        }

        public List<Symbol> Right
        {
            get { return _right; }
            set { _right = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Right.ForEach(s => sb.Append(s.Sym));
            return String.Format("{0} -> {1}\n", _left.Sym, sb.ToString());
        }

        public bool isERule()
        {
            return (Right.Count == 1 && Right.First() is EmptySymbol);
        }

        public bool hasSymbolInRight(Symbol s)
        {
            return Right.Exists(rr => rr.Sym == s.Sym); 
        }
    }
}
