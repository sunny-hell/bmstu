using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_2
{
    class GrammarTransformer
    {
        private Dictionary<string, Nonterminal> _nonnullableNonterminalsMap;
        private Dictionary<string, List<Symbol>> _nonterminalSymbolStringMap;
        private List<Nonterminal> _nonterminalsForGrammarWithoutERules;
        
        private List<Nonterminal> bulidNonterminalsForNonnullableGrammar(Grammar g)
        {
            _nonnullableNonterminalsMap = new Dictionary<string, Nonterminal>();
            List<Nonterminal> N = new List<Nonterminal>(g.Nonterminals);
            foreach (Nonterminal vs in g.VanishingSymbols)
            {
                Nonterminal newNonterminal = new Nonterminal(String.Format("{0}*", vs.Sym));
                N.Add(newNonterminal);
                _nonnullableNonterminalsMap.Add(vs.Sym, newNonterminal);
            }
            return N;
        }

        private Nonterminal distinguishedSymbolForNonnullableGrammar(Grammar g)
        {
            Nonterminal S;
            if (g.isVanishingSymbol(g.DistinguishedSymbol))
                S = _nonnullableNonterminalsMap[g.DistinguishedSymbol.Sym];
            else
                S = g.DistinguishedSymbol;
            return S;

        }

        private void breakRuleForNonnullableGrammar(Rule r, Grammar g, out List<Nonterminal> firstVanishing, out List<Symbol> rest)
        {
            firstVanishing = new List<Nonterminal>();
            rest = new List<Symbol>();
            foreach (Symbol s in r.Right)
            {
                if (rest.Count > 0)
                    rest.Add(s);
                else
                    if (g.isVanishingSymbol(s))
                        firstVanishing.Add((Nonterminal)s);
                    else
                        rest.Add(s);
            }
        }
        private List<Rule> createNewRulesForFisrtVanishing(Rule rule, List<Nonterminal> firstVanishing, List<Symbol> rest)
        {
            List<Rule> rules= new List<Rule>();
            List<Nonterminal> firstPart = new List<Nonterminal>(firstVanishing);
            
            foreach (Nonterminal nt in firstVanishing)
            {
                firstPart[0] = _nonnullableNonterminalsMap[firstPart.First().Sym];
                Rule r = new Rule();
                r.Left = new Nonterminal(rule.Left);
                r.Right.AddRange(firstPart);
                r.Right.Concat(rest);
                rules.Add(r);
                firstPart.RemoveAt(0);
            }
            return rules;

        }

        private Rule createRuleWithNoFisrtVanishing(Rule rule, List<Symbol> rest)
        {
            Rule r = new Rule();
            r.Left = new Nonterminal(rule.Left);
            r.Right.AddRange(rest);
            return r;
        }

        private List<Rule> createRulesForLeftVanishing(List<Rule> curRules, Nonterminal left)
        {
            List<Rule> newRules = new List<Rule>();
            foreach (Rule r in curRules)
            {
                Rule newRule = new Rule(r);
                newRule.Left = _nonnullableNonterminalsMap[left.Sym];
                newRules.Add(newRule);
            }
            return newRules;
        }
        private List<Rule> buildRulesForNonnullableGrammar(Grammar g)
        {
            List<Rule> rules = new List<Rule>();
            List<Rule> newRules = new List<Rule>();
            foreach (Rule r in g.Rules)
            {
                if (r.isERule())
                    rules.Add(r);
                else
                {
                    newRules.Clear();
                    List<Nonterminal> firstVanishing;
                    List<Symbol> rest;
                    breakRuleForNonnullableGrammar(r, g, out firstVanishing, out rest);
                    if (firstVanishing.Count > 0)
                        newRules.AddRange(createNewRulesForFisrtVanishing(r, firstVanishing, rest));
                    if (rest.Count > 0)
                        newRules.Add(createRuleWithNoFisrtVanishing(r, rest));
                    if (g.isVanishingSymbol(r.Left))
                        newRules.AddRange(createRulesForLeftVanishing(newRules, r.Left));
                    rules.AddRange(newRules);
                }

            }
            return rules;
        }


       
        public Grammar transformToNonnullableGrammar(Grammar g)
        {
            List<Nonterminal> nonterminals = bulidNonterminalsForNonnullableGrammar(g);
            Nonterminal distinguishedSymbol = distinguishedSymbolForNonnullableGrammar(g);
            List<Rule> rules = buildRulesForNonnullableGrammar(g);
            Grammar g1 = new Grammar(nonterminals, g.Terminals, rules, distinguishedSymbol);
            g1.removeUnreachableSymbols();
            g1.findVanishingSymbols();
            return g1;
        }

        private Grammar removeNonterminalProdusesOnlyERules(Grammar g)
        {
            Grammar newG = new Grammar(g);
            List<Nonterminal> toBeRemoved = new List<Nonterminal>();
            foreach (Rule r in newG.Rules)
                if (r.isERule() && !newG.Rules.Exists(rule => rule.Left.Sym == r.Left.Sym))
                    toBeRemoved.Add(r.Left);
            foreach (Rule r in newG.Rules)
                r.Right.RemoveAll(s => toBeRemoved.Exists(A => A.Sym == s.Sym));
            newG.Rules.RemoveAll(r => toBeRemoved.Exists(A => A.Sym == r.Left.Sym));
            newG.Nonterminals.RemoveAll(nt => toBeRemoved.Exists(A => A.Sym == nt.Sym));
            return newG;
        }

        private Nonterminal symbolStringToNonterminal(List<Symbol> symStr)
        {
            StringBuilder sb = new StringBuilder();
            symStr.ForEach(s => sb.Append(s.Sym));
            string newSym = String.Format("[{0}]", sb.ToString());
            if (!_nonterminalSymbolStringMap.ContainsKey(newSym))
                _nonterminalSymbolStringMap.Add(newSym, symStr);
            return new Nonterminal(newSym);
        }

        private List<Symbol> nonterminalToSymbolString(Nonterminal nt)
        {
            return _nonterminalSymbolStringMap[nt.Sym];

        }

        private List<Symbol> buildRightPartOfNewRule(List<Symbol> firstTerminals, List<Symbol> restOfString, Rule r)
        {
            List<Symbol> rightPart = new List<Symbol>(firstTerminals);
            List<Symbol> symStrForNewNonterminal = new List<Symbol>(r.Right);
            Nonterminal newNonterminal;
            symStrForNewNonterminal.AddRange(restOfString);
            newNonterminal = symbolStringToNonterminal(symStrForNewNonterminal);
            if (!_nonterminalsForGrammarWithoutERules.Exists(A => A.Sym == newNonterminal.Sym))
                _nonterminalsForGrammarWithoutERules.Add(newNonterminal);
            rightPart.Add(newNonterminal);
            return rightPart;
        }

        private List<Rule> buildRulesForNonterminal(Nonterminal nonterminal, Grammar g)
        {
            List<Rule> newRules = new List<Rule>();
            List<Rule> rules = new List<Rule>();
            List<Symbol> symStr = nonterminalToSymbolString(nonterminal);
            List<Symbol> firstTerminals = symStr.TakeWhile(ss => ss is Terminal).ToList();
            foreach (Symbol nt in symStr)
                if (nt is Nonterminal)
                {
                    int ind = symStr.IndexOf(nt);
                    List<Symbol> restOfString = symStr.GetRange(ind + 1, symStr.Count - ind - 1);
                    foreach (Rule r in g.Rules)
                    {
                        if (r.Left.Sym == nt.Sym && !r.isERule())
                        {
                            Rule newRule = new Rule();
                            newRule.Left = new Nonterminal(nonterminal);
                            newRule.Right = buildRightPartOfNewRule(firstTerminals, restOfString, r);
                            newRules.Add(newRule);
                        }
                    }
                    if (ind == 0)
                        break;
                }
            if (firstTerminals.Count != 0)
                newRules.Add(new Rule((Nonterminal)nonterminal, firstTerminals));
            return newRules;
        }


        public Grammar removeERules(Grammar g)
        {
            Grammar g1 = new Grammar();
            Grammar tmpGrammar = removeNonterminalProdusesOnlyERules(g);
            List<Rule> newRules = new List<Rule>();
            _nonterminalSymbolStringMap = new Dictionary<string, List<Symbol>>();
            _nonterminalsForGrammarWithoutERules = new List<Nonterminal>();
            Queue<Nonterminal> newNonterminals = new Queue<Nonterminal>();
            List<Symbol> symStr = new List<Symbol>();
            symStr.Add(tmpGrammar.DistinguishedSymbol);
            newNonterminals.Enqueue(symbolStringToNonterminal(symStr));
            g1.Terminals = new List<Terminal>(tmpGrammar.Terminals);
            g1.DistinguishedSymbol = new Nonterminal(newNonterminals.Peek());
            _nonterminalsForGrammarWithoutERules.Add(g1.DistinguishedSymbol);

            while (newNonterminals.Count > 0)
            {
                Nonterminal curNonterminal = newNonterminals.Dequeue();
                if (!g1.Nonterminals.Exists(A => A.Sym == curNonterminal.Sym))
                {
                    g1.Nonterminals.Add(curNonterminal);
                    g1.Rules.AddRange(buildRulesForNonterminal(curNonterminal, tmpGrammar));
                    foreach (Nonterminal nt in _nonterminalsForGrammarWithoutERules)
                            newNonterminals.Enqueue(nt);
                }
            }

            return g1;
        }
    }
}
