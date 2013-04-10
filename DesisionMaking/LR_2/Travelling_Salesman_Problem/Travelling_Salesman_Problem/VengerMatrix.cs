using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Travelling_Salesman_Problem
{
    
    class VengerMatrix
    {
        /// <summary>
        /// Класс для представления матрицы стоимстей
        /// </summary>
        struct Element
        {
            /// <summary>
            /// Структура для представления элементов матрицы
            /// </summary>
            public double _value;
            public char _mark;
            public Element(double val, char mark = ' ')
            {
                _value = val;
                _mark = mark;
            }
            
        }
        //private int _n; // размерность матрицы
        //private int _k; // число нулей в СНН
        private Element[][] _c; // матрица стоимостей
        private int[] _markedRows, _markedColumns; // номера выделенных строк и столбцов
        private int _mrCnt, _mcCnt;

        // properties
        public int N { get; set; } // размерность матрицы
        public int K { get; set; } // число нулей в СНН
        public int mrCnt        // текущее число элементов в списке выделенных строк
        {
            get { return _mrCnt; }
            set 
            {
                if (value < N)
                    _mrCnt = value;
            }
        }

        public int mcCnt        // текущее число элементов в списке выделенных столбцов
        {
            get { return _mcCnt; }
            set
            {
                if (value < N)
                    _mcCnt = value;
            }
        }


        // Constructors
        public VengerMatrix(int n)
        {
            N = n;
            K = 0;
            _c = new Element[n][];
            for (int i = 0; i < n; i++)
                _c[i] = new Element[n];
            _markedRows = new int[n];
            _markedColumns = new int[n];
            
            
            mrCnt = 0;
            mcCnt = 0;
        }

        // Methods

        private double getMinFromUnmarked()
        {
            double min = Double.MaxValue;
            for (int j = 0; j < N; j++)
            {
                if (_markedColumns[j] == 1)
                    continue;
                for (int i = 0; i < N; i++)
                {
                    if (_markedRows[i] == 1)
                        continue;
                    if (_c[i][j]._value < min)
                        min = _c[i][j]._value;
                }
            }
            return min;
        }
        /// <summary>
        /// Задать значения матрицы
        /// </summary>
        /// <param name="c"></param>
        public void setMatrix(double[][] c)
        {
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    _c[i][j] = new Element(c[i][j]);

        }
        /// <summary>
        /// Печать матрицы
        /// </summary>
        /// <returns></returns>
        public String print()
        {
            String s = "";
            for (int i=0; i<N; i++)
            {
                for (int j = 0; j < N; j++)
                    s = s +_c[i][j]._value + _c[i][j]._mark + "\t";
                s += "\n";
            }
            return s;
        }

        /// <summary>
        /// вычисление целевой функции
        /// </summary>
        /// <param name="opt"></param>
        /// <returns></returns>
        public double f(int[][] opt)
        {
            double sum = 0;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    if (opt[i][j] == 1)
                        sum += _c[i][j]._value;
            return sum;
        }

        /// <summary>
        /// Получить копию матрицы
        /// </summary>
        /// <returns></returns>
        public VengerMatrix Copy()
        {
            VengerMatrix m = new VengerMatrix(N);
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    m._c[i][j] = new Element(_c[i][j]._value, _c[i][j]._mark);
            return m; 
        }

        /// <summary>
        /// Получить максимальный элемент матрицы
        /// </summary>
        /// <returns></returns>
        public double getMax()
        {
            double max = -1.0;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    if (_c[i][j]._value > max)
                        max = _c[i][j]._value;
            return max;
        }

        public void subFromTau(double tau)
        {
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    _c[i][j]._value = tau - _c[i][j]._value;
        }
        /// <summary>
        /// Получить минимальный элемент в столбце
        /// </summary>
        /// <param name="j"> номер столбца </param>
        /// <returns></returns>
        public double getMinInColumn(int j)
        {
            double min = Double.MaxValue;
            for (int i = 0; i < N; i++)
                if (_c[i][j]._value < min)
                    min = _c[i][j]._value;
            return min;
        }
        /// <summary>
        /// Получить минимальный элемент в строке
        /// </summary>
        /// <param name="i"> номер строки </param>
        /// <returns></returns>
        public double getMinInRow(int i)
        {
            double min = Double.MaxValue;
            for (int j = 0; j < N; j++)
                if (_c[i][j]._value < min)
                    min = _c[i][j]._value;
            return min;
        }

        /// <summary>
        /// Вычитание одного и того же числа из строки матрицы 
        /// </summary>
        /// <param name="i">номер строки</param>
        /// <param name="value">вычитаемое число</param>
        public void subFromRow(int i, double value)
        {
            for (int j = 0; j < N; j++)
                _c[i][j]._value -= value;
        }

        
        /// <summary>
        /// Вычитание одного и того же числа из столбца матрицы 
        /// </summary>
        /// <param name="j">номер столбца</param>
        /// <param name="value">вычитаемое число</param>
        public void subFromColumn(int j, double value)
        {
            for (int i = 0; i < N; i++)
                _c[i][j]._value -= value;
        }

        /// <summary>
        /// Выделить элемент матрицы
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="sym">символ выделения</param>
        public void markElement(int i, int j, char sym)
        {
            _c[i][j]._mark = sym;
        }

        /// <summary>
        /// Проверка наличия в строке выделенного элемента
        /// </summary>
        /// <param name="i"> № строки</param>
        /// <param name="sym"> символ выделения </param>
        /// <returns></returns>
        public int hasMarkedElementInRow(int i, char sym)
        {
            for (int j = 0; j < N; j++)
                if (_c[i][j]._mark == sym)
                    return j;
            return -1;
        }

        /// <summary>
        /// Проверка наличия в столбце выделенного элемента
        /// </summary>
        /// <param name="j"> № столбца</param>
        /// <param name="sym"> символ выделения </param>
        /// <returns></returns>
        public int hasMarkedElementInColumn(int j, char sym)
        {
            for (int i = 0; i < N; i++)
                if (_c[i][j]._mark == sym)
                    return i;
            return -1;
        }
        /// <summary>
        /// Получить эл-т матрицы (i, j)
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public double get(int i, int j)
        {
            return _c[i][j]._value;
        }

        /// <summary>
        /// Выделить столбцы, содержащие 0*
        /// </summary>
        public void markColumns()
        {
            for (int j=0; j<N; j++)
                if (hasMarkedElementInColumn(j, '*') != -1)
                    _markedColumns[j] = 1;
        }
        /// <summary>
        /// Выделить строку с номером i 
        /// </summary>
        /// <param name="i"></param>
        public void markRow(int i)
        {
            _markedRows[i] = 1;
        }
        /// <summary>
        /// Снять выделение со столбца
        /// </summary>
        /// <param name="j"></param>
        public void unmarkColumn(int j)
        {
            _markedColumns[j] = 0;
        }

        public void unmarkAll()
        {
            for (int i = 0; i < N; i++)
            {
                _markedColumns[i] = 0;
                _markedRows[i] = 0;
                for (int j = 0; j < N; j++)
                    if (_c[i][j]._mark == '\'')
                        _c[i][j]._mark = ' ';

            }
        }
        /// <summary>
        /// Проверка: "Среди невыделенных эл-тов есть нуль?"
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public bool hasUnmarkedNulls(out int i, out int j)
        {
            i = -1;
            j = -1;
            for (int l=0; l<N; l++)
            {
                if (_markedColumns[l] == 1)
                    continue;
                for (int k = 0; k < N; k++)
                {
                    if (_markedRows[k] == 1)
                        continue;
                    if (_c[k][l]._value == 0.0)
                    {
                        i = k;
                        j = l;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Обновление матрицы в случае, когда среди невыделенных элементов не оказалось нуля
        /// </summary>
        public void update()
        {
            double min = getMinFromUnmarked();
            for (int i = 0; i < N; i++)
            {
                // Вычесть из невыделенной строки
                if (_markedRows[i] != 1)
                    subFromRow(i, min);
                // Добавить к выделенному столбцу
                if (_markedColumns[i] == 1)
                    subFromColumn(i, -min);
            }
            
        }

        /// <summary>
        /// Замена по L-цепочке
        /// </summary>
        /// <param name="chain"></param>
        public void replaceInChain(LChain chain)
        {
            for (int k = 0; k < chain.N; k++)
            {
                int i = -1, j = -1;
                chain.get(k, out i, out j);
                if (_c[i][j]._mark == '*')
                {
                    _c[i][j]._mark = ' ';
                    K--;
                }
                else
                {
                    _c[i][j]._mark = '*';
                    K++;
                }
            }
        }

        public int[][] createOptimalMatrix()
        {
            int[][] opt = new int [N][];
            for (int i = 0; i < N; i++)
            {
                opt[i] = new int[N];
                for (int j = 0; j < N; j++)
                    if (_c[i][j]._mark == '*')
                        opt[i][j] = 1;
                    else
                        opt[i][j] = 0;
            }
            return opt;
        }
    }
}
