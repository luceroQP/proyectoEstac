using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class Calificacion
    {
        public int cod_calificacion { get; set; }
        public int nota { get; set; }
        public string observacion { get; set; }
        public int cod_calificacion_tipo { get; set; }
        public int cod_usuario_calificador { get; set; }
        public int cod_usuario_calificado { get; set; }
        public int cod_arriendo { get; set; }
        public CalificacionesTipo CalificacionTipo { get; set; }
        public Usuario UsuarioCalificador { get; set; }
        public Usuario UsuarioCalificado { get; set; }
        public Arriendo Arriendo { get; set; }

        private Calificacion llenarObjeto(OracleDataReader dr)
        {
            Calificacion calificacion = new Calificacion();
            calificacion.cod_calificacion = Int32.Parse(dr["cod_calificacion"].ToString());
            calificacion.nota = Int32.Parse(dr["nota"].ToString());
            calificacion.observacion = dr["observacion"].ToString();
            calificacion.cod_calificacion_tipo = Int32.Parse(dr["cod_calificacion_tipo"].ToString());
            calificacion.cod_usuario_calificado = Int32.Parse(dr["cod_usuario_calificado"].ToString());
            calificacion.cod_usuario_calificador = Int32.Parse(dr["cod_usuario_calificador"].ToString());
            calificacion.cod_arriendo = Int32.Parse(dr["cod_arriendo"].ToString());
            return calificacion;
        }

        public int guardar(Calificacion calificacion)
        {
            Conexion conexion = new Conexion();
            int cod = conexion.getSequenceValor("CALIFICACIONES_SEQ", 1);

            string query = "insert into CALIFICACIONES (COD_CALIFICACION, NOTA,OBSERVACION,COD_CALIFICACION_TIPO,COD_USUARIO_CALIFICADOR,COD_USUARIO_CALIFICADO,COD_ARRIENDO) values (";
            query += cod + ",";
            query += calificacion.nota + ",";
            query += "'" + calificacion.observacion + "',";
            query += calificacion.cod_calificacion_tipo + ",";
            query += calificacion.cod_usuario_calificador + ",";
            query += calificacion.cod_usuario_calificado + ",";
            query += calificacion.cod_arriendo + ")";

            int filasIngresadas = conexion.ingresar(query);
            if (filasIngresadas > 0){
                return cod;
            }else{
                return -1;
            }
        }

        public Boolean actualizar(Calificacion calificacion)
        {
            Boolean guarda = false;
            Conexion conexion = new Conexion();

            string query = "update CALIFICACIONES set";
            query += " COD_CALIFICACION = " + calificacion.cod_calificacion;

            if (!calificacion.cod_arriendo.Equals(0)) { query += ",COD_ARRIENDO = " + calificacion.cod_arriendo; }
            if (!calificacion.cod_calificacion_tipo.Equals(0)) { query += ",COD_CALIFICACION_TIPO = " + calificacion.cod_calificacion_tipo; }
            if (!calificacion.cod_usuario_calificador.Equals(0)) { query += ",COD_USUARIO_CALIFICADOR = " + calificacion.cod_usuario_calificador; }
            if (!calificacion.cod_usuario_calificado.Equals(0)) { query += ",COD_USUARIO_CALIFICADO = " + calificacion.cod_usuario_calificado; }
            if (!calificacion.nota.Equals(0)) { query += ",NOTA = " + calificacion.nota; }
            if (!string.IsNullOrEmpty(calificacion.observacion)) { query += ",OBSERVACION= '" + calificacion.observacion + "'"; }

            query += " where COD_CALIFICACION = " + calificacion.cod_calificacion;

            int filasActualizadas = conexion.ingresar(query);
            if (filasActualizadas > 0)
            {
                guarda = true;
            }
            return guarda;
        }

        public Calificacion buscarPorPk(int codCalificacion, Boolean incluirAsocc = false)
        {
            Calificacion calificacion = new Calificacion();
            Conexion conexion = new Conexion();
            string query = "select * from CALIFICACIONES where COD_CALIFICACION = " + codCalificacion;

            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                calificacion = this.llenarObjeto(dr);
                if (incluirAsocc){
                    calificacion.CalificacionTipo = new CalificacionesTipo().buscarPorPk(calificacion.cod_calificacion_tipo);
                }
            }
            dr.Close();
            return calificacion;
        }

        public Calificacion buscarPorCodArriendo(int codArriendo)
        {
            Calificacion calificacion = new Calificacion();
            Conexion conexion = new Conexion();
            string query = "select * from CALIFICACIONES where COD_ARRIENDO = " + codArriendo;

            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                calificacion = this.llenarObjeto(dr);
            }
            dr.Close();
            return calificacion;
        }

        public List<Calificacion> calificacionesPendientes(int codUsuario, string tipoUsuarioLogeado, string tipoClasificacion, Boolean count = false)
        {
            List<Calificacion> calificacionesPendientes = new List<Calificacion>();
            Conexion conexion = new Conexion();
            string query = "";

            if (count){
                query = "select count(*) as cantidad from CALIFICACIONES CA";
            }else{
                query = "select * from CALIFICACIONES CA";
            }

            string extraConditions = "";
            //Usuario Dueño calificando a sus clientes
            if (tipoUsuarioLogeado.Equals("Dueño") && tipoClasificacion.Equals("Cliente"))
            {
                query += " INNER JOIN ARRIENDOS ar on ar.cod_arriendo = CA.cod_arriendo";
                query += " INNER JOIN ESTACIONAMIENTOS EST on EST.cod_estacionamiento = ar.cod_estacionamiento";
                extraConditions += " and CA.COD_USUARIO_CALIFICADOR = " + codUsuario;
                extraConditions += " and EST.cod_usuario = " + codUsuario;
            }

            //Usuario dueño calificando a otros dueños
            if (tipoUsuarioLogeado.Equals("Dueño") && tipoClasificacion.Equals("Dueño"))
            {
                query += " INNER JOIN ARRIENDOS ar on ar.cod_arriendo = CA.cod_arriendo";
                query += " INNER JOIN ESTACIONAMIENTOS EST on EST.cod_estacionamiento = ar.cod_estacionamiento";
                extraConditions += " and CA.COD_USUARIO_CALIFICADOR = " + codUsuario;
                extraConditions += " and EST.cod_usuario <> " + codUsuario;
            }

            if (tipoUsuarioLogeado.Equals("Cliente") && tipoClasificacion.Equals("Dueño"))
            {
                query += " INNER JOIN ARRIENDOS ar on ar.cod_arriendo = CA.cod_arriendo";
                query += " INNER JOIN VEHICULOS VE on VE.cod_vehiculo = ar.cod_vehiculo";
                extraConditions += " and CA.COD_USUARIO_CALIFICADO = " + codUsuario;
                extraConditions += " and VE.cod_usuario =" + codUsuario;
            }
            
            query += " where CA.NOTA = 0";
            //query += " and AR.FIN_ARRIENDO < SYSDATE";
            query += extraConditions;

            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read())
            {
                Calificacion calificacion = new Calificacion();
                if (count)
                {
                    int cantidad = Int32.Parse(dr["cantidad"].ToString());
                    if (cantidad > 0){
                        calificacion.cod_calificacion = cantidad;
                        calificacionesPendientes.Add(calificacion);
                    }
                }
                else
                {
                    calificacion = this.llenarObjeto(dr);
                    calificacion.Arriendo = new Arriendo().buscarPorPk(calificacion.cod_arriendo);
                    switch (tipoClasificacion)
                    {
                        case "Cliente":
                            calificacion.Arriendo.Vehiculo.Usuario = new Usuario().buscarPorPK(calificacion.Arriendo.Vehiculo.cod_usuario);
                            break;
                        case "Dueño":
                            calificacion.Arriendo.Estacionamiento.Usuario = new Usuario().buscarPorPK(calificacion.Arriendo.Estacionamiento.cod_usuario);
                            break;
                    }
                    calificacionesPendientes.Add(calificacion);
                }
            }
            dr.Close();
            return calificacionesPendientes;
        }

        public List<Calificacion> calificacionesRealizadas(int codUsuario, string tipoUsuarioLogeado, string tipoClasificacion)
        {
            List<Calificacion> calificacionesPendientes = new List<Calificacion>();
            Conexion conexion = new Conexion();
            string query = "select * from CALIFICACIONES CA";

            string extraConditions = "";
            //Usuario Dueño calificando a sus clientes
            if (tipoUsuarioLogeado.Equals("Dueño") && tipoClasificacion.Equals("Cliente"))
            {
                query += " INNER JOIN ARRIENDOS ar on ar.cod_arriendo = CA.cod_arriendo";
                query += " INNER JOIN ESTACIONAMIENTOS EST on EST.cod_estacionamiento = ar.cod_estacionamiento";
                extraConditions += " and CA.COD_USUARIO_CALIFICADOR = " + codUsuario;
                extraConditions += " and EST.cod_usuario = " + codUsuario;
            }

            //Usuario dueño calificando a otros dueños
            if (tipoUsuarioLogeado.Equals("Dueño") && tipoClasificacion.Equals("Dueño"))
            {
                query += " INNER JOIN ARRIENDOS ar on ar.cod_arriendo = CA.cod_arriendo";
                query += " INNER JOIN ESTACIONAMIENTOS EST on EST.cod_estacionamiento = ar.cod_estacionamiento";
                extraConditions += " and CA.COD_USUARIO_CALIFICADOR = " + codUsuario;
                extraConditions += " and EST.cod_usuario <> " + codUsuario;
            }

            if (tipoUsuarioLogeado.Equals("Cliente") && tipoClasificacion.Equals("Dueño"))
            {
                query += " INNER JOIN ARRIENDOS ar on ar.cod_arriendo = CA.cod_arriendo";
                query += " INNER JOIN VEHICULOS VE on VE.cod_vehiculo = ar.cod_vehiculo";
                extraConditions += " and CA.COD_USUARIO_CALIFICADO = " + codUsuario;
                extraConditions += " and VE.cod_usuario =" + codUsuario;
            }

            query += " where CA.NOTA <> 0";
            //query += " and AR.FIN_ARRIENDO < SYSDATE";
            query += extraConditions;

            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read())
            {
                Calificacion calificacion = this.llenarObjeto(dr);
                calificacion.Arriendo = new Arriendo().buscarPorPk(calificacion.cod_arriendo);
                switch (tipoClasificacion)
                {
                    case "Cliente":
                        calificacion.Arriendo.Vehiculo.Usuario = new Usuario().buscarPorPK(calificacion.Arriendo.Vehiculo.cod_usuario);
                        break;
                    case "Dueño":
                        calificacion.Arriendo.Estacionamiento.Usuario = new Usuario().buscarPorPK(calificacion.Arriendo.Estacionamiento.cod_usuario);
                        break;
                }
                calificacionesPendientes.Add(calificacion);
            }
            dr.Close();
            return calificacionesPendientes;
        }
    }
}
