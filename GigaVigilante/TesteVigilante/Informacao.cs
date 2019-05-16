using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesteVigilante
{
    public partial class Informacao : Form
    {
        string caminho;
        public Informacao(Form owner)
        {
            this.Owner = owner;
            InitializeComponent();
        }
        public string Operador { get { return textBox1.Text; } }

        private void Informacao_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Operador != Config.Cfg.operador)
            {
                Config.Cfg.operador = Operador;
                Config.Save();
            }
        }

        private void okbutton1_Click(object sender, EventArgs e)
        {
            Config.Cfg.relat_path = textBox1.Text;
            Config.Save();
            this.DialogResult = DialogResult.OK;

        }
    }
}
public class Config
{
    public string comport;
    public int speed;
    public string operador;
    public string relat_path;
    public static Config Cfg = new Config();


    private Config()
    {
        comport = "COM6";
        speed = 115200;
        initPaths();
    }

    private void initPaths()
    {
    }
    public static void Save()
    {
    }
}



