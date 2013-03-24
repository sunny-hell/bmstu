using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LR_3
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

        public bool analyze(String s)
        {
            List<Symbol> symbolInput = _grammar.verifyString(s);
            DescendingBacktrackAnalyzer analyzer = new DescendingBacktrackAnalyzer(_grammar);
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
           
    }
}
