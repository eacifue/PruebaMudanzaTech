using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.Reto.DT
{
    public class DTEjecucion
    {

        private string cedula = string.Empty;
        private string traza = string.Empty;


        public string Cedula
        {
            get
            {
                return this.cedula;
            }
            set
            {
                this.cedula = value;
            }
        }

        public string Traza
        {
            get
            {
                return this.traza;
            }
            set
            {
                this.traza = value;
            }
        }




    }
}
