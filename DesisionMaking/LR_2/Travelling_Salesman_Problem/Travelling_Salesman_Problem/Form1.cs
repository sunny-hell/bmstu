using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Travelling_Salesman_Problem
{
    public partial class Form1 : Form
    {
        bool rand = false, dbg = false, max = false;
        public Form1()
        {
            InitializeComponent();

        }

        private double[][] readMatrix(int n)
        {
            double[][] c = new double[n][];
            string[] s = rtbSource.Lines;
            //try
            //{
                for (int i = 0; i < n; i++)
                {
                    string[] row = s[i].Split('\t', ' ');
                    if (row.Length != n)
                        throw new Exception("Некорректно задана исходная матрица.");
                    char ch = row[0][0];
                    c[i] = new double[n];
                    for (int j = 0; j < row.Length; j++)
                    {
                        if (row[j][0] == 8734)
                            c[i][j] = Double.PositiveInfinity;
                        else
                          if (!Double.TryParse(row[j], out c[i][j]))
                            throw new Exception("Некорректно задана исходная матрица.");
                    }
                }
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //    return null;
            //}
            return c;

        }
        

        private double[][] createRandomMatrix(int n)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            double[][] m = new double[n][];
            for (int i = 0; i < n; i++)
            {
                m[i] = new double[n];
                for (int j = 0; j < n; j++)
                    m[i][j] = r.Next(20);
            }
            return m;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VengerMatrix m;
            int n;
            double[][] c;
            try
            {
                if (rand)
                {
                    n = Int32.Parse(txtSize.Text);
                    m = new VengerMatrix(n);
                    c = createRandomMatrix(n);
                    m.setMatrix(c);
                    rtbSource.Text = m.print();
                }
                else
                {
                    n = Int32.Parse(txtSize.Text);
                    m = new VengerMatrix(n);
                    c = readMatrix(n);

                    //double[][] c = {
                    //         new double [5]{10.0, 12.0, 7.0, 11.0, 10.0},
                    //         new double [5]{12.0, 5.0, 12.0, 7.0, 12.0},
                    //         new double [5]{8.0, 6.0, 7.0, 8.0, 13.0},
                    //         new double [5]{8.0, 11.0, 5.0, 9.0, 9.0},
                    //         new double [5]{10.0, 8.0, 9.0, 11.0, 11.0}};
                    //int[][] c = {
                    //         new int [5]{0, 0, 0, 0, 0},
                    //         new int [5]{0, 0, 0, 0, 0},
                    //         new int [5]{0, 0, 0, 0, 0},
                    //         new int [5]{0, 0, 0, 0, 0},
                    //         new int [5]{0, 0, 0, 0, 0}};
                    m.setMatrix(c);
                }


                VengerAlgorithm va = new VengerAlgorithm(m);
                String s = "";
                int[][] opt;
                double f;
                Task t = new Task(n, c);
                BranchAndBoundAlgorithm bba = new BranchAndBoundAlgorithm(t);
                bba.start(dbg, out s);
                
               /* if (dbg)
                    va.startDebug(max, out opt, out f, out s);
                else
                    va.start(max, out opt, out f);*/
                
        //        s += printOptimalMatrix(opt, 5);
               
                rtbResult.Text = s; // m.print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //for (int i = 0; i < m.N; i++)
            //    rtbResult.Text += (m.getMinInRow(i) + "\t");
            //rtbResult.Text += "\n";

            //VengerMatrix m2 = m.Copy();
            //rtbResult.Text += "\n" + m2.print();
        }

        private void chkDebug_CheckedChanged(object sender, EventArgs e)
        {
            dbg = chkDebug.Checked;
        }

        private void chkRandom_CheckedChanged(object sender, EventArgs e)
        {
            rand = chkRandom.Checked;
        }
    }
}
