using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class CalificacionesTipo
    {
        public int cod_calificacion_tipo { get; set; }
        public string nombre_calificacion_tipo { get; set; }
        public List<Calificacion> calificaciones { get; set; }

        private CalificacionesTipo llenarObjeto(OracleDataReader dr)
        {
            CalificacionesTipo tipo = new CalificacionesTipo();
            tipo.nombre_calificacion_tipo = dr["nombre_calificacion_tipo"].ToString();
            tipo.cod_calificacion_tipo = Int32.Parse(dr["cod_calificacion_tipo"].ToString());
            return tipo;
        }

        public List<CalificacionesTipo> buscarTodos(Boolean llenaCombo = false)
        {
            List<CalificacionesTipo> tipos = new List<CalificacionesTipo>();
            Conexion conexion = new Conexion();
            string query = "select * from calificaciones_tipos";

            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read()){
                CalificacionesTipo tipo = this.llenarObjeto(dr);
                tipos.Add(tipo);
            }

            if (llenaCombo){
                tipos.Insert(0, new CalificacionesTipo { cod_calificacion_tipo = 0, nombre_calificacion_tipo = "Seleccione"});
            }
            dr.Close();
            return tipos;
        }

        public CalificacionesTipo buscarPorPk(int codCalificacionTipo)
        {
            CalificacionesTipo tipo = new CalificacionesTipo();
            Conexion conexion = new Conexion();
            string query = "select * from calificaciones_tipos where cod_calificacion_tipo = "+codCalificacionTipo;

            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                tipo = this.llenarObjeto(dr);
            }
            dr.Close();
            return tipo;
        }
    }
}
