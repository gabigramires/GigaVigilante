using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TesteVigilante
{
    class AtivaWifi: Estado
    {
        public AtivaWifi(IStateMachine g) : base(g) { }
        Queue<string> commands = new Queue<string>();
        public override void AdicionaComandos()
        {
            owner.PromptLinux = false;
            // for (int i = 0; i < 2; i++)
            commands.Enqueue("AT+CWJAP_DEF=\"TesteHW\",\" \"\r\n");
            commands.Enqueue("AT+CIPSTA_DEF=\"192.168.4.3\",\"192.168.4.1 \",\"255.255.255.0\"\r\n");

        }
        public override void EnviaComando()
        {
            Thread.Sleep(300);
            SendCommand(commands.Dequeue());
        }
        public override Estado OnEnd()
        {
            return new TesteWifi(owner);
        }
        public override void DecodeLine(string message)
        {

            if (message.StartsWith("WIFI CONNECTED"))
            {
                owner.PromptLinux = true;
            }
        }
    }
}

