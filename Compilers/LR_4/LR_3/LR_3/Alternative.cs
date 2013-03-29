using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_4
{
    class Alternative: HistoryItem
    {
        private Nonterminal _base;
        private string _strValue;
        private int _index;
        private List<Symbol> _value;

        public Alternative()
        {
            _base = new Nonterminal();
            _index = 0;
            _value = new List<Symbol>();
            _strValue = "";
        }

        public Alternative(Nonterminal b, int index, List<Symbol> value)
        {
            _base = new Nonterminal(b);
            _index = index;
            _value = new List<Symbol>(value);
            _strValue = String.Format("{0}{1}", _base.Sym, index);
        }

        public Nonterminal Base
        {
            get { return _base; }
        }
        public List<Symbol> Value
        {
            get { return _value; }
        }

        public int Index
        {
            get { return _index; }
        }

        public override string ToString()
        {
            return _strValue;
        }
    }
}
