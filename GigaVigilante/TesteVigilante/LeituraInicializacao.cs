using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools;

namespace TesteVigilante
    {
        class LeituraInicializacao : Estado
        {
            public LeituraInicializacao(IStateMachine g) : base(g) { }
            public override void Inicia() { }
            public override Estado OnEnd()
            {
                return new IniciaConfiguracao(owner);
            }
            public override void DecodeLine(string message)
            {

                    if (message.StartsWith("rl\0l\u009c"))
                    { 
                        // owner.ShowMessage("Click no Botão Inicializa Teste");
                         owner.PromptLinux = true;
                    }
                    else
                        return;
                }
            }
 }
