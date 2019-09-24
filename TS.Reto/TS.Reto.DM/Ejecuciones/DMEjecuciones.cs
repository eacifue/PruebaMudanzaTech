using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Reto.DM.DataAccessObjects;
using TS.Reto.DM.Resources;
using TS.Reto.DT;

namespace TS.Reto.DM.Ejecuciones
{
    public class DMEjecuciones
    {

        public int InsertarEjecicion(DTEjecucion ejecucion)
        {
            int _Resultado;
            ManagerDM gestorDB = new ManagerDM(nomeCadena.TS);
            List<Parameter> listParam = new List<Parameter>();
            listParam.Add(new Parameter("@Cedula", ejecucion.Cedula));
            listParam.Add(new Parameter("@Traza", ejecucion.Traza));

            _Resultado = (int)gestorDB.EjecutarScalar(DMResources.Insertar_Ejecucion, listParam);
            return _Resultado;

        }

    }
}
