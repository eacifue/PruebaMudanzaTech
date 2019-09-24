using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TS.Reto.DT;
using TS.Reto.DM.Ejecuciones;

namespace TS.Reto.BM
{
    public class BMArchivo
    {


        public string CargarArchivo(StreamReader srBM, string cedula)
        {
            string ArchivoRespuesta = String.Empty;
            try
            {
                
                string archivo = srBM.ReadToEnd();
                archivo = archivo.Replace("\n", "\r");
                List<string> ListaLineas = archivo.Split('\r').ToList<string>();
                ListaLineas.Remove("");

                ArchivoRespuesta =  ProcesarArcivo(ListaLineas, cedula);

            }
            catch(Exception ex)
            {

            }

            return ArchivoRespuesta;
        }


        private string ProcesarArcivo(List<string> ListaTexto, string cedula)
        {


            DMEjecuciones objEjecucionesBD = new DMEjecuciones();
            List<int> objListaInt = new List<int>();
            
                
                objListaInt = ListaTexto.Select(x => Convert.ToInt32(x)).ToList();
                
            int T = 0;
                int Dias = objListaInt[0];
                int Wi;
                string Respuesta = String.Empty; ;
            try
            {
                if (Dias >= 1 && Dias <= 500)
                {
                    for (int i = 1; i < objListaInt.Count; i++)
                    {

                        T = T + 1;
                        int N = Convert.ToInt32(objListaInt[i].ToString());
                        List<int> objListaPesos = new List<int>();
                        if (N >= 1 && N <= 100)
                        {

                            for (Wi = i + 1; Wi <= (i + N); Wi++)
                            {

                                objListaPesos.Add(objListaInt[Wi]);

                            }

                            int ViajesDia = CalculoViajesDia(objListaPesos);
                            if(ViajesDia == -1)
                            {
                                return "uno de los pesos es mayor a 100";

                            }
                            else
                            {
                                string RespuestaDia = "Case #" + T + ":" + ViajesDia;
                                Respuesta = string.Concat(Respuesta, RespuestaDia, Environment.NewLine);

                            }

                        }
                        else
                        {

                            return "Los pesos no pueden sobrepasar el valor de 100";
                        }

                        i = Wi - 1;
                    }

                    

                }
                else
                {

                    return "Solo se pueden procesar 500 dias máximo";
                }

            }
            catch (Exception ex)
            {

            }

            DTEjecucion objEjecucion = new DTEjecucion();
            objEjecucion.Cedula = cedula;
            objEjecucion.Traza = Respuesta;
            int resultado = objEjecucionesBD.InsertarEjecicion(objEjecucion);
            if (resultado != 0)
                {
                return "No se pudo insetar la traza";
            }
            return Respuesta;

        }



        private static int CalculoViajesDia(List<int> ListaElementos)
        {

            int PesoMaximo = ListaElementos.Max();
            int peso = 0;
            int elementos = 1;
            int Viajes = 0;
            try
            {
                if (PesoMaximo < 100)
                {

                    ListaElementos.Remove(PesoMaximo);
                    while (peso < 50 && PesoMaximo < 50)
                    {
                        if (ListaElementos.Count == 0)
                        {
                            return 0;
                        }
                        else
                        {
                            int MenorPeso = ListaElementos.Min();
                            ListaElementos.Remove(MenorPeso);
                            elementos++;
                            peso = elementos * PesoMaximo;

                        }
                    }
                    Viajes++;
                    if (ListaElementos.Count > 0)
                    {
                        Viajes = Viajes + CalculoViajesDia(ListaElementos);
                    }

                }
                else
                {
                    return -1;

                }
            }
            catch(Exception ex)
            {

            }

            return Viajes;

        }



    }

    
}
