namespace TesteVigilante
{
    partial class HWInfo
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
            this.button1 = new System.Windows.Forms.Button();
            this.sernum = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lCode = new System.Windows.Forms.Label();
            this.lDesc = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(206, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "&OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // sernum
            // 
            this.sernum.Location = new System.Drawing.Point(126, 12);
            this.sernum.Mask = "0000000000 000000000";
            this.sernum.Name = "sernum";
            this.sernum.Size = new System.Drawing.Size(141, 20);
            this.sernum.TabIndex = 0;
            this.sernum.TextChanged += new System.EventHandler(this.sernum_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Número de Série:";
            // 
            // lCode
            // 
            this.lCode.AutoSize = true;
            this.lCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lCode.Location = new System.Drawing.Point(31, 45);
            this.lCode.Name = "lCode";
            this.lCode.Size = new System.Drawing.Size(101, 16);
            this.lCode.TabIndex = 9;
            this.lCode.Text = "801.0000.00-0";
            this.lCode.Visible = false;
            // 
            // lDesc
            // 
            this.lDesc.AutoSize = true;
            this.lDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lDesc.Location = new System.Drawing.Point(31, 64);
            this.lDesc.Name = "lDesc";
            this.lDesc.Size = new System.Drawing.Size(55, 13);
            this.lDesc.TabIndex = 10;
            this.lDesc.Text = "Descrição";
            this.lDesc.Visible = false;
            // 
            // HWInfo
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 106);
            this.ControlBox = false;
            this.Controls.Add(this.lDesc);
            this.Controls.Add(this.lCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sernum);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "HWInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Informações de Hardware";
            this.Load += new System.EventHandler(this.HWInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MaskedTextBox sernum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lCode;
        private System.Windows.Forms.Label lDesc;
    }
}