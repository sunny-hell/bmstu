using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_2
{
    class Dispatcher
    {
        private Grammar _grammar;

        public Dispatcher()
        {
            _grammar = new Grammar();
        }

        public void loadGrammar(string fname)
        {
            TextGrammarReader gr = new TextGrammarReader();
            _grammar = gr.read(fname); 
        }

        public string printGrammar()
        {
            return _grammar.ToString();
        }

        public void transformToNonnullableGrammar()
        {
            GrammarTransformer gt = new GrammarTransformer();
            _grammar = gt.transformToNonnullableGrammar(_grammar);
        }

        public void removeERules()
        {
            GrammarTransformer gt = new GrammarTransformer();
            _grammar = gt.removeERules(_grammar);
        }

    }
}
