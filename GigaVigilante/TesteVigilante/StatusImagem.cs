using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Resources;

namespace TesteVigilante
{
    public enum ImageTag
    {
        None,
        Wait,
        Ok,
        Error,
    }
    public static class StatusImagem
    {
        public static Bitmap Image(this ImageTag i)
        {
            switch (i)
            {
                case ImageTag.Wait: return Properties.Resources.s_wait;
                case ImageTag.Ok: return Properties.Resources.s_ok;
                case ImageTag.Error: return Properties.Resources.s_error;
            }
            return null;
        }      
    }
   
}
