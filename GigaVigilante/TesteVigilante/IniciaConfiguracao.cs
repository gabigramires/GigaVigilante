using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools;

namespace TesteVigilante
{
    class IniciaConfiguracao : Estado
    {
        public IniciaConfiguracao(IStateMachine g) : base(g) { }
        Queue<string> comands = new Queue<string>();
        public override void AdicionaComandos()
        {
            owner.PromptLinux = false;
            comands.Enqueue("AT+RFPOWER=01\r\n");
            comands.Enqueue("AT+SYSIOSETCFG=12,3,1\r\n");
            comands.Enqueue("AT+SYSIOSETCFG=13,3,1\r\n");
            comands.Enqueue("AT+SYSIOSETCFG=14,3,1\r\n");
            comands.Enqueue("AT+SYSIOSETCFG=15,3,1\r\n");
            comands.Enqueue("AT+SYSGPIODIR=12,1\r\n");
            comands.Enqueue("AT+SYSGPIODIR=13,1\r\n");
            comands.Enqueue("AT+SYSGPIODIR=14,1\r\n");
            comands.Enqueue("AT+SYSGPIODIR=15,1\r\n");
            comands.Enqueue("AT+SYSGPIODIR=4,0\r\n");
            comands.Enqueue("AT+SYSGPIODIR=5,0\r\n");
            comands.Enqueue("AT+CWMODE_DEF=1\r\n");

        }
        public override void EnviaComando()
        {
            Thread.Sleep(200);
            SendCommand(comands.Dequeue());
        }
        public override Estado OnEnd()
        {
            return new VerificaSensorCaixa(owner);
        }
       

        public override void DecodeLine(string message) 
        { 
            string at_final = "AT+SYSGPIODIR=5,0\r";
                if (comands.Count != 0)
                    EnviaComando();
                if (message.StartsWith(at_final))
                {
                   // owner.ShowMessage("Verifica Sensor da porta da Jiga");
                    owner.PromptLinux = true;
                }
                else
                    return;
        }
    }
}
