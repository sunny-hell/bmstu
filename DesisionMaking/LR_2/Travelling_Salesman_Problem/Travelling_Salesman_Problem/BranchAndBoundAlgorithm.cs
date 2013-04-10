using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travelling_Salesman_Problem
{
    class BranchAndBoundAlgorithm
    {
        private Queue<Task> _taskList;   // Список текущих задач
        private double _f;              // Оптимальное значение целевой функции
        private int[][] _x;             // Оптимальное решение
        private int _n;                 // Размер матрицы
        private List<Loop> _subLoops;

        public BranchAndBoundAlgorithm() { }
        public BranchAndBoundAlgorithm(Task t)
        {
            _taskList = new Queue<Task>();
            _taskList.Enqueue(t);
            _n = t.N;
            _x = new int[_n][];
            for (int i = 0; i < _n; i++)
                _x[i] = new int[_n];
            _subLoops = new List<Loop>();

            // Начальные оценки допустимого решения х* = "1 - 2 - ... - n - 1" и f*
            double[][] c = t.getValue();
            _f = 0;
            for (int i = 0; i < _n - 1; i++)
            {
                _x[i][i + 1] = 1;
                _f += c[i][i + 1];
            }
            _x[_n - 1][0] = 1;
            _f += c[_n - 1][0];
                     
        }

        private int getInd(int[] a)
        {
            for (int i=0; i<a.Length; i++)
                if (a[i] == 1)
                    return i;
            return -1;
        }
        private List<Loop> getSubLoops(int[][] m)
        {
            List<Loop> loops = new List<Loop>();
            List<int> cities = new List<int>(_n);
            for (int i = 0; i < _n; i++)
                cities.Add(i);
            while (cities.Count != 0)
            {
                Loop l = new Loop();
                int cur = cities[0];
                do
                {
                    l.add(cur);
                    cities.Remove(cur);
                    cur = getInd(m[cur]);
                } while (cur != l[0]);
                loops.Add(l);
            }
            return loops;
        }

        private String printOptimalMatrix(int[][] opt, int n)
        {
            String s = "";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    s = s + opt[i][j] + " \t";
                s += "\n";
            }
            return s;
        }

        private void addNewTasks(Loop l, Task t)
        {
            double[][] c;
            for (int k = 0; k < l.Length; k++)
            {
                int i,j;
                c = t.getValue();
                if (k != l.Length-1)
                {
                    i = l[k];
                    j = l[k+1];
                }
                else
                {
                    i = l[k];
                    j = l[0];
                }
                c[i][j] = Double.PositiveInfinity;
                Task nt = new Task(_n, c);
                _taskList.Enqueue(nt);
            }
        }

        private Loop getMinLoops(List<Loop> loops)
        {
            int min = 0;
            for (int i = 0; i < loops.Count; i++)
                if (loops[i].Length < loops[min].Length)
                    min = i;
            return loops[min];
        }
        public void start(bool dbg, out String s)
        {
            s = "";
            Task tsk;
            int[][] x;
            double f;
            int i = 1;

            if (dbg)
            {
                s += "Начальные оценки: \n";
                s += "x*: \n";
                s += printOptimalMatrix(_x, _n);
                s += String.Format("f* = {0}\n", _f);
                s += "\n";
            }
            while (_taskList.Count > 0)
            {
                if (dbg)
                {
                    s += String.Format("Число задач в списке S: {0}.\n", _taskList.Count);
                    s += String.Format("Итерация № {0}\n", i);
                    i++;
                }
                tsk = _taskList.Dequeue();
                if (dbg)
                    s += "Решается задача: \n" + tsk.ToString();
                 
                VengerAlgorithm va = new VengerAlgorithm(tsk.toVengerMatrix());
                va.start(false, out x, out f);
                if (dbg)
                {
                    s += "x: \n";
                    s += printOptimalMatrix(x, _n);
                    s += String.Format("f = {0} \n", f);
                }
                
                // полученное значение целевой функции меньше текущего?
                if (f < _f)
                {
                    List<Loop> loops = getSubLoops(x);
                    if (loops.Count == 1)
                    {
                        _f = f;
                        _x = x;
                        if (dbg)
                        {
                            s += "Полученное решение является полным подциклом; \n";
                            s += String.Format("x* = x, f* = {0}\n", _f);
                            s += "\n";
                        }
                    }
                    else
                    {
                        if (dbg)
                        {
                            s += "Подциклы: ";
                            foreach (Loop l in loops)
                                s += l.ToString() + ";";
                            s += "\n\n";
                        }
                        Loop min = getMinLoops(loops);
                        addNewTasks(min, tsk);
                    }
                }
            }
            if (dbg)
                s += "Список задач S пуст.\n";
            s += "============================================\n";
            s += "Итоговое решение задачи: \n";
            s += "x*:\n";
            s += printOptimalMatrix(_x, _n);
            s += String.Format("f* = {0} \n", _f);
        }

        public int[][] start(out double cost)
        {
            Task tsk;
            int[][] x;
            double f;
            int i = 1;

            while (_taskList.Count > 0)
            {
                tsk = _taskList.Dequeue();
                
                VengerAlgorithm va = new VengerAlgorithm(tsk.toVengerMatrix());
                va.start(false, out x, out f);
                
                // полученное значение целевой функции меньше текущего?
                if (f < _f)
                {
                    List<Loop> loops = getSubLoops(x);
                    if (loops.Count == 1)
                    {
                        _f = f;
                        _x = x;
                    }
                    else
                    {
                        Loop min = getMinLoops(loops);
                        addNewTasks(min, tsk);
                    }
                }
            }
            cost = _f;
            return _x;
        }
    }
}
