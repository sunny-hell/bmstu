namespace LR_2
{
    partial class Form1
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
            this.btnLoadGrammar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtbInitGrammar = new System.Windows.Forms.RichTextBox();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.btnRemoveERules = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadGrammar
            // 
            this.btnLoadGrammar.Location = new System.Drawing.Point(7, 429);
            this.btnLoadGrammar.Name = "btnLoadGrammar";
            this.btnLoadGrammar.Size = new System.Drawing.Size(135, 23);
            this.btnLoadGrammar.TabIndex = 0;
            this.btnLoadGrammar.Text = "Загрузить";
            this.btnLoadGrammar.UseVisualStyleBackColor = true;
            this.btnLoadGrammar.Click += new System.EventHandler(this.btnLoadGrammar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtbInitGrammar);
            this.groupBox1.Controls.Add(this.btnLoadGrammar);
            this.groupBox1.Location = new System.Drawing.Point(24, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 458);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Исходная грамматика";
            // 
            // rtbInitGrammar
            // 
            this.rtbInitGrammar.Location = new System.Drawing.Point(7, 20);
            this.rtbInitGrammar.Name = "rtbInitGrammar";
            this.rtbInitGrammar.ReadOnly = true;
            this.rtbInitGrammar.Size = new System.Drawing.Size(245, 394);
            this.rtbInitGrammar.TabIndex = 1;
            this.rtbInitGrammar.Text = "";
            // 
            // ofd
            // 
            this.ofd.InitialDirectory = "\"../../\"";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtbOutput);
            this.groupBox2.Location = new System.Drawing.Point(327, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 458);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Результат";
            // 
            // rtbOutput
            // 
            this.rtbOutput.Location = new System.Drawing.Point(7, 20);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.ReadOnly = true;
            this.rtbOutput.Size = new System.Drawing.Size(266, 432);
            this.rtbOutput.TabIndex = 0;
            this.rtbOutput.Text = "";
            // 
            // btnRemoveERules
            // 
            this.btnRemoveERules.Location = new System.Drawing.Point(327, 476);
            this.btnRemoveERules.Name = "btnRemoveERules";
            this.btnRemoveERules.Size = new System.Drawing.Size(141, 23);
            this.btnRemoveERules.TabIndex = 3;
            this.btnRemoveERules.Text = "Исключить е-правила";
            this.btnRemoveERules.UseVisualStyleBackColor = true;
            this.btnRemoveERules.Click += new System.EventHandler(this.btnRemoveERules_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(487, 476);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(129, 23);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 507);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRemoveERules);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoadGrammar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtbInitGrammar;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.Button btnRemoveERules;
        private System.Windows.Forms.Button btnExit;
    }
}

