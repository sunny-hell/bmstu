using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_3
{
    class DescendingBacktrackAnalyzer: Analyzer
    {
        enum State { NORMAL, FINISH, RETURN }

        // ----------------------- Конфигурация -----------------------
        private State _state;                   //  текущее состояние
        private int _position;                  //  позиция во входной цепочке
        private Stack<Symbol> _currentString;    //  Магазин L2 - цепочка, которая получилась к данному моменту в результате развертки нетерминалов
        private Stack<HistoryItem> _history;     //  Магазин L1 - текущая история проделанных выборов альтернатив и выходные символы, по которым прошла входная головка. 
        // ------------------------------------------------------------

        private Dictionary<string, List<Alternative>> _orderedAlternatives;
        private List<Symbol> _inputString;
        private int _altIdx;  // текущая альтернатива
        private bool _noMoreConfigurations;    // флаг, указывающий на то, что следующая конфигурация невозможна
        private StringBuilder _configurationHistory;

        public DescendingBacktrackAnalyzer(Grammar g) : base(g) 
        {
            _state = State.NORMAL;
            _position = 0;
            _currentString = new Stack<Symbol>();
            _history = new Stack<HistoryItem>();
            _orderedAlternatives = new Dictionary<string, List<Alternative>>();
            _inputString = new List<Symbol>();
            _altIdx = 0;
            _noMoreConfigurations = false;
            _configurationHistory = new StringBuilder();
        }

        private void resetConfiguration()
        {
            _state = State.NORMAL;
            _position = 0;
            _currentString.Push(_grammar.DistinguishedSymbol);
            _history.Clear();
            _configurationHistory = new StringBuilder();
        }
        
        private void saveInputString(List<Symbol> input)
        {
            _inputString = new List<Symbol>(input);
        }

        private Alternative createAlternative(Rule r)
        {
            int idx = _orderedAlternatives[r.Left.Sym].Count;
            return new Alternative(r.Left, idx, r.Right);
        }
 
        private void orderAlternatives()
        {
            foreach (Nonterminal A in _grammar.Nonterminals)
                foreach (Rule r in _grammar.Rules)
                    if (r.Left.Sym == A.Sym)
                    {
                        if (!_orderedAlternatives.ContainsKey(A.Sym))
                            _orderedAlternatives.Add(A.Sym, new List<Alternative>());
                        _orderedAlternatives[A.Sym].Add(createAlternative(r));
                    }
        }

        private void pushAlternativeToCurrentString(Alternative a)
        {
            List<Symbol> altValue = new List<Symbol>(a.Value);
            altValue.Reverse();
            altValue.ForEach(sym => _currentString.Push(sym));
        }

        
        private void saveConfiguration()
        {
            StringBuilder sb = new StringBuilder();
            foreach (HistoryItem hi in _history.Reverse())
                sb.Append(hi.ToString());
            string strHistory = sb.ToString();
            sb.Clear();
            foreach (Symbol s in _currentString)
                sb.Append(s.ToString());
            string strCurrentString = sb.ToString();
            _configurationHistory.AppendLine(String.Format("({0},\t{1},\t{2},\t{3})", _state, _position, strHistory, strCurrentString));
        }
        // ----------------------- Типы шагов -----------------------
        private void stepExpansion()
        {   // разрастание дерева
            Nonterminal A = (Nonterminal)_currentString.Pop();
            Alternative alt = _orderedAlternatives[A.Sym][_altIdx];
            _history.Push(alt);
            pushAlternativeToCurrentString(alt);
        }

        private void stepComparedSuccess()
        {
            // успешное сравнение входного символа с порожденным
            _history.Push(_currentString.Pop());
            _position++;
        }

        private void stepComparedFail()
        {   // неудачное сравнение входного символа с порожденным
            _state = State.RETURN;
        }

        private void stepReturn()
        {   // возврат
            while (_history.Peek() is Terminal)
            {
                _currentString.Push((Symbol)_history.Pop());
                _position--;
            }
        }

        private void stepTryNextAlternative()
        {   // испытание очередной альтернативы
            Alternative oldAlt = (Alternative)_history.Pop();
            Nonterminal A = oldAlt.Base;
            _altIdx = oldAlt.Index + 1;
            for (int i = 0; i < oldAlt.Value.Count; i++)
                _currentString.Pop();
            if (_altIdx < _orderedAlternatives[A.Sym].Count)
            {
                Alternative newAlt = _orderedAlternatives[A.Sym][_altIdx];
                _history.Push(newAlt);
                pushAlternativeToCurrentString(newAlt);
                _altIdx = 0;
                _state = State.NORMAL;
            }
            else
            {
                if ((A.Sym == _grammar.DistinguishedSymbol.Sym) && (_position == 1))
                    _noMoreConfigurations = true;
                else
                    _currentString.Push(A);
            }
        }

        private void stepFinish()
        {
            _state = State.FINISH;
        }
        // ----------------------------------------------------------

        private bool doAnalysis()
        {
            resetConfiguration();

            while (!_noMoreConfigurations)
            {
                saveConfiguration();
                switch (_state)
                {
                    case State.NORMAL:
                        if (_currentString.Count > 0)
                        {
                            Symbol sym = _currentString.Peek();
                            if (sym is Terminal)
                                if ((_position < _inputString.Count) && (sym.Sym == _inputString[_position].Sym))
                                    stepComparedSuccess();
                                else
                                    stepComparedFail();
                            else
                                stepExpansion();
                        }
                        else
                            stepFinish();
                        break;
                    case State.RETURN:
                        if (_history.Peek() is Terminal)
                            stepReturn();
                        else
                            stepTryNextAlternative();
                        break;
                    case State.FINISH:
                        if ((_position == _inputString.Count) && (_currentString.Count==0))
                            return true;
                        return false;
                }
 
            }

            return false;
        }
        public override bool analyzeSymbolString(List<Symbol> symbolString)
        {
            saveInputString(symbolString);
            orderAlternatives();
            return doAnalysis();
        }

        public StringBuilder getHistory()
        {
            return _configurationHistory;
        }

    }
}
