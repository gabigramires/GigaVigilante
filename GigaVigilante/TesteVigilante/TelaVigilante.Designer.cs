using Tools;
namespace TesteVigilante
{
    partial class TelaVigilante
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaVigilante));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.vigilantePanel3 = new TesteVigilante.VigilantePanel();
            this.vigilantePanel8 = new TesteVigilante.VigilantePanel();
            this.vigilantePanel2 = new TesteVigilante.VigilantePanel();
            this.vigilantePanel6 = new TesteVigilante.VigilantePanel();
            this.vigilantePanel4 = new TesteVigilante.VigilantePanel();
            this.vigilantePanel1 = new TesteVigilante.VigilantePanel();
            this.vigilantePanel5 = new TesteVigilante.VigilantePanel();
            this.vigilantePanel7 = new TesteVigilante.VigilantePanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.testeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvarRelatórioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox9.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 46);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Serial Port:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "img";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(379, 42);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(109, 33);
            this.button3.TabIndex = 6;
            this.button3.Text = "Terminal";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Terminal);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.comboBox1);
            this.groupBox9.Controls.Add(this.label1);
            this.groupBox9.Location = new System.Drawing.Point(28, 40);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(158, 82);
            this.groupBox9.TabIndex = 20;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Config Serial";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(545, 42);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(199, 55);
            this.button2.TabIndex = 29;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Start);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.41209F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.08399F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.41993F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.vigilantePanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.vigilantePanel8, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.vigilantePanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.vigilantePanel6, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.vigilantePanel4, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.vigilantePanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.vigilantePanel5, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.vigilantePanel7, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(28, 143);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.354F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.646F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(893, 375);
            this.tableLayoutPanel1.TabIndex = 38;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maskedTextBox1.Location = new System.Drawing.Point(17, 34);
            this.maskedTextBox1.Mask = "000000";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(72, 20);
            this.maskedTextBox1.TabIndex = 39;
            this.maskedTextBox1.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.maskedTextBox1_MaskInputRejected);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.maskedTextBox1);
            this.groupBox1.Location = new System.Drawing.Point(207, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(140, 82);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial Number Start";
            // 
            // vigilantePanel3
            // 
            this.vigilantePanel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.vigilantePanel3.Image = false;
            this.vigilantePanel3.Image1 = false;
            this.vigilantePanel3.Image2 = false;
            this.vigilantePanel3.Location = new System.Drawing.Point(221, 3);
            this.vigilantePanel3.Name = "vigilantePanel3";
            this.vigilantePanel3.Numero = 3;
            this.vigilantePanel3.Size = new System.Drawing.Size(158, 170);
            this.vigilantePanel3.TabIndex = 32;
            // 
            // vigilantePanel8
            // 
            this.vigilantePanel8.BackColor = System.Drawing.SystemColors.ControlLight;
            this.vigilantePanel8.Image = false;
            this.vigilantePanel8.Image1 = false;
            this.vigilantePanel8.Image2 = false;
            this.vigilantePanel8.Location = new System.Drawing.Point(672, 188);
            this.vigilantePanel8.Name = "vigilantePanel8";
            this.vigilantePanel8.Numero = 8;
            this.vigilantePanel8.Size = new System.Drawing.Size(163, 168);
            this.vigilantePanel8.TabIndex = 37;
            // 
            // vigilantePanel2
            // 
            this.vigilantePanel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.vigilantePanel2.Image = false;
            this.vigilantePanel2.Image1 = false;
            this.vigilantePanel2.Image2 = false;
            this.vigilantePanel2.Location = new System.Drawing.Point(3, 188);
            this.vigilantePanel2.Name = "vigilantePanel2";
            this.vigilantePanel2.Numero = 2;
            this.vigilantePanel2.Size = new System.Drawing.Size(155, 168);
            this.vigilantePanel2.TabIndex = 31;
            // 
            // vigilantePanel6
            // 
            this.vigilantePanel6.BackColor = System.Drawing.SystemColors.ControlLight;
            this.vigilantePanel6.Image = false;
            this.vigilantePanel6.Image1 = false;
            this.vigilantePanel6.Image2 = false;
            this.vigilantePanel6.Location = new System.Drawing.Point(445, 188);
            this.vigilantePanel6.Name = "vigilantePanel6";
            this.vigilantePanel6.Numero = 6;
            this.vigilantePanel6.Size = new System.Drawing.Size(172, 168);
            this.vigilantePanel6.TabIndex = 35;
            // 
            // vigilantePanel4
            // 
            this.vigilantePanel4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.vigilantePanel4.Image = false;
            this.vigilantePanel4.Image1 = false;
            this.vigilantePanel4.Image2 = false;
            this.vigilantePanel4.Location = new System.Drawing.Point(221, 188);
            this.vigilantePanel4.Name = "vigilantePanel4";
            this.vigilantePanel4.Numero = 4;
            this.vigilantePanel4.Size = new System.Drawing.Size(158, 168);
            this.vigilantePanel4.TabIndex = 33;
            // 
            // vigilantePanel1
            // 
            this.vigilantePanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.vigilantePanel1.Image = false;
            this.vigilantePanel1.Image1 = false;
            this.vigilantePanel1.Image2 = false;
            this.vigilantePanel1.Location = new System.Drawing.Point(3, 3);
            this.vigilantePanel1.Name = "vigilantePanel1";
            this.vigilantePanel1.Numero = 1;
            this.vigilantePanel1.Size = new System.Drawing.Size(155, 170);
            this.vigilantePanel1.TabIndex = 30;
            // 
            // vigilantePanel5
            // 
            this.vigilantePanel5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.vigilantePanel5.Image = false;
            this.vigilantePanel5.Image1 = false;
            this.vigilantePanel5.Image2 = false;
            this.vigilantePanel5.Location = new System.Drawing.Point(445, 3);
            this.vigilantePanel5.Name = "vigilantePanel5";
            this.vigilantePanel5.Numero = 5;
            this.vigilantePanel5.Size = new System.Drawing.Size(172, 170);
            this.vigilantePanel5.TabIndex = 34;
            // 
            // vigilantePanel7
            // 
            this.vigilantePanel7.BackColor = System.Drawing.SystemColors.ControlLight;
            this.vigilantePanel7.Image = false;
            this.vigilantePanel7.Image1 = false;
            this.vigilantePanel7.Image2 = false;
            this.vigilantePanel7.Location = new System.Drawing.Point(672, 3);
            this.vigilantePanel7.Name = "vigilantePanel7";
            this.vigilantePanel7.Numero = 7;
            this.vigilantePanel7.Size = new System.Drawing.Size(163, 170);
            this.vigilantePanel7.TabIndex = 36;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1062, 24);
            this.menuStrip1.TabIndex = 42;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // testeToolStripMenuItem
            // 
            this.testeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salvarRelatórioToolStripMenuItem});
            this.testeToolStripMenuItem.Name = "testeToolStripMenuItem";
            this.testeToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.testeToolStripMenuItem.Text = "Teste";
            // 
            // salvarRelatórioToolStripMenuItem
            // 
            this.salvarRelatórioToolStripMenuItem.Name = "salvarRelatórioToolStripMenuItem";
            this.salvarRelatórioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.salvarRelatórioToolStripMenuItem.Text = "Salvar Relatório";
            this.salvarRelatórioToolStripMenuItem.Click += new System.EventHandler(this.salvarRelatórioToolStripMenuItem_Click);
            // 
            // TelaVigilante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1062, 530);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TelaVigilante";
            this.Text = "Teste Vigilante";
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button button2;
        public VigilantePanel vigilantePanel1;
        private VigilantePanel vigilantePanel2;
        private VigilantePanel vigilantePanel3;
        private VigilantePanel vigilantePanel4;
        private VigilantePanel vigilantePanel5;
        private VigilantePanel vigilantePanel6;
        private VigilantePanel vigilantePanel7;
        private VigilantePanel vigilantePanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Button button3;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salvarRelatórioToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

