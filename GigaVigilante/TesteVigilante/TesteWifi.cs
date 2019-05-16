using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TesteVigilante
{
    class TesteWifi : Estado
    {
        public TesteWifi(IStateMachine g) : base(g) { }
        Queue<string> commands = new Queue<string>();
        public override void AdicionaComandos()
        {
            owner.PromptLinux = false;
            // for (int i = 0; i < 2; i++)
            commands.Enqueue("AT+PING=\"192.168.4.1\"\r\n");

        }
        public override void EnviaComando()
        {
            Thread.Sleep(200);
            SendCommand(commands.Dequeue());
        }
        public override Estado OnEnd()
        {
            return new StartTCP(owner);
        }
        public override void DecodeLine(string message)
        {

            if (message.StartsWith("+"))
            {
                var arr = message.Split('+');
                var value = arr[1];
                int n = int.Parse(value);
                if(n > 1)
                { 
                    owner.StatusWifi = true;
                   // owner.Finish = true;
                    owner.PromptLinux = true;
                    owner.ShowMessage("PING OK");
                }
            }
        }
    }
}
