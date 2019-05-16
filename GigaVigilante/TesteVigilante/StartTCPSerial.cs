using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TesteVigilante
{
    class StartTCPSerial: Estado
    {
        public StartTCPSerial(IStateMachine g) : base(g) { }
        Queue<string> commands = new Queue<string>();
        public override void AdicionaComandos()
        {
            owner.PromptLinux = false;
            commands.Enqueue("AT+CIPSTART=\"TCP\",\"192.168.4.1\",80\r\n");

        }
        public override void EnviaComando()
        {
            Thread.Sleep(2000);
            SendCommand(commands.Dequeue());
        }
        public override Estado OnEnd()
        {
                return new GravaSerialNumber(owner);
           
        }
        public override void DecodeLine(string message)
        {
            if (commands.Count != 0)
                EnviaComando();
            if (message.StartsWith("CONNECT"))
            {
                owner.ShowMessage("starttcp ok ");
                owner.PromptLinux = true;

            }
        }
    }
}
