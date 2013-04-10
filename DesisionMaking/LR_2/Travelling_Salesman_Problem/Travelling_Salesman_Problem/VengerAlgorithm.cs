using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travelling_Salesman_Problem
{
    /// <summary>
    /// Класс, представляющий работу Венгерского метода
    /// </summary>
    class VengerAlgorithm
    {
        private VengerMatrix _c;  // Исходная матрица стоимостей

        // Constructors
        public VengerAlgorithm(VengerMatrix c)
        {
            _c = c;
        }

        public void minimize(ref VengerMatrix c)
        {
            double tau = c.getMax();
            c.subFromTau(tau);
        }
        /// <summary>
        /// Подготовительный этап.
        /// 1.Вычесть из каждого столбца минимальный находящийся в нем элемент.
        /// 2.Вычесть из каждой строки минимальный находящийся в ней элемент.
        /// </summary>
        /// <returns> Эквивалентная матрица стоимостей </returns>
        private void getNewMatrix(ref VengerMatrix c)
        {
            //VengerMatrix c = _c.Copy();
            for (int i = 0; i < c.N; i++)
            {
                double min = c.getMinInColumn(i);
                c.subFromColumn(i, min);
            }
            for (int i = 0; i < c.N; i++)
            {
                double min = c.getMinInRow(i);
                c.subFromRow(i, min);
            }
            
        }
        /// <summary>
        /// Построение первоначальной СНН
        /// </summary>
        /// <param name="c"></param>
        private void buildInitialINS(ref VengerMatrix c)
        {
            c.K = 0;
            for (int j = 0; j < c.N; j++)
                for (int i = 0; i < c.N; i++)
                    if (c.get(i, j) == 0.0 && (c.hasMarkedElementInRow(i, '*') == -1))
                    {
                        c.markElement(i, j, '*');
                        c.K++;
                        break;
                    }
        }

        private LChain buildLChain(VengerMatrix c, int i, int j)
        {
            LChain chain = new LChain(c.N * c.N);
            chain.add(i, j);
            while ((i = c.hasMarkedElementInColumn(j, '*')) != -1)
            {
                chain.add(i, j);
                j = c.hasMarkedElementInRow(i, '\'');
                chain.add(i, j);
            }
            return chain;
        }

        public void startDebug(bool max, out int[][] opt, out double f, out String s)
        {
            s = "";
            s += "Исходная матрица: \n";
            s += (_c.print() + "\n");
            VengerMatrix c = _c.Copy();
            if (max)
            {
                minimize(ref c);
                s += "Эквивалентная матрица\n";
                s += (c.print() + "\n");
            }
            getNewMatrix(ref c);
            s += "Начальная система независимых нулей: \n";
            buildInitialINS(ref c);
            s += (c.print() + "\n");
            while (c.K < c.N)
            {
                c.markColumns();
                int i = 0, j = 0;
                bool exit = false;
                while (!exit)
                {
                    while (!c.hasUnmarkedNulls(out i, out j))
                        c.update();
                    c.markElement(i, j, '\'');
                    s += String.Format("Среди невыделенных элементов есть 0; Позиция ({0},{1})\n", i, j);
                    s += (c.print() + "\n");
                    int k = c.hasMarkedElementInRow(i, '*');
                    if (k != -1)
                    {
                        c.markRow(i);
                        c.unmarkColumn(k);
                    }
                    else
                    {
                        exit = true;
                        LChain chain = buildLChain(c, i, j);
                        s += "L-цепочка: " + chain.ToString();
                        c.replaceInChain(chain);
                        c.unmarkAll();
                        s += (c.print() + "\n");
                    }
                }
            }
            // Поиск оптимального решения и суммы затрат
            opt = c.createOptimalMatrix();
            f = _c.f(opt);
        }

        public void start(bool max, out int[][] opt, out double f)
        {
            VengerMatrix c = _c.Copy();
            if (max)
                minimize(ref c);
            getNewMatrix(ref c);
            buildInitialINS(ref c);
            while (c.K < c.N)
            {
                c.markColumns();
                int i = 0, j = 0;
                bool exit = false;
                while (!exit)
                {
                    while (!c.hasUnmarkedNulls(out i, out j))
                        c.update();
                    c.markElement(i, j, '\'');
                    int k = c.hasMarkedElementInRow(i, '*');
                    if (k != -1)
                    {
                        c.markRow(i);
                        c.unmarkColumn(k);
                    }
                    else
                    {
                        exit = true;
                        LChain chain = buildLChain(c, i, j);
                        c.replaceInChain(chain);
                        c.unmarkAll();
                    }
                }
            }
            // Поиск оптимального решения и суммы затрат
            opt = c.createOptimalMatrix();
            f = _c.f(opt);
        }
    }
}
