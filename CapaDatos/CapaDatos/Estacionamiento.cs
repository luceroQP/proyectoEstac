using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Globalization;

namespace CapaDatos
{
    public class Estacionamiento
    {
        public int cod_estacionamiento { get; set; }
        public string direccion { get; set; }
        public int valor_hora { get; set; }
        public Double latitud { get; set; }
        public Double longitud { get; set; }
        public DateTime inicio_disponibilidad { get; set; }
        public DateTime fin_disponibilidad { get; set; }
        public int capacidad { get; set; }
        public int existencias { get; set; }
        public int cod_usuario { get; set; }
        public int cod_estacionamiento_estado { get; set; }
        public Usuario Usuario { get; set; }
        public EstacionamientoEstado EstacionamientoEstado { get; set; }

        public List<Estacionamiento> buscarTodos(int codUsuario = 0, Boolean incluirAsocc = false)
        {
            List<Estacionamiento> estacionamientos = new List<Estacionamiento>();
            Conexion conexion = new Conexion();
            string query = "select * from ESTACIONAMIENTOS";
            if (!codUsuario.Equals(0)) { query += "where COD_USUARIO=" + codUsuario; }

            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read())
            {
                Estacionamiento estacionamiento = new Estacionamiento();
                estacionamiento.cod_estacionamiento = int.Parse(dr["cod_estacionamiento"].ToString());
                estacionamiento.direccion = dr["direccion"].ToString();
                estacionamiento.valor_hora = int.Parse(dr["valor_hora"].ToString());
                if (!string.IsNullOrEmpty(dr["latitud"].ToString())){
                    estacionamiento.latitud = Double.Parse(dr["latitud"].ToString().Replace(",","."),CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrEmpty(dr["longitud"].ToString())){
                    estacionamiento.longitud = Double.Parse(dr["longitud"].ToString().Replace(",", "."), CultureInfo.InvariantCulture);
                }
                if (dr["inicio_disponibilidad"].ToString().Equals("")){
                    estacionamiento.inicio_disponibilidad = new DateTime();
                }else{
                    estacionamiento.inicio_disponibilidad = Convert.ToDateTime(dr["inicio_disponibilidad"].ToString());
                }
                if (dr["fin_disponibilidad"].ToString().Equals("")){
                    estacionamiento.fin_disponibilidad = new DateTime();
                }else{
                    estacionamiento.fin_disponibilidad = Convert.ToDateTime(dr["fin_disponibilidad"].ToString());
                }
                estacionamiento.capacidad = int.Parse(dr["capacidad"].ToString());
                estacionamiento.existencias = int.Parse(dr["existencias"].ToString());
                estacionamiento.cod_usuario=int.Parse(dr["cod_usuario"].ToString());
                estacionamiento.cod_estacionamiento_estado = int.Parse(dr["cod_estacionamiento_estado"].ToString());

                if (incluirAsocc)
                {
                    estacionamiento.EstacionamientoEstado = new EstacionamientoEstado().buscarPorPk(estacionamiento.cod_estacionamiento_estado);
                }
                estacionamientos.Add(estacionamiento);
            }
            conexion.cerrarConexion();
            return estacionamientos;
        }

        public int guardar(Estacionamiento estacionamiento)
        {
            Conexion conexion = new Conexion();
            int id = conexion.getSequenceValor("ESTACIONAMIENTOS_SEQ", 1);
            conexion.cerrarConexion();

            string query = "insert into ESTACIONAMIENTOS(COD_ESTACIONAMIENTO, DIRECCION, VALOR_HORA, LATITUD, LONGITUD,INICIO_DISPONIBILIDAD,FIN_DISPONIBILIDAD,CAPACIDAD,EXISTENCIAS,COD_USUARIO,COD_ESTACIONAMIENTO_ESTADO) values (";
            query += id + ",";
            query += "'" + estacionamiento.direccion + "',";
            query += estacionamiento.valor_hora + ",";
            query += "'" + estacionamiento.latitud.ToString().Replace(",",".") + "',";
            query += "'" + estacionamiento.longitud.ToString().Replace(",", ".") + "',";
            if (estacionamiento.inicio_disponibilidad != default(DateTime))
            {
                query += " DATE '" + estacionamiento.inicio_disponibilidad.Date.ToString("yyyy-MM-dd H:mm:ss") + "',";
            }
            else {
                query += "'',";
            }

            if (estacionamiento.fin_disponibilidad != default(DateTime))
            {
                query += " DATE '" + estacionamiento.fin_disponibilidad.Date.ToString("yyyy-MM-dd H:mm:ss") + "',";
            }else{
                query += "'',";
            }
            query += estacionamiento.capacidad + ",";
            query += estacionamiento.existencias + ",";
            query += estacionamiento.cod_usuario + ",";
            query += estacionamiento.cod_estacionamiento_estado + ")";

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

        public Estacionamiento buscarPorPk(int codEstacionamiento, Boolean incluirAsocc = false)
        {
            Estacionamiento estacionamiento = new Estacionamiento();
            Conexion conexion = new Conexion();
            string query = "select * from ESTACIONAMIENTOS where COD_ESTACIONAMIENTO = " + codEstacionamiento;

            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                estacionamiento.cod_estacionamiento = int.Parse(dr["cod_estacionamiento"].ToString());
                estacionamiento.direccion = dr["direccion"].ToString();
                estacionamiento.valor_hora = int.Parse(dr["valor_hora"].ToString());

                if (!string.IsNullOrEmpty(dr["latitud"].ToString())){
                    estacionamiento.latitud = Double.Parse(dr["latitud"].ToString().Replace(",", "."), CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrEmpty(dr["longitud"].ToString())){
                    estacionamiento.longitud = Double.Parse(dr["longitud"].ToString().Replace(",", "."), CultureInfo.InvariantCulture);
                }

                if (dr["inicio_disponibilidad"].ToString().Equals("")){
                    estacionamiento.inicio_disponibilidad = new DateTime();
                }else{
                    estacionamiento.inicio_disponibilidad = Convert.ToDateTime(dr["inicio_disponibilidad"].ToString());
                }
                if (dr["fin_disponibilidad"].ToString().Equals("")){
                    estacionamiento.fin_disponibilidad = new DateTime();
                }else{
                    estacionamiento.fin_disponibilidad = Convert.ToDateTime(dr["fin_disponibilidad"].ToString());
                }
                estacionamiento.capacidad = int.Parse(dr["capacidad"].ToString());
                estacionamiento.existencias = int.Parse(dr["existencias"].ToString());
                estacionamiento.cod_usuario = int.Parse(dr["cod_usuario"].ToString());
                estacionamiento.cod_estacionamiento_estado = int.Parse(dr["cod_estacionamiento_estado"].ToString());

                if (incluirAsocc)
                {
                    estacionamiento.EstacionamientoEstado = new EstacionamientoEstado().buscarPorPk(estacionamiento.cod_estacionamiento_estado);
                }
            }
            conexion.cerrarConexion();

            return estacionamiento;
        }

        public Boolean actualizar(Estacionamiento estacionamiento)
        {
            Boolean guarda = false;
            Conexion conexion = new Conexion();

            string query = "update ESTACIONAMIENTOS set";
            query += " COD_ESTACIONAMIENTO = " + estacionamiento.cod_estacionamiento;

            if (!estacionamiento.cod_estacionamiento_estado.Equals(0)) { query += ",COD_ESTACIONAMIENTO_ESTADO = " + estacionamiento.cod_estacionamiento_estado; }
            if (!string.IsNullOrEmpty(estacionamiento.direccion)) { query += ",DIRECCION = '" + estacionamiento.direccion +"'"; }
            if (!estacionamiento.latitud.Equals(0)) { query += ",LATITUD = '" + estacionamiento.latitud.ToString().Replace(",", ".") + "'"; }
            if (!estacionamiento.longitud.Equals(0)) { query += ",LONGITUD = '" + estacionamiento.longitud.ToString().Replace(",", ".") + "'"; }
            if (!estacionamiento.valor_hora.Equals(0)) { query += ",VALOR_HORA = '" + estacionamiento.valor_hora +"'"; }
            if (!estacionamiento.capacidad.Equals(0)) { query += ",CAPACIDAD = " + estacionamiento.capacidad; }
            if (!estacionamiento.existencias.Equals(0)) { query += ",EXISTENCIAS = " + estacionamiento.existencias; }
            if (estacionamiento.inicio_disponibilidad != default(DateTime)) { query += ",INICIO_DISPONIBILIDAD = to_date('" + estacionamiento.inicio_disponibilidad.ToString("yyyy-MM-dd H:mm:ss") + "', 'YYYY-MM-DD HH24:MI:SS')"; }
            if (estacionamiento.fin_disponibilidad != default(DateTime)) { query += ",FIN_DISPONIBILIDAD = to_date('" + estacionamiento.fin_disponibilidad.ToString("yyyy-MM-dd H:mm:ss") + "', 'YYYY-MM-DD HH24:MI:SS')"; }

            query += " where COD_ESTACIONAMIENTO = '" + estacionamiento.cod_estacionamiento + "'";

            int filasActualizadas = conexion.ingresar(query);
            conexion.cerrarConexion();
            if (filasActualizadas > 0){
                guarda = true;
            }
            return guarda;
        }

        public List<Estacionamiento> estacionamientosDisponibles(int codUsuario, Boolean llenaCombo = false)
        {
            List<Estacionamiento> estacionamientos = new List<Estacionamiento>();
            Conexion conexion = new Conexion();
            string query = "select * from ESTACIONAMIENTOS";

            OracleDataReader dr = conexion.consultar(query);
            while(dr.Read()){
                Estacionamiento estacionamiento = new Estacionamiento();

                estacionamiento.cod_estacionamiento = int.Parse(dr["cod_estacionamiento"].ToString());
                estacionamiento.direccion = dr["direccion"].ToString();
                estacionamiento.valor_hora = int.Parse(dr["valor_hora"].ToString());
                if (!string.IsNullOrEmpty(dr["latitud"].ToString())){
                    estacionamiento.latitud = Double.Parse(dr["latitud"].ToString().Replace(",", "."), CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrEmpty(dr["longitud"].ToString())){
                    estacionamiento.longitud = Double.Parse(dr["longitud"].ToString().Replace(",", "."), CultureInfo.InvariantCulture);
                }

                if (dr["inicio_disponibilidad"].ToString().Equals("")){
                    estacionamiento.inicio_disponibilidad = new DateTime();
                }else{
                    estacionamiento.inicio_disponibilidad = Convert.ToDateTime(dr["inicio_disponibilidad"].ToString());
                }
                if (dr["fin_disponibilidad"].ToString().Equals("")){
                    estacionamiento.fin_disponibilidad = new DateTime();
                }else{
                    estacionamiento.fin_disponibilidad = Convert.ToDateTime(dr["fin_disponibilidad"].ToString());
                }
                estacionamiento.capacidad = int.Parse(dr["capacidad"].ToString());
                estacionamiento.existencias = int.Parse(dr["existencias"].ToString());
                estacionamiento.cod_usuario = int.Parse(dr["cod_usuario"].ToString());
                estacionamiento.cod_estacionamiento_estado = int.Parse(dr["cod_estacionamiento_estado"].ToString());

                estacionamientos.Add(estacionamiento);
            }
            conexion.cerrarConexion();

            if (llenaCombo)
            {
                estacionamientos.Insert(0, new Estacionamiento { cod_estacionamiento = 0, direccion = "Seleccione"});
            }
            return estacionamientos;
        }
    }
}
