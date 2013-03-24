namespace LR_3
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
            this.rtbGrammar = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.txtInputString = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCheckString = new System.Windows.Forms.Button();
            this.bthSaveHistory = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbHistory = new System.Windows.Forms.RichTextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbGrammar
            // 
            this.rtbGrammar.Location = new System.Drawing.Point(6, 19);
            this.rtbGrammar.Name = "rtbGrammar";
            this.rtbGrammar.ReadOnly = true;
            this.rtbGrammar.Size = new System.Drawing.Size(259, 402);
            this.rtbGrammar.TabIndex = 0;
            this.rtbGrammar.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoad);
            this.groupBox1.Controls.Add(this.rtbGrammar);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 458);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Грамматика";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(175, 427);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(90, 25);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Загрузить";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // ofd
            // 
            this.ofd.InitialDirectory = "\"../../\"";
            // 
            // txtInputString
            // 
            this.txtInputString.Location = new System.Drawing.Point(289, 31);
            this.txtInputString.Name = "txtInputString";
            this.txtInputString.Size = new System.Drawing.Size(182, 20);
            this.txtInputString.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(289, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Входная цепочка";
            // 
            // btnCheckString
            // 
            this.btnCheckString.Location = new System.Drawing.Point(289, 57);
            this.btnCheckString.Name = "btnCheckString";
            this.btnCheckString.Size = new System.Drawing.Size(126, 27);
            this.btnCheckString.TabIndex = 4;
            this.btnCheckString.Text = "Проверить цепочку";
            this.btnCheckString.UseVisualStyleBackColor = true;
            this.btnCheckString.Click += new System.EventHandler(this.btnCheckString_Click);
            // 
            // bthSaveHistory
            // 
            this.bthSaveHistory.Location = new System.Drawing.Point(440, 438);
            this.bthSaveHistory.Name = "bthSaveHistory";
            this.bthSaveHistory.Size = new System.Drawing.Size(159, 26);
            this.bthSaveHistory.TabIndex = 5;
            this.bthSaveHistory.Text = "Сохранить историю вывода";
            this.bthSaveHistory.UseVisualStyleBackColor = true;
            this.bthSaveHistory.Click += new System.EventHandler(this.bthSaveHistory_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(512, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Результат";
            // 
            // lblResult
            // 
            this.lblResult.AllowDrop = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblResult.ForeColor = System.Drawing.Color.Red;
            this.lblResult.Location = new System.Drawing.Point(512, 34);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(218, 50);
            this.lblResult.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtbHistory);
            this.groupBox2.Location = new System.Drawing.Point(295, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(435, 336);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Вывод";
            // 
            // rtbHistory
            // 
            this.rtbHistory.Location = new System.Drawing.Point(7, 20);
            this.rtbHistory.Name = "rtbHistory";
            this.rtbHistory.ReadOnly = true;
            this.rtbHistory.Size = new System.Drawing.Size(422, 310);
            this.rtbHistory.TabIndex = 0;
            this.rtbHistory.Text = "";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(627, 440);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(103, 23);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // sfd
            // 
            this.sfd.InitialDirectory = "\"../../\"";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 477);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bthSaveHistory);
            this.Controls.Add(this.btnCheckString);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInputString);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Нисходящий разбор с возвратами";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbGrammar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.TextBox txtInputString;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCheckString;
        private System.Windows.Forms.Button bthSaveHistory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.RichTextBox rtbHistory;
        private System.Windows.Forms.SaveFileDialog sfd;
    }
}

