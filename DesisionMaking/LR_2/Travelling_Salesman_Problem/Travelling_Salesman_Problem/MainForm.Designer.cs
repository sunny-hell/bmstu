namespace Travelling_Salesman_Problem
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chklstCities = new System.Windows.Forms.CheckedListBox();
            this.tblCosts = new System.Windows.Forms.DataGridView();
            this.btnCreateTable = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.lblTotalCost = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tblCosts)).BeginInit();
            this.SuspendLayout();
            // 
            // chklstCities
            // 
            this.chklstCities.FormattingEnabled = true;
            this.chklstCities.Location = new System.Drawing.Point(12, 12);
            this.chklstCities.Name = "chklstCities";
            this.chklstCities.Size = new System.Drawing.Size(172, 289);
            this.chklstCities.TabIndex = 0;
            // 
            // tblCosts
            // 
            this.tblCosts.AllowUserToAddRows = false;
            this.tblCosts.AllowUserToDeleteRows = false;
            this.tblCosts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblCosts.Location = new System.Drawing.Point(272, 34);
            this.tblCosts.Name = "tblCosts";
            this.tblCosts.Size = new System.Drawing.Size(516, 274);
            this.tblCosts.TabIndex = 1;
            // 
            // btnCreateTable
            // 
            this.btnCreateTable.Location = new System.Drawing.Point(202, 46);
            this.btnCreateTable.Name = "btnCreateTable";
            this.btnCreateTable.Size = new System.Drawing.Size(48, 35);
            this.btnCreateTable.TabIndex = 2;
            this.btnCreateTable.Text = "->";
            this.btnCreateTable.UseVisualStyleBackColor = true;
            this.btnCreateTable.Click += new System.EventHandler(this.btnCreateTable_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(272, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Стоимости проезда между городами";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(13, 317);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(131, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Построить маршрут";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtResult
            // 
            this.txtResult.Enabled = false;
            this.txtResult.Location = new System.Drawing.Point(162, 317);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(626, 20);
            this.txtResult.TabIndex = 5;
            // 
            // lblTotalCost
            // 
            this.lblTotalCost.AutoSize = true;
            this.lblTotalCost.Location = new System.Drawing.Point(162, 344);
            this.lblTotalCost.Name = "lblTotalCost";
            this.lblTotalCost.Size = new System.Drawing.Size(0, 13);
            this.lblTotalCost.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 371);
            this.Controls.Add(this.lblTotalCost);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCreateTable);
            this.Controls.Add(this.tblCosts);
            this.Controls.Add(this.chklstCities);
            this.Name = "MainForm";
            this.Text = "Построение оптимального маршрута";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tblCosts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chklstCities;
        private System.Windows.Forms.DataGridView tblCosts;
        private System.Windows.Forms.Button btnCreateTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label lblTotalCost;
    }
}