using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace TesteVigilante {
    static class USBCfg {
        public delegate void OnLog(string s);
        public static OnLog LogEvent;

        static void Log(string txt) { if(LogEvent != null) LogEvent(txt); }
        static void Log(string fmt, params object[] args) { Log(String.Format(fmt, args)); }

        public static bool Config(string id)
        {
            bool write = false;
            using(USBManufacturing usb = new USBManufacturing()) {
                //Log("Lendo configurações USB");
                int ndevices = usb.GetNumDevices();
                if(ndevices == 0) {
                    Log("Nenhum dispositivo usb foi detectado");
                    return false;
                } else {
                    //sempre pega o primeiro device encontrado!
                    usb.DeviceNumber = 0;
                }
                //Log("dispositivo: {0}", usb.GetProductFullPath());
                if(usb.GetLockValue() != 0) {
                    Log("Este dispositivo USB está bloqueado. Configuração não está disponivel");
                    return false;
                }
                if(!id.Equals(usb.GetDeviceProductString())){
                    write = true;
                    usb.SetProductString(id);
                }
                const UInt16 pid = 0xEA60; // 0x8526;  << using silabs id for the signed drivers
                if(usb.GetDevicePid() != pid){
                    write = true;
                    usb.SetPID(pid);
                }
                //usb.SetLockValue();
                if(write) {
                    usb.Reset();
                    Log("Programando USB para {0}", id);
                    System.Threading.Thread.Sleep(1500); //tempo para voltar do reset
                } else {
                    Log("Não é necessário atualizar as configurações da USB");
                }
                return true;
            }
        }
    }
}
