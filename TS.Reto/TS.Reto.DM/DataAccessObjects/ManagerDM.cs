
namespace TS.Reto.DM.DataAccessObjects
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;
    using System.IO;
    using System.Linq;
    #region Enumerador
    /// <summary>
    /// Configuración del nombre de la cadena de conexión.
    /// </summary>
    public enum nomeCadena
    {
        TS,

    }
    #endregion
    internal class ManagerDM
    {


        #region Variables

        string nomeCadenaBancoDoDados;

        #endregion

        #region Constructores

        /// <summary>
        /// Asigna el nombre de la cadena de conexión cuando no se recibe como parametro.
        /// </summary>
        public ManagerDM()
        {
            nomeCadenaBancoDoDados = "strConnectionTS";
            baseDatos = new DatabaseProviderFactory().Create(ObtenerNombreCadenaBaseDatos(nomeCadenaBancoDoDados));
        }

        /// <summary>
        /// Asigna el nombre de la cadena de conexión cuando se recibe como parametro.
        /// </summary>
        /// //
        public ManagerDM(nomeCadena Cadena)
        {
            switch (Cadena)
            {
                case nomeCadena.TS:
                    nomeCadenaBancoDoDados = "strConnectionTS";
                    break;
            }
            baseDatos = new DatabaseProviderFactory().Create(ObtenerNombreCadenaBaseDatos(nomeCadenaBancoDoDados));
        }

        #endregion     

        #region Atributos

        ///// <summary>
        ///// Objeto para conectar a la base de datos.
        ///// </summary>       
        private Database baseDatos;

        ///// <summary>
        ///// Objeto para almacenar una transacción de base de datos.
        ///// </summary>
        private IDbTransaction transaccion;

        ///// <summary>
        /// Objeto para dar contexto a una transacción.
        /// </summary>
        private DbConnection connection;

        #endregion   

        #region Métodos
        /// <summary>
        /// Ejecuta un procedimiento almacenado.
        /// </summary>
        /// <param name="procedimiento">Nombre del procedimiento.</param>
        /// <returns>Retorna la cantidad de registros afectados.</returns>
        /// <author>"Alexander Gonzalez Valencia"</author>
        public int EjecutarNonQuery(string procedimiento, List<Parameter> listParametros)
        {
            DbCommand comando = PrepararComando(procedimiento) as DbCommand;
            EstablecerParametros(listParametros, comando);
            int result = baseDatos.ExecuteNonQuery(comando);
            ObtenerParametros(listParametros, comando);
            return result;
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado.
        /// </summary>
        /// <param name="procedimiento">Nombre del procedimiento.</param>
        /// <returns>Retorna un valor de tipo object.</returns>
        /// <author>"Alexander Gonzalez Valencia"</author>
        public object EjecutarScalar(string procedimiento, List<Parameter> listParametros)
        {
            object respuesta = null;
            DbCommand comando = PrepararComando(procedimiento) as DbCommand;
            EstablecerParametros(listParametros, comando);
            respuesta = baseDatos.ExecuteScalar(comando);
            ObtenerParametros(listParametros, comando);
            return respuesta;
        }



        /// <summary>
        /// Obtiene la cadena de conexión a la base de datos.
        /// </summary>
        /// <returns>Retorna el connection string de la base de datos.</returns>
        /// <author>"Alexander Gonzalez Valencia"</author>
        private static string ObtenerNombreCadenaBaseDatos(string nomeCadenaBancoDoDados)
        {
            return ConfigurationManager.ConnectionStrings[nomeCadenaBancoDoDados].Name;
        }

        /// <summary>
        /// Permite obtener el contexto de una transacción de FileStream.
        /// </summary>
        /// <returns>Retorna el contexto de la transacción para FileStream</returns>
        private byte[] ObtenerContextoFileStream()
        {
            using (SqlCommand objSqlCmdFS = new SqlCommand("SELECT GET_FILESTREAM_TRANSProjetoION_CONTEXT()", (SqlConnection)connection, (SqlTransaction)transaccion))
            {
                byte[] objContext = (byte[])objSqlCmdFS.ExecuteScalar();
                return objContext;
            }
        }

        /// <summary>
        /// Permite crear un objeto IDbCommand con el procedimiento que se suminitra.
        /// </summary>
        /// <returns>Retorna un comando.</returns>
        /// <param name="procedimiento">Nombre del procedimiento.</param>
        /// <returns></returns>
        /// <author>"Alexander Gonzalez Valencia"</author>
        private IDbCommand PrepararComando(string procedimiento)
        {
            DbCommand comando = baseDatos.GetStoredProcCommand(procedimiento);
            comando.CommandTimeout = int.Parse(ConfigurationManager.AppSettings["TimeoutBD"].ToString());
            return comando;
        }

        /// <summary>
        /// Estable los parametros de un comando de Base de datos
        /// </summary>
        /// <param name="listParametros"></param>
        /// <param name="cmd"></param>
        private static void EstablecerParametros(List<Parameter> listParametros, DbCommand cmd)
        {
            if (listParametros != null)
            {
                DbParameter param;
                for (int index = 0; index < listParametros.Count; index++)
                {
                    param = cmd.CreateParameter();
                    Parameter itemParam = listParametros[index];
                    param.ParameterName = itemParam.Nombre;
                    param.Value = itemParam.Valor;
                    if (itemParam.Direccion == ParameterDirection.Output || itemParam.Direccion == ParameterDirection.InputOutput)
                    {
                        param.Direction = itemParam.Direccion;
                        param.Size = itemParam.Tamano;
                    }
                    cmd.Parameters.Add(param);
                }
            }
        }

        /// <summary>
        /// Obtiene los parametros de salida de un procedimiento
        /// </summary>
        /// <param name="listParametros"></param>
        /// <param name="cmd"></param>
        private static void ObtenerParametros(List<Parameter> listParametros, DbCommand cmd)
        {
            if (listParametros != null)
            {
                for (int index = 0; index < listParametros.Count; index++)
                {
                    Parameter itemParam = listParametros[index];
                    if (itemParam.Direccion == ParameterDirection.Output || itemParam.Direccion == ParameterDirection.InputOutput)
                    {
                        itemParam.Valor = cmd.Parameters[index].Value;
                    }
                }
            }
        }
        #endregion

    }
}
