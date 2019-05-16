using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteVigilante
{
    class DesligaFonte : Estado
    {
        public DesligaFonte(IStateMachine g) : base(g) { }
        Queue<string> commands = new Queue<string>();
        public override void AdicionaComandos()
        {
            owner.PromptLinux = false;
            commands.Enqueue("AT+SYSGPIOWRITE=15,0\r\n");
        }
        public override void EnviaComando()
        {
            SendCommand(commands.Dequeue());
        }
        public override Estado OnEnd()
        {
            return null;
        }
        public override void DecodeLine(string message)
        {
            if (commands.Count != 0)
                EnviaComando();
            if (message.StartsWith("AT+SYSGPIOWRITE=15,0\r"))
            {
                owner.ShowMessage("Fonte Desligada desabilite o Vigilante em Vermelho e click Start");
                owner.PromptLinux = true;
            }
        }
    }
}
