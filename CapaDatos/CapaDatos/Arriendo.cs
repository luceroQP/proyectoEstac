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
        public Transaccion Transaccion { get; set; }
        public Calificacion Calificacion { get; set; }

        private Arriendo llenarObjecto(OracleDataReader dr)
        {
            Arriendo arriendo = new Arriendo();
            arriendo.cod_arriendo = Int32.Parse(dr["cod_arriendo"].ToString());
            if (dr["inicio_arriendo"].ToString().Equals("")){
                arriendo.inicio_arriendo = new DateTime();
            }else{
                arriendo.inicio_arriendo = Convert.ToDateTime(dr["inicio_arriendo"].ToString());
            }
            if (dr["fin_arriendo"].ToString().Equals("")){
                arriendo.fin_arriendo = new DateTime();
            }else{
                arriendo.fin_arriendo = Convert.ToDateTime(dr["fin_arriendo"].ToString());
            }
            arriendo.horas_usadas = Int32.Parse(dr["horas_usadas"].ToString());
            arriendo.cod_estacionamiento = Int32.Parse(dr["cod_estacionamiento"].ToString());
            arriendo.cod_vehiculo = Int32.Parse(dr["cod_vehiculo"].ToString());
            return arriendo;
        }

        public List<Arriendo> buscarTodos(int codUsuario = 0)
        {
            List<Arriendo> arriendos = new List<Arriendo>();
            Conexion conexion = new Conexion();
            string query = "select ARR.* from VEHICULOS V";
            query += " join ARRIENDOS ARR on ARR.COD_VEHICULO = V.COD_VEHICULO";
            query += " where V.COD_USUARIO =" + codUsuario;

            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read())
            {
                Arriendo arriendo = this.llenarObjecto(dr);
                arriendo.Vehiculo = new Vehiculo().buscarPorPK(arriendo.cod_vehiculo);
                arriendo.Estacionamiento = new Estacionamiento().buscarPorPk(arriendo.cod_estacionamiento);
                arriendo.Transaccion = new Transaccion().buscarPorCodArriendo(arriendo.cod_arriendo);
                arriendo.Calificacion = new Calificacion().buscarPorCodArriendo(arriendo.cod_arriendo);

                arriendos.Add(arriendo);
            }
            dr.Close();
            return arriendos;
        }

        public List<Arriendo> buscarActivos(int codUsuario = 0)
        {
            List<Arriendo> arriendos = new List<Arriendo>();
            Conexion conexion = new Conexion();
            string query = "select ARR.* from VEHICULOS V";
            query += " join ARRIENDOS ARR on ARR.COD_VEHICULO = V.COD_VEHICULO";
            query += " where ARR.FIN_ARRIENDO > SYSDATE and V.COD_USUARIO =" + codUsuario;

            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read())
            {
                Arriendo arriendo = this.llenarObjecto(dr);
                arriendo.Vehiculo = new Vehiculo().buscarPorPK(arriendo.cod_vehiculo);
                arriendo.Estacionamiento = new Estacionamiento().buscarPorPk(arriendo.cod_estacionamiento);
                arriendo.Transaccion = new Transaccion().buscarPorCodArriendo(arriendo.cod_arriendo);
                arriendo.Calificacion = new Calificacion().buscarPorCodArriendo(arriendo.cod_arriendo);
                arriendos.Add(arriendo);
            }
            dr.Close();
            return arriendos;
        }

        public List<Arriendo> buscarHistorial(int codUsuario = 0)
        {
            List<Arriendo> arriendos = new List<Arriendo>();
            Conexion conexion = new Conexion();
            string query = "select ARR.* from VEHICULOS V";
            query += " join ARRIENDOS ARR on ARR.COD_VEHICULO = V.COD_VEHICULO";
            query += " where ARR.FIN_ARRIENDO < SYSDATE and V.COD_USUARIO =" + codUsuario;

            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read())
            {
                Arriendo arriendo = this.llenarObjecto(dr);
                arriendo.Vehiculo = new Vehiculo().buscarPorPK(arriendo.cod_vehiculo);
                arriendo.Estacionamiento = new Estacionamiento().buscarPorPk(arriendo.cod_estacionamiento);
                arriendo.Transaccion = new Transaccion().buscarPorCodArriendo(arriendo.cod_arriendo);
                arriendo.Calificacion = new Calificacion().buscarPorCodArriendo(arriendo.cod_arriendo);
                arriendos.Add(arriendo);
            }
            dr.Close();
            return arriendos;
        }

        public int guardar(Arriendo arriendo)
        {
            Conexion conexion = new Conexion();
            int id = conexion.getSequenceValor("ARRIENDOS_SEQ", 1);

            string query = "insert into ARRIENDOS(COD_ARRIENDO, INICIO_ARRIENDO, FIN_ARRIENDO, HORAS_USADAS, COD_ESTACIONAMIENTO,COD_VEHICULO) values (";
            query += id + ",";
            if (arriendo.inicio_arriendo != default(DateTime)){
                query += " TO_DATE('" + arriendo.inicio_arriendo.ToString("yyyy-MM-dd H:mm:ss") + "', 'YYYY-MM-DD HH24:MI:SS'),";
            }else{
                query += "'',";
            }
            if (arriendo.fin_arriendo != default(DateTime)){
                query += " TO_DATE('" + arriendo.fin_arriendo.ToString("yyyy-MM-dd H:mm:ss") + "', 'YYYY-MM-DD HH24:MI:SS'),";
            }else{
                query += "'',";
            }
            query += arriendo.horas_usadas + ",";
            query += arriendo.cod_estacionamiento + ",";
            query += arriendo.cod_vehiculo + ")";

            int filasIngresadas = conexion.ingresar(query);
            if (filasIngresadas > 0){
                return id;
            }else{
                return -1;
            }
        }

        public Arriendo buscarPorPk(int codArriendo)
        {
            Arriendo arriendo = new Arriendo();
            Conexion conexion = new Conexion();
            string query = "select * from arriendos where cod_arriendo = "+ codArriendo;

            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                arriendo = this.llenarObjecto(dr);
                arriendo.Vehiculo = new Vehiculo().buscarPorPK(arriendo.cod_vehiculo);
                arriendo.Estacionamiento = new Estacionamiento().buscarPorPk(arriendo.cod_estacionamiento);
            }
            dr.Close();
            return arriendo;
        }

        public Arriendo datosPagar(int codArriendo)
        {
            Arriendo arriendo = new Arriendo();
            Conexion conexion = new Conexion();
            string query = "select * from arriendos where cod_arriendo = " + codArriendo;

            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                arriendo = this.llenarObjecto(dr);
                arriendo.Vehiculo = new Vehiculo().buscarPorPK(arriendo.cod_vehiculo, true);
                arriendo.Estacionamiento = new Estacionamiento().buscarPorPk(arriendo.cod_estacionamiento, true);
            }
            dr.Close();
            return arriendo;
        }

        public Arriendo arriendoActivoEstacionamiento(int codEstacionamiento)
        {
            Arriendo arriendo = new Arriendo();
            Conexion conexion = new Conexion();
            string query = "select * from ARRIENDOS where FIN_ARRIENDO > SYSDATE and COD_ESTACIONAMIENTO = "+ codEstacionamiento;
            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                arriendo = this.llenarObjecto(dr);
            }
            dr.Close();
            return arriendo;
        }
    }
}