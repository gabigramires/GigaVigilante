using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Tools;
using System.IO;


namespace TesteVigilante
{
    public interface IShow { void ShowMessage(string msg); }
    class Jiga : IStateMachine, IProtocol
    {
        Filetxt doc;
        private static Jiga instance = null;
        List<VigilantePanel> lista = new List<VigilantePanel>();
        SerialNumber sn;
       

        public static Jiga Instance
        {
            get
            {
                if (instance == null)
                    instance = new Jiga();
                return instance;
            }
        }
        Queue<VigilantePanel> q = new Queue<VigilantePanel>();
        private Estado current = null;
        private Terminal term;
        private int  numeroId;
        public int NumSerial { get; set; }
        public bool PromptLinux { get; set; } = false;
        public bool Finish { get; set; } = false;
        public bool FonteCurto { get; set; } = false;
        public bool StatusErro { get; set; } = false;
        public bool StatusFonte
        {
            get { return false; }
            set
            {
                lista[numeroId-1].Image = value ? true : false;
                if(value == false)
                    lista[numeroId - 1].groupBox2.BackColor = Color.Red;
            }
        }
        public bool StatusBuzzer
        {
            get { return false; }
            set
            {
                lista[numeroId-1].Image2 = value ? true : false;
            }
        }
        public bool StatusWifi
        {
            get { return false; }
            set
            {
                lista[numeroId - 1].Image1 = value ? true : false;
            }
        }
        public IShow Show { get; set; }
        public void AdicionaLista(VigilantePanel x)
        {
            lista.Add(x);
        }

        public void Decode(string msg)
        {
            bool is_prompt = false;
            if(Finish)
            {
                current = current.OnEnd();
                ConfiguraVigilante();
            }
            if (PromptLinux)
            {
                is_prompt = true;
            }
            if (is_prompt)
            {
                current = current.OnEnd();
                if (current != null)
                {
                    current.AdicionaComandos();
                    current.EnviaComando();
                }
            }
            else
            {
                current.DecodeLine(msg);
            }
        }

        public void AdicionaVigilante()
        {
            bool add = false;
            for (int i = 0; i < 8; i++)
            {
                add = lista[i].VerificaEnable();
                if (add == true)
                {
                    q.Enqueue(lista[i]);
                }
            }

        }
        public void ConfiguraVigilante()
        {
            if(StatusErro)
                lista[numeroId - 1].groupBox2.BackColor = Color.Red;
            if (Finish)
            {
                lista[numeroId - 1].groupBox2.BackColor = Color.Green;
               // GravaSerialNumber();
            }

            if (q.Count != 0)
            {
                q.Dequeue().InicializaConfiguracaoVig();
            }
        }
        public void ConfigTerminal(string x, Form f)
        {
            term = new Terminal(x, 115200, f, this);
            term.Show();
          
        }

        public void EscreveRelatorio(string path, string filename)
        {
            doc = new Filetxt();
            doc.Escreve(path, filename);
           // term.dump2doc(doc);
           /* fs = new FileStream(get_file(path, filename), FileMode.Create);
            sw = new StreamWriter(fs);
            sw.WriteLine("Testando");
            sw.Flush();
            sw.Close();*/
        }

        public void VerificaSerialNumber(string serial)
        {
           sn = new SerialNumber(serial);
           NumSerial = sn.Parse(serial);
        }
        public void GravaSerialNumber()
        {    
                var num = NumSerial.ToString();
               // MessageBox.Show($"grava serial  {num}");
                NumSerial = NumSerial + 1;         
        }
        public void LeituraConfig()
        {
            current = new LeituraInicializacao(this);
            current.Inicia();

        }
      /*  public void InicializaTeste()
        {
            current = new IniciaConfiguracao(this);
            current.AdicionaComandos();
            current.EnviaComando();
        }*/
      
        public void RecebeIdVigilante(int n)
        {
            numeroId = n;
            current = new AtivaVigilante(this);
            switch (n)
            {
                case 1:
                    current.RecebeConfig(1, "0", "1", "1");
                    current.AdicionaComandos();
                    current.EnviaComando();
                    break;
                case 2:
                    current.RecebeConfig(2,"1", "1", "1");
                    current.AdicionaComandos();
                    current.EnviaComando();
                    break;
                case 3:
                    current.RecebeConfig(3,"0", "0", "1");
                    current.AdicionaComandos();
                    current.EnviaComando();
                    break;
                case 4:
                    current.RecebeConfig(4,"1", "0", "1");
                    current.AdicionaComandos();
                    current.EnviaComando();
                    break;
                case 5:
                    current.RecebeConfig(5,"0", "1", "0");
                    current.AdicionaComandos();
                    current.EnviaComando();
                    break;
                case 6:
                    current.RecebeConfig(6,"1", "1", "0");
                    current.AdicionaComandos();
                    current.EnviaComando();
                    break;
                case 7:
                    current.RecebeConfig(7,"0", "0", "0");
                    current.AdicionaComandos();
                    current.EnviaComando();
                    break;
                case 8:
                    current.RecebeConfig(8,"1", "0", "0");
                    current.AdicionaComandos();
                    current.EnviaComando();
                    break;
                default:
                    break;
            };
        }
      /*  public void TestaBuzzer(int n)
        {
            current = new AtivaBuzzer(this);
            current.AdicionaComandos();
            current.EnviaComando();
        }
        public void TestaFonte(int n)
        {
            current = new AtivaFonte(this);
            current.RecebeId(n);
            current.AdicionaComandos();
            current.EnviaComando();
        }*/

        public bool PartialReader { get { return true; } }
        public void PartialRead(string msg, EventHandlingEventArgs e)
        {
          
        }
        public void ShowMessage(string message)
        {
            Show.ShowMessage(message);
        }
    }
}
