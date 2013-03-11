using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_1
{
    class Transition
    {
        private int _from;
        private int _to;
        private char _label;

        public Transition(int from, int to, char lbl)
        {
            _from = from;
            _to = to;
            _label = lbl;
        }

        public int From
        {
            get { return _from; }
            set { _from = value; }
        }

        public int To
        {
            get { return _to; }
            set { _to = value; }
        }

        public char Label
        {
            get { return _label; }
            set { _label = value; }
        }

        public string toGVString()
        {
            string gvLabel = Label.ToString();
            if (gvLabel == " ")
                gvLabel = "eps";
            return String.Format("{0}->{1}[label=\"{2}\"];", From, To, gvLabel);
        }
    }
}
 