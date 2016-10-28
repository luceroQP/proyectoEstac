using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class Arriendo
    {
        public int cod_arriendo {get; set;}
        public DateTime inicio_arriendo { get; set; }
        public DateTime fin_arriendo { get; set; }
        public int horas_usadas { get; set; }
        public int cod_estacionamiento { get; set; }
        public int cod_vehiculo { get; set; }
        public Estacionamiento Estacionamiento {get; set;}
        public Vehiculo Vehiculo { get; set; }

        public int guardar(Arriendo arriendo)
        {
            Conexion conexion = new Conexion();
            int id = conexion.getSequenceValor("ARRIENDOS_SEQ", 1);
            conexion.cerrarConexion();

            string query = "insert into ARRIENDOS(COD_ARRIENDO, INICIO_ARRIENDO, FIN_ARRIENDO, HORAS_USADAS, COD_ESTACIONAMIENTO,COD_VEHICULO) values (";
            query += id + ",";
            if (arriendo.inicio_arriendo != default(DateTime))
            {
                query += " TO_DATE('" + arriendo.inicio_arriendo.ToString("yyyy-MM-dd H:mm:ss") + "', 'YYYY-MM-DD HH24:MI:SS'),";
            }
            else
            {
                query += "'',";
            }
            if (arriendo.fin_arriendo != default(DateTime))
            {
                query += " TO_DATE('" + arriendo.fin_arriendo.ToString("yyyy-MM-dd H:mm:ss") + "', 'YYYY-MM-DD HH24:MI:SS'),";
            }
            else
            {
                query += "'',";
            }
            query += arriendo.horas_usadas + ",";
            query += arriendo.cod_estacionamiento + ",";
            query += arriendo.cod_vehiculo + ")";

            int filasIngresadas = conexion.ingresar(query);
            conexion.cerrarConexion();
            if (filasIngresadas > 0)
            {
                return id;
            }
            else
            {
                return -1;
            }
        }
    }
}
