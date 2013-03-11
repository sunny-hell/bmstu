using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_1
{
    class Dispatcher
    {
        private FiniteAutomate _nonDeterministicFA;
        private FiniteAutomate _deterministicFA;
        private FiniteAutomate _minDeterministicFA;

        public Dispatcher()
        {
            _nonDeterministicFA = new FiniteAutomate();
            _deterministicFA = new FiniteAutomate();
            _minDeterministicFA = new FiniteAutomate();
        }

        public void buildNonDeterministicFA(string regExpr)
        {
            string postfixNotation = ExpressionTransformer.transformToPostfixNotation(regExpr);
            BuilderFA builder = new TompsonBuilder();
            _nonDeterministicFA = builder.build(postfixNotation);
        }

        public void buildDeterminsticFA()
        {
            _deterministicFA = _nonDeterministicFA.determine();
        }

        public void minimizeDeterministicFA()
        {
            _minDeterministicFA = _deterministicFA.minimize();
        }

        public void buildAll(string regExpr)
        {
            buildNonDeterministicFA(regExpr);
            buildDeterminsticFA();
            minimizeDeterministicFA();
            _deterministicFA = _nonDeterministicFA.determine();
        }
        public void saveNonDeterministicFA(string fname)
        {
            _nonDeterministicFA.save(fname);
        }

        public void saveDeterministicFA(string fname)
        {
            
            _deterministicFA.save(fname);
        }

        public void saveMinDeterministicFA(string fname)
        {
            _minDeterministicFA.save(fname);
        }

        public void saveAll(string[] fnames)
        {
            saveNonDeterministicFA(fnames[0]);
            saveDeterministicFA(fnames[1]);
            saveMinDeterministicFA(fnames[2]);
        }

        public bool checkString(string s)
        {
            return _minDeterministicFA.checkString(s);
        }
    }
}
