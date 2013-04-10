using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travelling_Salesman_Problem
{
    class LChain
    {
        // Структура для представления элемента цепочки
        struct Position
        {
            public int _x;
            public int _y;

            public Position(int x=-1, int y=-1)
            {
                _x = x;
                _y = y;
            }

        }

        private Position[] _chain;  // цепочка
        public int N {get; set;}    // длина цепочки

        // Constructor
        public LChain(int n)
        {
            _chain = new Position[n];
            N = 0;
        }

        // Metods
        /// <summary>
        /// Добавить в цепочку новый элемент
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public void add(int i, int j)
        {
            _chain[N] = new Position(i, j);
            N++;
        }

        public void get(int i, out int x, out int y)
        {
            x = -1;
            y = -1;
            if (i<N)
            {
                x = _chain[i]._x;
                y = _chain[i]._y;
            }
        }

        public override String ToString()
        {
            String s = "";
            for (int i = 0; i < N; i++)
                s += String.Format("({0},{1}), ", _chain[i]._x, _chain[i]._y);
            s += "\n";
            return s;
        }
    }
}
