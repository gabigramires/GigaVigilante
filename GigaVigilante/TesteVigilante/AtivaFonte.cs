using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TesteVigilante
{
    class AtivaFonte : Estado
    {
        public AtivaFonte(IStateMachine g) : base(g) { }
        Queue<string> commands = new Queue<string>();
        public override void AdicionaComandos()
        {
            owner.PromptLinux = false;
            for (int i = 0; i < 50; i++)
                commands.Enqueue("AT+SYSGPIOREAD=5\r\n");

        }
        public override void EnviaComando()
        {
            Thread.Sleep(100);
            SendCommand(commands.Dequeue());
        }
        public override Estado OnEnd()
        {
            if (owner.FonteCurto == true)
                return new DesligaFonte(owner);
            else
                return new AtivaWifi(owner);
        }
        public override void DecodeLine(string message)
        {
           
            if (message.StartsWith("+SYSGPIOREAD:5,0,0")) 
            {
                if (commands.Count != 0)
                    EnviaComando();
                else
                {
                    // owner.ShowMessage("teste Fonte");
                    owner.StatusFonte = true;
                    owner.PromptLinux = true;
                }
               
            }
            else if (message.StartsWith("+SYSGPIOREAD:5,0,1"))
            {
                owner.ShowMessage($"Fonte em curto desligue o Vigilante!");
                owner.StatusFonte = false;
                owner.FonteCurto = true;
                owner.PromptLinux = true;
            }
            return;
        }
    }
}
