using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LR_4
{
    class Dispatcher
    {
        private Grammar _grammar;
        private StringBuilder _history;
        
        public void loadGrammar(string fileName)
        {
            TextGrammarReader reader = new TextGrammarReader();
            _grammar = reader.read(fileName);
        }

        public string printGrammar()
        {
            if (_grammar == null)
                return "";
            return _grammar.ToString();
        }

        public bool analyze(String s, Analyzer analyzer)
        {
            if (analyzer is DescentRecursiveAnalyzer)
                _grammar = analyzer.getGrammar();
                       
            List<Symbol> symbolInput = _grammar.verifyString(s);
            bool result = analyzer.analyzeSymbolString(symbolInput);

            _history = analyzer.getHistory();
            return result;
        }

        
        public void saveHistory(string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName);
            writer.Write(_history.ToString());
            writer.Close();
        }

        public string getHistory()
        {
            return _history.ToString();
        }

        public Grammar getGrammar()
        {
            return _grammar;
        }

        public void setGrammar(Grammar g)
        {
            _grammar = new Grammar(g);
        }
    }
}
