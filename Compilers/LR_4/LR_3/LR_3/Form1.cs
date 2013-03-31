using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LR_4
{
    public partial class Form1 : Form
    {
        private Dispatcher _dispatcher;
        
        public Form1()
        {
            InitializeComponent();
            _dispatcher = new Dispatcher();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _dispatcher.loadGrammar(ofd.FileName);
                rtbGrammar.Text = _dispatcher.printGrammar();
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCheckString_Click(object sender, EventArgs e)
        {
            Analyzer analyzer;
            if (rdbRecursive.Checked)
                analyzer = new DescentRecursiveAnalyzer();
            else
            {
                Grammar g = _dispatcher.getGrammar();
                analyzer = new DescendingBacktrackAnalyzer(g);
            }
            
            try
            {
                if (_dispatcher.analyze(txtInputString.Text, analyzer))
                    lblResult.Text = "Цепочка порождается данной грамматикой.";
                else
                    lblResult.Text = "Цепочка не порождается данной грамматикой.";
                rtbHistory.Text = _dispatcher.getHistory();
            }
            catch (FormatException fex)
            {
                MessageBox.Show("Произошла следующая ошибка: " + fex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (BadArgumentException baex)
            {
                MessageBox.Show("Произошла следующая ошибка: " + baex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bthSaveHistory_Click(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                _dispatcher.saveHistory(sfd.FileName);
            }
        }

        private void rdbRecursive_CheckedChanged(object sender, EventArgs e)
        {
            btnLoad.Enabled = !rdbRecursive.Checked;
            if (rdbRecursive.Checked)
            {
                Grammar g = new DescentRecursiveAnalyzer().getGrammar();
                rtbGrammar.Text = g.ToString();
            }

        }

    }
}
