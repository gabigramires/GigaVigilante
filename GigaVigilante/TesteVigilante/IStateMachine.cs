using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteVigilante
{
        public interface IStateMachine
        {
        // HWInfoData hwinfo { get; }
            bool Finish { get; set; }
            bool FonteCurto { get; set; }
            bool StatusFonte { get; set; }
            bool StatusBuzzer { get; set; }
            bool StatusWifi { get; set; }
            bool PromptLinux { get; set; }
            bool StatusErro { get; set; }
            void ShowMessage(string message);
            int NumSerial { get; set; }
        }
}
