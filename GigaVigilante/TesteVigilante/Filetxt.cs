using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TesteVigilante {
    internal class Filetxt {
        FileStream fs;
        StreamWriter sw;
        private string get_file(string path, string filename)
        {
            string f = $@"{path}\{filename}.txt";
            int n = 1;
            while (File.Exists(f))
                f = $@"{path}\{filename}-{++n}.txt";
            return f;
        }

        public void Escreve(string path, string filename)
        {
            fs = new FileStream(get_file(path, filename), FileMode.Create);
            sw = new StreamWriter(fs);
            sw.WriteLine("Testando");
            sw.Flush();
            sw.Close();
           

        }

        public void Close()
        {
            sw.Close();
            fs.Close();
        }
    }
}
