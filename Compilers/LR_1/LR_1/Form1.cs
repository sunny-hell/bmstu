using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LR_1
{
    public partial class Form1 : Form
    {
        Dispatcher disp = new Dispatcher();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            disp.buildNonDeterministicFA(txtStringForFA.Text);
            string expr = ExpressionTransformer.transformToPostfixNotation(txtStringForFA.Text);
            txtInputString.Text = expr;
            sfd.Title = "Сохранить недетерминированный автомат как";
            if (sfd.ShowDialog() == DialogResult.OK)
                disp.saveNonDeterministicFA(sfd.FileName);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //string fname_nonDet, fname_det;
            //sfd.Title = "Сохранить недетерминированный автомат как";
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    fname_nonDet = sfd.FileName;
            //    sfd.Title = "Сохранить детерминированный автомат как";
            //    if (sfd.ShowDialog() == DialogResult.OK)
            //    {
            //        fname_det = sfd.FileName;
            //        disp.saveAll(fname_nonDet, fname_det);
            //    }
            //}
        }

        private void btnDetermineFA_Click(object sender, EventArgs e)
        {
            disp.buildDeterminsticFA();
            sfd.Title = "Сохранить детерминированный автомат как";
            if (sfd.ShowDialog() == DialogResult.OK)
                disp.saveDeterministicFA(sfd.FileName);
                
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            disp.minimizeDeterministicFA();
            sfd.Title = "Сохранить детерминированный автомат с минимальным числом состояний как";
            if (sfd.ShowDialog() == DialogResult.OK)
                disp.saveMinDeterministicFA(sfd.FileName);
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            disp.buildAll(txtStringForFA.Text);
            string[] fnames = { @"..\..\fa.gv", @"..\..\fa_det.gv", @"..\..\fa_det_min.gv" };
            disp.saveAll(fnames);
            lblResult.Text = "";
        }

        private void btnCheckString_Click(object sender, EventArgs e)
        {
            if (disp.checkString(txtInputString.Text))
                lblResult.Text = "Данная цепочка допускается автоматом";
            else
                lblResult.Text = "Данная цепочка не допускается автоматом";

        }

        private void txtInputString_TextChanged(object sender, EventArgs e)
        {
            lblResult.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
