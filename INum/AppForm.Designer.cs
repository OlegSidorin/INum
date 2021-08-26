
namespace INum
{
    partial class AppForm
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
            this.tb_prefix = new System.Windows.Forms.TextBox();
            this.tb_middle_part = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_suffix = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nm = new System.Windows.Forms.NumericUpDown();
            this.buttonС = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nm)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_prefix
            // 
            this.tb_prefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_prefix.Location = new System.Drawing.Point(12, 41);
            this.tb_prefix.Margin = new System.Windows.Forms.Padding(2);
            this.tb_prefix.Name = "tb_prefix";
            this.tb_prefix.Size = new System.Drawing.Size(76, 26);
            this.tb_prefix.TabIndex = 1;
            // 
            // tb_eqp
            // 
            this.tb_middle_part.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_middle_part.Location = new System.Drawing.Point(104, 41);
            this.tb_middle_part.Margin = new System.Windows.Forms.Padding(2);
            this.tb_middle_part.Name = "tb_eqp";
            this.tb_middle_part.Size = new System.Drawing.Size(106, 26);
            this.tb_middle_part.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(10, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Префикс:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(101, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Оборудование:";
            // 
            // tb_suffix
            // 
            this.tb_suffix.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_suffix.Location = new System.Drawing.Point(225, 41);
            this.tb_suffix.Margin = new System.Windows.Forms.Padding(2);
            this.tb_suffix.Name = "tb_suffix";
            this.tb_suffix.Size = new System.Drawing.Size(76, 26);
            this.tb_suffix.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(223, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Суффикс:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(9, 83);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Нач.номер:";
            // 
            // nm
            // 
            this.nm.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nm.Location = new System.Drawing.Point(13, 108);
            this.nm.Margin = new System.Windows.Forms.Padding(2);
            this.nm.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nm.Name = "nm";
            this.nm.Size = new System.Drawing.Size(98, 47);
            this.nm.TabIndex = 5;
            this.nm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nm.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonС
            // 
            this.buttonС.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonС.BackColor = System.Drawing.Color.Indigo;
            this.buttonС.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonС.ForeColor = System.Drawing.Color.PapayaWhip;
            this.buttonС.Location = new System.Drawing.Point(115, 99);
            this.buttonС.Margin = new System.Windows.Forms.Padding(2);
            this.buttonС.Name = "buttonС";
            this.buttonС.Size = new System.Drawing.Size(188, 61);
            this.buttonС.TabIndex = 0;
            this.buttonС.Text = "СОЗДАТЬ МАРКУ";
            this.buttonС.UseVisualStyleBackColor = false;
            this.buttonС.Click += new System.EventHandler(this.buttonС_Click);
            // 
            // SuperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(314, 171);
            this.Controls.Add(this.nm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_suffix);
            this.Controls.Add(this.tb_middle_part);
            this.Controls.Add(this.tb_prefix);
            this.Controls.Add(this.buttonС);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SuperForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "МСК_Маркировка";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.nm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

        public System.Windows.Forms.TextBox tb_prefix;
        public System.Windows.Forms.TextBox tb_middle_part;
        public System.Windows.Forms.TextBox tb_suffix;
        public System.Windows.Forms.NumericUpDown nm;

        private System.Windows.Forms.Button buttonС;
    }
}