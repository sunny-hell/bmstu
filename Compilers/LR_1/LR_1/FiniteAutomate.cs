using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LR_1
{
    class FiniteAutomate
    {
        private List<int> _states;
        private List<char> _labels;
        private int _initState;
        private List<int> _finStates;
        private List<int> _deadStates;
        //private Dictionary<char, int> labels;
        private Dictionary<int, List<int>> _unionStates;
        private List<Transition> _transitions;
        

        // =========================================
        // Properties
        public int InitState
        {
            set { _initState = value; }
            get { return _initState;  }
        }

        public List<int> FinStates
        {
            set { _finStates = value; }
            get { return _finStates; }
        }
        //
        // ==========================================

        // ==========================================
        // Constructors
        public FiniteAutomate()
        {
          //  labels = new Dictionary<char, int>();
            // transMatrix = new  Dictionary<int,Dictionary<char,List<int>>>();
            _labels = new List<char>();
            _states = new List<int>();
            _finStates = new List<int>();
            _transitions = new List<Transition>();
            _unionStates = new Dictionary<int, List<int>>();
            _deadStates = new List<int>();
        }

        public FiniteAutomate(char label): this()
        {
            InitState = addState();
            FinStates.Add(addState());
            addLabel(label);
            addTransition(InitState, FinStates.First(), label);
        }
        //
        // ==========================================

        // ==========================================
        // Methods
        private void evaluateFinStates(List<int> baseFS, Dictionary<int, List<int>> map)
        {
            foreach (KeyValuePair<int, List<int>> m in map)
            {
                if (m.Value.Intersect(baseFS).Count() != 0)
                    FinStates.Add(m.Key);
            }
        }

        public void save(string filename)
        {
            StreamWriter writer = new StreamWriter(filename, false);
            writer.WriteLine("digraph G{");
            writer.WriteLine("rankdir = LR;");
            writer.WriteLine("{0}[style=\"filled\",fillcolor=\"red\"]", InitState);
            foreach (int state in FinStates)
                writer.WriteLine("{0}[style=\"filled\",fillcolor=\"red\"]", state);
            foreach (Transition t in _transitions)
                writer.WriteLine(t.toGVString());
            writer.WriteLine("}");
            writer.Close();
        }

        public int addState()
        {   // Add new state to automate
            int id = 0;
            if (_states.Count > 0)
                id = _states.Max() + 1;
            _states.Add(id);
            return id;
        }

        public void addLabel(char lbl)
        {
            if (!_labels.Contains(lbl))
                _labels.Add(lbl);
        }
        public void addTransition(int fromState, int toState, char label)
        {   // add new transition to automate
            _transitions.Add(new Transition(fromState, toState, label));
        }

        private int addDeadState()
        {
            int ds = addState();
            foreach (char lbl in _labels)
                addTransition(ds, ds, lbl);
            _deadStates.Add(ds);
            return ds;
        }

        private void removeState(int s)
        {
            _transitions.RemoveAll(t => (t.To == s));
            _states.Remove(s);
        }
        public int add(FiniteAutomate fa)
        {   // copy states from other FA
            int maxSt = 0;
            if (_states.Count > 0)
                maxSt = _states.Max()+1;
            for (int i = 0; i < fa._states.Count; i++)
                _states.Add(fa._states[i] + maxSt);
            for (int i = 0; i < fa._transitions.Count; i++)
                addTransition(fa._transitions[i].From + maxSt, fa._transitions[i].To + maxSt, fa._transitions[i].Label);
            for (int i = 0; i < fa._labels.Count; i++)
                addLabel(fa._labels[i]);
            return maxSt;
        }

        public void unionStates(List<int> statesToUnion)
        {
            int newState = addState();

            _unionStates.Add(newState, statesToUnion);
            for (int i = 0; i < _transitions.Count; i++)
            {
                if (statesToUnion.Contains(_transitions[i].To))
                    _transitions[i].To = newState;
                if (statesToUnion.Contains(_transitions[i].From))
                    _transitions[i].From = newState;
            }
        }

        private List<int> closure(List<int> T, char label)
        {
            Stack<int> stack = new Stack<int>(T);
            List<int> closure = new List<int>();
            if (label == ' ')
                closure.AddRange(T);
            while (stack.Count != 0)
            {
                int node = stack.Pop();
                foreach (Transition t in _transitions)
                    if ((t.From == node) && (t.Label == label))
                        if (!closure.Contains(t.To))
                        {
                            closure.Add(t.To);
                            if (label == ' ')
                                stack.Push(t.To);
                        }
            }
            closure.Sort();
            return closure;
        }

        public FiniteAutomate determine()
        {
            FiniteAutomate detFA = new FiniteAutomate();
            Dictionary<int, bool> markedStates = new Dictionary<int, bool>();
            Dictionary<int, List<int>> statesMap = new Dictionary<int, List<int>>();
            List<int> T = new List<int>();
            T.Add(InitState);
            int newStateID = detFA.addState();  // state number in det. FA
            markedStates.Add(newStateID, false);  // state in unmarked
            statesMap.Add(newStateID, closure(T, ' '));  // maps state in FA to set of states in current FA
            while (markedStates.Values.Contains(false))
            {
                int curStateID = markedStates.First((s) => (s.Value == false)).Key;
                T.Clear();
                T.AddRange(statesMap[curStateID]); 
                markedStates[curStateID] = true;
                foreach (char lbl in _labels)
                {
                    detFA.addLabel(lbl);
                    List<int> newState = closure(closure(T, lbl), ' ');
                    if (newState.Count != 0)
                    {
                        if (!statesMap.Values.ToList().Any(l => l.SequenceEqual(newState))) //  (!setInList(newState, statesMap.Values.ToList()))
                        {
                            newStateID = detFA.addState();
                            markedStates.Add(newStateID, false);
                            statesMap.Add(newStateID, newState);
                        }
                        else
                            newStateID = statesMap.First(sm => (sm.Value.SequenceEqual(newState))).Key;
                        detFA.addTransition(curStateID, newStateID, lbl);
                    }
                }                              
            }
            detFA.evaluateFinStates(FinStates, statesMap);
            return  detFA;

        }

        private bool partitionsAreEqual(List<List<int>> p1, List<List<int>> p2)
        {
            foreach (List<int> p in p1)
                if (!p2.Any(pp => pp.SequenceEqual(p)))
                    return false;
            return true;
        }

        private int findGroupIndex(List<List<int>> p, int state, char lbl)
        {
            int s = _transitions.Find(t => ((t.From == state) && (t.Label == lbl))).To;
            return p.FindIndex(g => g.Contains(s));
 
        }
        private List<List<int>> createSubgroups(List<int> group, List<List<int>> p)
        {
            List<List<int>> subgroups;
            List<List<int>> newSubgroups = new List<List<int>>();
            newSubgroups.Add(group);
            if (group.Count != 1)
            {
                Dictionary<int, List<int>> partition = new Dictionary<int, List<int>>();
                foreach (char lbl in _labels)
                {
                    partition.Clear();
                    subgroups = new List<List<int>>(newSubgroups);
                    newSubgroups.Clear();
                    foreach (List<int> sg in subgroups)
                    {
                        if (sg.Count == 1)
                            newSubgroups.Add(sg);
                        else
                        {
                            foreach (int s in sg)
                            {
                                int gIdx = findGroupIndex(p, s, lbl);
                                if (!partition.ContainsKey(gIdx))
                                    partition.Add(gIdx, new List<int>());
                                partition[gIdx].Add(s);
                            }
                            newSubgroups.AddRange(partition.Values.ToList());
                        }
                    }
                }
            }
            return newSubgroups;
        }
        private List<List<int>> decompose(List<List<int>> p)
        {
            List<List<int>> newP = new List<List<int>>();
            foreach (List<int> g in p)
                if (g.Count == 1)
                    newP.Add(g);
                else
                    newP.AddRange(createSubgroups(g, p));
            return newP;
        }

        private void detectDeadStates()
        {
            _deadStates.Clear();
            bool maybeDead;
            foreach (int s in _states)
            {
                maybeDead = true;
                foreach (char lbl in _labels)
                if (_transitions.Exists(t => (t.From == s) && (t.Label == lbl) && (t.To != s)))
                {
                    maybeDead = false;
                    break;
                }
                if (maybeDead && !_finStates.Contains(s))
                    _deadStates.Add(s);
            }
        }

        private void removeDeadStates()
        {
            _deadStates.ForEach(ds => removeState(ds));
            _deadStates.Clear();
        }

        private void removeUnreachableStates()
        {
            List<int> unreachableStates = new List<int>(_states);
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(InitState);
            unreachableStates.Remove(InitState);
            while ((queue.Count > 0) && (unreachableStates.Count > 0))
            {
                int s = queue.Dequeue();
                unreachableStates.Remove(s);
                foreach (Transition t in _transitions)
                    if (t.From == s)
                        queue.Enqueue(t.To);
            }

            unreachableStates.ForEach(s => removeState(s));
        }

        public void complement()
        {
            // дополним автомат так, чтобы из каждого состояния был переход по каждому символу
            foreach (int s in _states)
            {
                foreach (char lbl in _labels)
                    if (!_transitions.Exists(t => ((t.From == s) && (t.Label == lbl))))
                    {
                        // добавляем "мертвое" состояние
                        int deadState = addDeadState();
                        addTransition(s, deadState, lbl);
                    }
            }
        }

        public FiniteAutomate minimize()
        {
            FiniteAutomate minFA = new FiniteAutomate();
            List<int> nonFinStates = _states.Except(_finStates).ToList();
            List<List<int>> partition = new List<List<int>>();
            List<List<int>> newPartition = new List<List<int>>();
            newPartition.Add(nonFinStates);
            newPartition.Add(_finStates);
            do {
                partition.Clear();
                partition.AddRange(newPartition);
                newPartition = decompose(partition);
            } while(!partitionsAreEqual(partition, newPartition));

            
            foreach (List<int> g in partition)
                minFA.addState();
            foreach (List<int> g in partition)
            {
                int gIdx = partition.IndexOf(g);
                foreach (char lbl in _labels)
                {
                    int to = findGroupIndex(partition, g.First(), lbl);
                    minFA.addTransition(gIdx, to, lbl);
                    if (g.Contains(InitState))
                        minFA.InitState = gIdx;
                    else 
                        if (g.Exists(s => _finStates.Contains(s)))
                            minFA._finStates.Add(gIdx);
                }
            }
            minFA._labels.AddRange(_labels);
            minFA.detectDeadStates();
            minFA.removeDeadStates();
            minFA.removeUnreachableStates();
            return minFA;
        }

        public bool checkString(string s)
        {
            List<int> states = new List<int>();
            states.Add(InitState);
            foreach (char c in s)
                states = closure(states, c);
            return _finStates.Exists(fs => states.Contains(fs));
        }

    }
}
