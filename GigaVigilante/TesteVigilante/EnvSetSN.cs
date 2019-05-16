using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;

namespace TesteVigilante
{

        public class EnvSetSN 
        {
            string value;
            public void ReceiveSerialNumber(string num)
            {
                    value = num;
                    Terminal.Write(num);
            }

            public string GetSerialNumber()
            {

                    return value; ;
            }
        }
}


