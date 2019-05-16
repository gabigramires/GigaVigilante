using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools;
using System.Threading;

namespace TesteVigilante
{
    class AtivaVigilante : Estado
    {
        string sel_0, sel_1, sel_2;
        int num;
        public AtivaVigilante(IStateMachine g) : base(g) { }
       // public AtivaVigilante(IStateMachine g, string a, string b, string c) : base(g) { sel_0 = a; sel_1 = b; sel_2 = c; }
        Queue<string> commands = new Queue<string>();
        public override void RecebeConfig(int n, string a, string b, string c)
        {
            sel_0 = a;
            sel_1 = b;
            sel_2 = c;
            num = n;
        }


        public override void AdicionaComandos()
        {
            owner.PromptLinux = false;
            var x = sel_0; var y = sel_1; var z = sel_2;
            commands.Enqueue("AT+SYSGPIOWRITE=15,0\r\n");
            commands.Enqueue($"AT+SYSGPIOWRITE=14,{x}\r\n");
            commands.Enqueue($"AT+SYSGPIOWRITE=12,{y}\r\n");
            commands.Enqueue($"AT+SYSGPIOWRITE=13,{z}\r\n");
            commands.Enqueue("AT+SYSGPIOWRITE=15,1\r\n");
        }
        public override void EnviaComando()
        {
            Thread.Sleep(500);
            SendCommand(commands.Dequeue());
        }
        public override Estado OnEnd()
        {
            return new AtivaFonte(owner);
        }
        public override void DecodeLine(string message)
        {
            if (commands.Count != 0)
                EnviaComando();
            if (message.StartsWith("AT+SYSGPIOWRITE=15,1\r"))
            {
               // owner.ShowMessage($"configurou vigilante {num}");
                owner.PromptLinux = true;
            }
            else
                return;
        }
    }
}
