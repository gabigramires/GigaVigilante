using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Tools
{
    public static class Network
    {
        private static string local_ip = null;
        public static string GetLocalIP(Form owner, ITextInfo info)
        {
            if(local_ip == null) {
                if(!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()) {
                    info.Add(Images.Error, "Rede não disponível!");
                    return "0.0.0.0";
                }
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                if(host.AddressList.Length == 1) {
                    local_ip = host.AddressList[0].ToString();
                } else {
                    //o usuario tem que escolher a rede...
                    ChooseList c = new ChooseList(owner, "Escolha a rede local conectada no equipamento:");
                    foreach(IPAddress ip in host.AddressList) {
                        if(ip.ToString().IndexOf(":") == -1) //ignora IPv6
                            c.AddItem(ip.ToString());
                    }
                    if(c.Count > 1) {
                        owner.Invoke((Action)(() => c.ShowDialog()));
                        local_ip = c.SelectedItem;
                    } else {
                        local_ip = c.First;
                    }
                }
            }
            return local_ip;
        }
    }
}
