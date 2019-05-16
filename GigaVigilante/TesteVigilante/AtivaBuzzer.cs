using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TesteVigilante
{
    class AtivaBuzzer : Estado
    {
        public AtivaBuzzer(IStateMachine g) : base(g) { }
        Queue<string> commands = new Queue<string>();
        public override void AdicionaComandos()
        {
            owner.PromptLinux = false;
            commands.Enqueue("AT+CIPSEND=123\r\n");
            commands.Enqueue("POST /setbuzzer HTTP/1.1\r\nContent-Type: application/x-www-form-urlencoded\r\nContent-Length: 9\r\n\r\ntime=10000\r\n\r\nargs count: 1");

        }
        public override void EnviaComando()
        {
            Thread.Sleep(500);
            SendCommand(commands.Dequeue());
        }
        public override Estado OnEnd()
        {
            return new TesteBuzzer(owner);
        }
        public override void DecodeLine(string message)
        {
           
            if (commands.Count != 0)
                EnviaComando();
            if (message.StartsWith("Connection: close"))
            {
                owner.PromptLinux = true;
            }
            else
                return;
           
        }
    }
}
