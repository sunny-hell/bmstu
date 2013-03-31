using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LR_4
{
    class TextGrammarReader: GrammarReader
    {
        // reads the grammar from the text file
        private List<string> readBlock(StreamReader sr)
        {
            if (sr.EndOfStream)
                throw new FormatException("Некорректный формат файла.");
            string line = sr.ReadLine();
            int n;
            if (!Int32.TryParse(line, out n))
                throw new FormatException("Некорректный формат файла.");
            List<string> lines = new List<string>();
            for (int i = 0; i < n; i++)
            {
                if (sr.EndOfStream)
                    throw new FormatException("Некорректный формат файла.");
                lines.Add(sr.ReadLine());
            }
            return lines;
        }

        private string readAxiom(StreamReader sr)
        {
            if (sr.EndOfStream)
                throw new FormatException("Некорректный формат файла.");
            return sr.ReadLine();
 
        }
        private Rule parseRule(string s, List<string> terminals, List<string> nonterminals)
        {
            Rule r = new Rule();
            int leftLength = s.IndexOf(" -> ");
            if (leftLength != 1)
                throw new FormatException("Некорректный формат файла.");
            r.Left.Sym = s.First().ToString();
            if (s.Length == 6 && s[5] == 'e')
            {
                r.Right.Add(new EmptySymbol());
                return r;
            }
            for (int i=5; i<s.Length; i++)
            {
                if (s[i] == 'e')
                    throw new FormatException("Некорректный формат файла.");
                if (terminals.Contains(s[i].ToString()))
                    r.Right.Add(new Terminal(s[i].ToString()));
                else
                    if (nonterminals.Contains(s[i].ToString()))
                        r.Right.Add(new Nonterminal(s[i].ToString()));
                    else
                        throw new FormatException("Некорректный формат файла.");

            }
            return r;
        }
        private List<Rule> parseRules(List<string> strRules, List<string> terminals, List<string> nonterminals)
        {
            List<Rule> rules = new List<Rule>();
            strRules.ForEach(s => rules.Add(parseRule(s, terminals, nonterminals)));
            return rules;
 
        }
        public override Grammar read(string fname)
        {
            Grammar g = new Grammar();
            StreamReader sr = new StreamReader(fname);
            string line;
            // читаем нетерминалы
            List<string> nonTerms = readBlock(sr);
            nonTerms.ForEach(s => g.Nonterminals.Add(new Nonterminal(s)));
            // читаем терминалы
            List<string> terms = readBlock(sr);
            terms.ForEach(s => g.Terminals.Add(new Terminal(s)));
            // читаем правила
            List<string> rules = readBlock(sr);
            g.Rules = parseRules(rules, terms, nonTerms);
            // читаем аксиому
            string axiom = readAxiom(sr);
            if (!nonTerms.Contains(axiom))
                throw new FormatException("Некорректный формат файла.");
            g.DistinguishedSymbol = new Nonterminal(axiom);
            sr.Close();
            g.findVanishingSymbols();
            return g;
        }
    }
}
