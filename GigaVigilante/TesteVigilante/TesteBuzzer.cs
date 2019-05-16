using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TesteVigilante
{
    class TesteBuzzer : Estado
    {
        public TesteBuzzer(IStateMachine g) : base(g) { }
        Queue<string> commands = new Queue<string>();
        public override void AdicionaComandos()
        {
            owner.PromptLinux = false;
          //  commands.Enqueue("AT+CIPCLOSE\r\n");
            for (int i = 0; i < 6; i++)
                commands.Enqueue("AT+SYSADC?\r\n");

        }
        public override void EnviaComando()
        {
            Thread.Sleep(300);
            SendCommand(commands.Dequeue());
        }
        public override Estado OnEnd()
        {
            return new StartTCPSerial(owner);
        }
        public override void DecodeLine(string message)
        {

            if (message.StartsWith("+SYSADC:"))
            {
                var arr = message.Split(':');
                var value = arr[1];
                int n = int.Parse(value);
                if (n > 170)
                {
                    owner.ShowMessage("teste buzzer");
                    owner.StatusBuzzer = true;
                    owner.PromptLinux = true;
                }
                else
                {

                    if (commands.Count != 0)
                        EnviaComando();
                    else
                    {
                        owner.StatusBuzzer = false;
                        owner.StatusErro = true;

                    }
                }
            }
            else
                return;

        }
    }
}
