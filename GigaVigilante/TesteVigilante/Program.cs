using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace TesteVigilante
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) => {
                string resource = "TesteVigilante." + new AssemblyName(args.Name).Name + ".dll";
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    byte[] data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                    return Assembly.Load(data);
                }
            };
            DelayedMain();
        }

        static void DelayedMain()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TelaVigilante());
        }



        /*  static void Main()
          {
              Application.EnableVisualStyles();
              Application.SetCompatibleTextRenderingDefault(false);
              Application
              //Application.Run(new TesteVigilante());
          }*/
    }
}
