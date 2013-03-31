using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_4
{
    abstract class Symbol: HistoryItem
    {
        protected string _sym;

        
        public string Sym
        {
            set { _sym = value; }
            get { return _sym; }
        }

        public override string ToString()
        {
            return _sym;
        }

        
    }
}
