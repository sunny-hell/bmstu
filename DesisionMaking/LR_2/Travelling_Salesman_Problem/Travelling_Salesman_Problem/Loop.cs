using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travelling_Salesman_Problem
{
    class Loop
    {
        List<int> _loop;

        public int Length
        {
            get { return _loop.Count; }
        }

        public int this[int i]
        {
            get
            {
                return _loop[i];
            }
        }
        public Loop() { _loop = new List<int>(); }
        public Loop(List <int> elements)
        {
            _loop = elements;
        }

        public void add(int i)
        {
            _loop.Add(i);
        }

        public override string ToString()
        {
            string s = "(";
            foreach (int i in _loop)
                s += String.Format("{0} ", i);
            s.TrimEnd(' ');
            s += ")";
            return s;
        }
    }
}
