using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Travelling_Salesman_Problem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private const string _citiesFile = @"..\..\cities.txt";
        private void initCitiesList()
        {
            StreamReader reader = new StreamReader(_citiesFile);
            while (!reader.EndOfStream)
                chklstCities.Items.Add(reader.ReadLine());
            reader.Close();
 
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            initCitiesList();
            
        }

        private int[][] createRandomMatrix(int n)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            int[][] m = new int[n][];
            for (int i = 0; i < n; i++)
            {
                m[i] = new int[n];
                for (int j = 0; j < n; j++)
                    if (i != j)
                        m[i][j] = r.Next(100);
            }
            return m;
        }
        private void btnCreateTable_Click(object sender, EventArgs e)
        {
            tblCosts.Columns.Clear();
            tblCosts.Rows.Clear();
            List<string> cities = new List<string>();
            foreach (string city in chklstCities.CheckedItems)
            {
                cities.Add(city);
                tblCosts.Columns.Add(String.Format("c{0}", city), city);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.HeaderCell.Value = city;
                tblCosts.Rows.Add(newRow);
            }
            tblCosts.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            int[][] m = createRandomMatrix(cities.Count);
            for (int i = 0; i < cities.Count; i++)
                for (int j = 0; j < cities.Count; j++)
                    tblCosts[j, i].Value = m[i][j];
        }

        private double[][] readMatrix()
        {
            int n = tblCosts.Rows.Count;
            double[][] m = new double[n][];
            for (int i = 0; i < n; i++)
            {
                m[i] = new double[n];
                for (int j=0; j<n; j++)
                    if (!Double.TryParse(tblCosts[j,i].Value.ToString(), out m[i][j]))
                        MessageBox.Show(String.Format("Ошибка в таблице стоимостей: строка {0}, столбец {1}.", i, j));
            }
            return m;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int n  = tblCosts.Rows.Count;
            VengerMatrix m = new VengerMatrix(n);
            double[][] c = readMatrix();
            m.setMatrix(c);
            VengerAlgorithm va = new VengerAlgorithm(m);
            String s = "";
            Task t = new Task(n, c);
            BranchAndBoundAlgorithm bba = new BranchAndBoundAlgorithm(t);
            double totalCost;
            int[][] path = bba.start(out totalCost);
            txtResult.Text = pathToString(path);
            lblTotalCost.Text = String.Format("Общая стоимость проезда по маршруту: {0} руб.", totalCost);
            /* if (dbg)
                 va.startDebug(max, out opt, out f, out s);
             else
                 va.start(max, out opt, out f);*/

            //        s += printOptimalMatrix(opt, 5);

        }

        private int getToCity(int[] cand)
        {
            for (int i = 0; i < cand.Count(); i++)
                if (cand[i] == 1)
                    return i;

            return -1;
        }

        private string pathToString(int[][] path)
        {
            StringBuilder sb = new StringBuilder();
            int from = 0, to = -1;
            sb.Append(String.Format("{0} -> ", tblCosts.Rows[from].HeaderCell.Value)); 
            while (to != 0)
            {
                to = getToCity(path[from]);
                if (to != 0)
                    sb.Append(String.Format("{0} -> ",tblCosts.Columns[to].HeaderCell.Value));
                else
                    sb.Append(String.Format("{0}", tblCosts.Columns[to].HeaderCell.Value));
                from = to;
            }
            return sb.ToString();
        }
    }
}
