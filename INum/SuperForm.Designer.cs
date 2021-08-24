
namespace INum
{
    partial class SuperForm
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
            this.button = new System.Windows.Forms.Button();
            this.tb_prefix = new System.Windows.Forms.TextBox();
            this.tb_eqp = new System.Windows.Forms.TextBox();
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
            // button
            // 
            this.button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button.BackColor = System.Drawing.SystemColors.Highlight;
            this.button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button.ForeColor = System.Drawing.Color.PapayaWhip;
            this.button.Location = new System.Drawing.Point(182, 154);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(220, 45);
            this.button.TabIndex = 0;
            this.button.Text = "РЕДАКТИРОВАТЬ";
            this.button.UseVisualStyleBackColor = false;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // tb_prefix
            // 
            this.tb_prefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_prefix.Location = new System.Drawing.Point(16, 50);
            this.tb_prefix.Name = "tb_prefix";
            this.tb_prefix.Size = new System.Drawing.Size(100, 26);
            this.tb_prefix.TabIndex = 1;
            // 
            // tb_eqp
            // 
            this.tb_eqp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_eqp.Location = new System.Drawing.Point(138, 50);
            this.tb_eqp.Name = "tb_eqp";
            this.tb_eqp.Size = new System.Drawing.Size(140, 26);
            this.tb_eqp.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Префикс:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(135, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Оборудование:";
            // 
            // tb_suffix
            // 
            this.tb_suffix.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_suffix.Location = new System.Drawing.Point(300, 50);
            this.tb_suffix.Name = "tb_suffix";
            this.tb_suffix.Size = new System.Drawing.Size(100, 26);
            this.tb_suffix.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(297, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Суффикс:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(13, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Нач.номер:";
            // 
            // nm
            // 
            this.nm.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nm.Location = new System.Drawing.Point(16, 153);
            this.nm.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nm.Name = "nm";
            this.nm.Size = new System.Drawing.Size(100, 38);
            this.nm.TabIndex = 5;
            this.nm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nm.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // buttonС
            // 
            this.buttonС.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonС.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.buttonС.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonС.ForeColor = System.Drawing.Color.PapayaWhip;
            this.buttonС.Location = new System.Drawing.Point(182, 99);
            this.buttonС.Name = "buttonС";
            this.buttonС.Size = new System.Drawing.Size(220, 45);
            this.buttonС.TabIndex = 0;
            this.buttonС.Text = "СОЗДАТЬ";
            this.buttonС.UseVisualStyleBackColor = false;
            this.buttonС.Click += new System.EventHandler(this.button_Click);
            // 
            // SuperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(414, 211);
            this.Controls.Add(this.nm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_suffix);
            this.Controls.Add(this.tb_eqp);
            this.Controls.Add(this.tb_prefix);
            this.Controls.Add(this.buttonС);
            this.Controls.Add(this.button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SuperForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Автонумеровка";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.nm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button;
        
        
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

        public System.Windows.Forms.TextBox tb_prefix;
        public System.Windows.Forms.TextBox tb_eqp;
        public System.Windows.Forms.TextBox tb_suffix;
        public System.Windows.Forms.NumericUpDown nm;
        private System.Windows.Forms.Button buttonС;
    }
}