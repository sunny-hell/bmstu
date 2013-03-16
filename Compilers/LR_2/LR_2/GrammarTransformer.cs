using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_2
{
    class GrammarTransformer
    {
        private Dictionary<string, Nonterminal> _nonnullableNonterminalsMap;
        
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
    }
}
