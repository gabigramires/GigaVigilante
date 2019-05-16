using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Tools;


namespace TesteVigilante
{
    public partial class TelaVigilante : Form, IShow
    {
        Filetxt doc;
        public TelaVigilante()
        {
            InitializeComponent();
            Jiga.Instance.Show = this;
            
            Jiga.Instance.AdicionaLista(vigilantePanel1);
            Jiga.Instance.AdicionaLista(vigilantePanel2);
            Jiga.Instance.AdicionaLista(vigilantePanel3);
            Jiga.Instance.AdicionaLista(vigilantePanel4);
            Jiga.Instance.AdicionaLista(vigilantePanel5);
            Jiga.Instance.AdicionaLista(vigilantePanel6);
            Jiga.Instance.AdicionaLista(vigilantePanel7);
            Jiga.Instance.AdicionaLista(vigilantePanel8);
            var x = SerialPort.GetPortNames();
            foreach (string s in x)
                comboBox1.Items.Add(s);
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;        
            Jiga.Instance.ConfigTerminal(comboBox1.Text, this);
            Jiga.Instance.LeituraConfig();
        }

        private void onError(string message)
        {
            throw new NotImplementedException();
        }

        private void trata_erro(string s)
        {
            MessageBox.Show(s);
        }

        private void Terminal(object sender, EventArgs e)
        {
            Jiga.Instance.ConfigTerminal(comboBox1.Text, this);
        }
        public void ShowMessage(string message)
        {
            
            Invoke((Action)(() =>
            {
                MessageBox.Show(message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }));
        }
 
        public void Start(object sender, EventArgs e)
        {         
            Jiga.Instance.AdicionaVigilante();
            Jiga.Instance.ConfiguraVigilante();
        }
       // public event Action<object, bool, EventArgs> SerialChanged;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // SerialChanged?.Invoke(this, ((MaskedTextBox)sender).MaskCompleted, e);
        }

        public void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            string serial = maskedTextBox1.Text;
            ShowMessage(serial);
            Jiga.Instance.VerificaSerialNumber(serial);
        }

        public void salvarRelatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {          
               Informacao S = new Informacao(this);
                if (S.ShowDialog() == DialogResult.OK && folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                 // doc = new Filetxt();
                // doc.Escreve(folderBrowserDialog1.SelectedPath, S.Operador);
                // doc.Close();*/
                Jiga.Instance.EscreveRelatorio(folderBrowserDialog1.SelectedPath, S.Operador);

            }
        }
    }
}
