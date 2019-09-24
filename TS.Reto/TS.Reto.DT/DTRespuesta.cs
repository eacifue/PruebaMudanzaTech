using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.Reto.DT
{
    public class DTRespuesta
    {

        private string caso = string.Empty;


        public string Casos
        {
            get
            {
                return this.caso;
            }
            set
            {
                this.caso = value;
            }
        }

    }
}
