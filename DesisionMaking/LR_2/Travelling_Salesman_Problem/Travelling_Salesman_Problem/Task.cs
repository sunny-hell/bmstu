using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travelling_Salesman_Problem
{
    class Task
    {
        public int N { get; set; }
        private double[][] _c;

        public Task(int n, double[][] c)
        {
            N = n;
            _c = new double[N][];
            for (int i = 0; i < N; i++)
                _c[i] = new double[N];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    _c[i][j] = c[i][j];
        }

        private double[][] copy()
        {
            double[][] c = new double[N][];
            for (int i = 0; i < N; i++)
                c[i] = new double[N];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    c[i][j] = _c[i][j];
            return c;
        }

        public double[][] getValue()
        {
            return copy();
        }

        public VengerMatrix toVengerMatrix()
        {
            VengerMatrix vm = new VengerMatrix(N);
            vm.setMatrix(copy());
            return vm;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (Double.IsPositiveInfinity(_c[i][j]))
                    {
                        char c = (char)8734;
                        s += c + "\t";
                    }
                    else
                        s += String.Format("{0}\t", _c[i][j]);
                }
                s += "\n";
            }
            return s;
        }
    }
}
