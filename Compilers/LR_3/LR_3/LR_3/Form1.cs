using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LR_3
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
            try
            {
                if (_dispatcher.analyze(txtInputString.Text))
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

    }
}
