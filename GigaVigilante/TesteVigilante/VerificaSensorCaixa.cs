using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools;

namespace TesteVigilante
{
    class VerificaSensorCaixa : Estado
    {
        public VerificaSensorCaixa(IStateMachine g) : base(g) { }
        Queue<string> comands = new Queue<string>();
        public override void AdicionaComandos()
        {
            owner.PromptLinux = false;
            comands.Enqueue("AT+SYSGPIOREAD=4\r\n");
            
        }
        public override  void EnviaComando()
        {
            SendCommand(comands.Dequeue());
        }
        public override Estado OnEnd()
        {
           return  null;
        }
        public override void DecodeLine(string message)
        {
            if (comands.Count != 0)
                EnviaComando();
            if (message.StartsWith("+SYSGPIOREAD:4,0,0") /*|| message.StartsWith("+SYSGPIOREAD:4,0,0")*/)
            {
                    owner.ShowMessage("Click Start");
                    owner.PromptLinux = true;
            }
           else if (message.StartsWith("+SYSGPIOREAD:4,0,1"))
           {
                owner.ShowMessage("Close the Door");
                AdicionaComandos();
                EnviaComando();
            }
            else
                return;
        }
    }
}
