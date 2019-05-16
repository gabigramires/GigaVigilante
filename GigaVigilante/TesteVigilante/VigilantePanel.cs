using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools;

namespace TesteVigilante
{
        public partial class VigilantePanel : UserControl
        {
            public VigilantePanel()
            {
                InitializeComponent();
            }
            private int Num;
            public int Numero
            {
                get { return Num; }
                set
                {
                    Num = value;
                    groupBox2.Text = $"Vigilante {Num}";
                }
            }
            public bool Image
            {
                get { return false; }
                set
                {
                    if (value)
                        pictureBox1.Image = Properties.Resources.s_ok;
                    else
                        pictureBox1.Image = Properties.Resources.s_error;
                }
            }
            public bool Image1
            {
                get { return false; }
                set
                {
                    if (value)
                        pictureBox2.Image = Properties.Resources.s_ok;
                    else
                        pictureBox2.Image = Properties.Resources.s_error;
                }
            }
            public bool Image2
            {
                get { return false; }
                set
                {
                    if (value)
                        pictureBox3.Image = Properties.Resources.s_ok;
                    else
                        pictureBox3.Image = Properties.Resources.s_error;
                }
            }
            public void  InicializaConfiguracaoVig()
            {
                //groupBox2.BackColor = Color.Red;
                //Image = false;
                Jiga.Instance.Finish = false;
                Jiga.Instance.RecebeIdVigilante(Num);

            }
            public bool VerificaEnable()
            {
                if (checkBox1.Checked)
                    return true;
                return false;

            }
        }
}
