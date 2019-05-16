using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TesteVigilante
{
    class SerialNumber
    {

        public SerialNumber(string sn)
        {
            Parse(sn);
        }

        public int Parse(string sn)
        {
            string pattern = @"^[0-9]{6}$";
            Match M = M = Regex.Match(sn, pattern);
            if (!M.Success)
            {
                    throw new ArgumentException("Número serial invalido!");             
            }
            else
            {
               int n = int.Parse(sn);
              // MessageBox.Show(n.ToString());
                return n;
            }
        }
    }
}
