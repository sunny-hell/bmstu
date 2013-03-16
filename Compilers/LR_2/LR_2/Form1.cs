using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LR_2
{
    public partial class Form1 : Form
    {
        private Dispatcher _dispatcher;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadGrammar_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
                try
                {
                    _dispatcher.loadGrammar(ofd.FileName);
                    rtbInitGrammar.Text = _dispatcher.printGrammar();
                }
                catch (FormatException fe) 
                {
                    MessageBox.Show(fe.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _dispatcher = new Dispatcher();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRemoveERules_Click(object sender, EventArgs e)
        {
            _dispatcher.transformToNonnullableGrammar();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Грамматика Nonnullable");
            sb.Append(_dispatcher.printGrammar());
            rtbOutput.Text = sb.ToString();
        }
        
    }
}
