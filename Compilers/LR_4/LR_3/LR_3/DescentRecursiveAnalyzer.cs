using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_4
{
    class DescentRecursiveAnalyzer: Analyzer
    {
        private StringBuilder _history;

        public DescentRecursiveAnalyzer()   {
            _grammar = new Grammar
            {
                Nonterminals = new List<Nonterminal> { new Nonterminal("S"), new Nonterminal("A"), new Nonterminal("B") },
                Terminals = new List<Terminal> { new Terminal("true"), new Terminal("false"), new Terminal("&"), new Terminal("!"), new Terminal("~"), new Terminal("a") },
                Rules = new List<Rule> 
                { 
                    new Rule { Left = new Nonterminal("S"), Right = new List<Symbol>{ new Terminal("true"), new Nonterminal("A") } },   // R1
                    new Rule { Left = new Nonterminal("S"), Right = new List<Symbol>{ new Terminal("false"), new Nonterminal("A") } },  // R2
                    new Rule { Left = new Nonterminal("S"), Right = new List<Symbol>{ new Terminal("a"), new Nonterminal("A") } },      // R3
                    new Rule { Left = new Nonterminal("S"), Right = new List<Symbol>{ new Terminal("~"), new Nonterminal("B") } },      // R4
                    new Rule { Left = new Nonterminal("A"), Right = new List<Symbol>{ new Terminal("&"), new Nonterminal("S") } },      // R5
                    new Rule { Left = new Nonterminal("A"), Right = new List<Symbol>{ new Terminal("!"), new Nonterminal("S") } },      // R6
                    new Rule { Left = new Nonterminal("A"), Right = new List<Symbol>{ new EmptySymbol()} },                             // R7
                    new Rule { Left = new Nonterminal("B"), Right = new List<Symbol>{new Terminal("true"), new Nonterminal("A") } },    // R8
                    new Rule { Left = new Nonterminal("B"), Right = new List<Symbol>{new Terminal("false"), new Nonterminal("A") } },   // R9
                    new Rule { Left = new Nonterminal("B"), Right = new List<Symbol>{new Terminal("a"), new Nonterminal("A") } }        // R10
                },
             DistinguishedSymbol = new Nonterminal("S")
            };
            _history = new StringBuilder();
        }

        private bool processS(List<Symbol> symbolString, int i)
        {
            if (i == symbolString.Count)
                return false;
            switch (symbolString[i].Sym)
            {
                case "true": 
                    _history.Append("R1 ");
                    return processA(symbolString, i + 1);
                case "false":
                    _history.Append("R2 ");
                    return processA(symbolString, i + 1);
                case "a":
                    _history.Append("R3 ");
                    return processA(symbolString, i + 1);
                case "~":
                    _history.Append("R4 ");
                    return processB(symbolString, i + 1);
                default:
                    return false;
            }

        }
        private bool processA(List<Symbol> symbolString, int i)
        {
            if (i == symbolString.Count)
                return true;

            switch (symbolString[i].Sym)
            {
                case "&":
                    _history.Append("R5 ");
                    return processS(symbolString, i + 1);
                case "!":
                    _history.Append("R6 ");
                    return processS(symbolString, i + 1);
                default:
                    return false;
            }
        }
        
        private bool processB(List<Symbol> symbolString, int i)
        {
            if (i == symbolString.Count)
                return false;
            switch (symbolString[i].Sym)
            {
                case "true":
                    _history.Append("R8 ");
                    return processA(symbolString, i + 1);
                case "false":
                    _history.Append("R9 ");
                    return processA(symbolString, i + 1);
                case "a":
                    _history.Append("R10 ");
                    return processA(symbolString, i + 1);
                default:
                    return false;
            }
        }

        public override bool analyzeSymbolString(List<Symbol> symbolString)
        {
            _history = new StringBuilder();
            return processS(symbolString, 0);
        }

        public override StringBuilder getHistory()
        {
            return _history;
        }
    }
}
