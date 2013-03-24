using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_3
{
    class Grammar
    {
        private List<Nonterminal> _nonterminals;
        private List<Terminal> _terminals;
        private Nonterminal _distinguishedSymbol;
        private List<Rule> _rules;
        private List<Nonterminal> _vanishingSymbols;

        public List<Nonterminal> Nonterminals
        {
            get { return _nonterminals; }
            set { _nonterminals = value; }
        }

        public List<Terminal> Terminals
        {
            get { return _terminals; }
            set { _terminals = value; }
        }

        public List<Rule> Rules
        {
            get { return _rules; }
            set { _rules = value; }
        }

        public Nonterminal DistinguishedSymbol
        {
            get { return _distinguishedSymbol; }
            set { _distinguishedSymbol = value;  }
        }

        public List<Nonterminal> VanishingSymbols
        {
            get { return _vanishingSymbols; }
        }
        public Grammar()
        {
            _nonterminals = new List<Nonterminal>();
            _terminals = new List<Terminal>();
            _rules = new List<Rule>();
            _vanishingSymbols = new List<Nonterminal>();
        }

        public Grammar(List<Nonterminal> nonterminals, List<Terminal> terminals, List<Rule> rules, Nonterminal distinguishedSymbol)
        {
            _nonterminals = new List<Nonterminal>(nonterminals);
            _terminals = new List<Terminal>(terminals);
            _rules = new List<Rule>(rules);
            _distinguishedSymbol = distinguishedSymbol;
            //findVanishingSymbols();
        }

        public Grammar(Grammar g)
        {
            _nonterminals = new List<Nonterminal>(g.Nonterminals);
            _terminals = new List<Terminal>(g.Terminals);
            _rules = new List<Rule>(g.Rules);
            _distinguishedSymbol = new Nonterminal(g.DistinguishedSymbol);
            _vanishingSymbols = new List<Nonterminal>(g.VanishingSymbols);
 
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Нетерминалы");
            Nonterminals.ForEach(nt => sb.AppendLine(nt.Sym));
            sb.AppendLine("Терминалы");
            Terminals.ForEach(t => sb.AppendLine(t.Sym));
            sb.AppendLine("Правила");
            Rules.ForEach(r => sb.Append(r.ToString()));
            sb.AppendLine("Начальный символ грамматики");
            sb.AppendLine(DistinguishedSymbol.Sym);
            return sb.ToString();
        }

        public bool isVanishingSymbol(Symbol s)
        {
            return _vanishingSymbols.Exists(vs => vs.Sym == s.Sym);
        }

        public bool isDistinguishedSymbol(Symbol s)
        {
            return (s is Nonterminal && s.Sym == DistinguishedSymbol.Sym);
        }

        public void findVanishingSymbols()
        {
            List<Nonterminal> set = new List<Nonterminal>();
            List<Nonterminal> oldSet = new List<Nonterminal>();
            _vanishingSymbols = new List<Nonterminal>();
            do
            {
                _vanishingSymbols.AddRange(set);
                oldSet = new List<Nonterminal>(set);
                set.Clear();
                foreach (Rule r in _rules)
                    if (!isVanishingSymbol(r.Left))
                    { // слева стоит нетерминал, не вошедший в мно-во исчезающих
                        bool isVanishing = true;
                        if (r.Right.All(s => s is Terminal))
                            // справа цепочка терминальных символов
                            isVanishing = false;
                        else
                            if (!r.isERule())
                                foreach (Symbol s in r.Right)
                                    if ((s is Nonterminal && !oldSet.Exists(vs => vs.Sym == s.Sym)) || s is Terminal)
                                    {
                                        isVanishing = false;
                                        break;
                                    }

                        if (isVanishing)
                            set.Add(r.Left);
                    }
                
            } while (!set.SequenceEqual(oldSet));
        }

        public void removeUnreachableSymbols()
        {
            List<Symbol> unreachable = new List<Symbol>();
            foreach (Nonterminal nt in Nonterminals)
                if (!Rules.Exists(r => r.hasSymbolInRight(nt)) && !isDistinguishedSymbol(nt))
                    unreachable.Add(nt);
            foreach (Nonterminal unr in unreachable)
            {
                Rules.RemoveAll(r => r.Left.Sym == unr.Sym);
                Nonterminals.Remove(unr);
            }
        }

        public List<Symbol> verifyString(String s)
        {
            List<Symbol> symbolString = new List<Symbol>();
            string[] items = s.Split(' ');
            foreach (string item in items)
            {
                bool isCorrect = false;
                foreach (Terminal t in Terminals)
                    if (t.Sym == item)
                    {
                        symbolString.Add(new Terminal(item));
                        isCorrect = true;
                        break;
                    }
                 if (!isCorrect)
                    throw new BadArgumentException("Некорретно задана входная цепочка");
            }
            return symbolString;
        }
    }
}
