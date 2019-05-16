using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Tools;

namespace TesteVigilante
{ 
        public abstract class Estado 
       {
            protected IStateMachine owner;
            public Estado(IStateMachine g) { owner = g; }
            public virtual void AdicionaComandos() { }
            public virtual void EnviaComando() { }
            public virtual Estado OnEnd() { return null; }
            public virtual void Inicia() { }
           public virtual void RecebeConfig(int n, string a, string b, string c) { }
            public virtual void RecebeId(int num) { }
            public virtual void DecodeLine(string message) { }
            protected void SendCommand(string cmd) { Terminal.WriteLine(cmd); }
            protected void SendCommand(string fmt, params object[] args) { SendCommand(String.Format(fmt, args)); }
        }
}
