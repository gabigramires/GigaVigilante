using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TesteVigilante
{
    class GravaSerialNumber: Estado
    {
        public GravaSerialNumber(IStateMachine g) : base(g) { }
        Queue<string> commands = new Queue<string>();
        string sn;

        public override void AdicionaComandos()
        {
            sn = owner.NumSerial.ToString();
            var x = sn;
            owner.PromptLinux = false;
            commands.Enqueue("AT+CIPSEND=112\r\n");
            commands.Enqueue($"POST /setsn HTTP/1.1\r\nContent-Type: application/x-www-form-urlencoded\r\nContent-Length: 9\r\n\r\nsn={x}\r\n\r\nargs count:1");
           
        }
        public override void EnviaComando()
        {
            Thread.Sleep(2000);
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
           int n;
            if (message.StartsWith("+IPD"))
            {
                var arr = message.Split(':');
                if (arr.Length > 0)
                {
                    string first = arr[0];
                    var arr2 = first.Split(',');
                    if (arr2.Length > 0)
                    {
                        var value = arr2[1];
                        n = int.Parse(value);
                        string http = arr[1];
                        if (n > 80 && http.Equals("HTTP/1.1 200 OK\r"))
                        {
                            owner.ShowMessage($"grava serial  ok {sn} ");
                            owner.Finish = true;
                            owner.PromptLinux = true;
                        }
                    }
                }

            }
            else
                return;
        }
    }
}

