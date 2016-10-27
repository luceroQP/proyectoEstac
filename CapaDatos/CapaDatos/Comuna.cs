using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class Comuna
    {
        public int cod_comuna { get; set; }
        public string nombre_comuna { get; set; }
        public int cod_provincia { get; set; }
        public Provincia Provincia { get; set; }
        public List<Usuario> usuarios { get; set; }

        public List<Comuna> buscarTodos(int provinciaId = 0, bool llenarCombo = false) {
            List<Comuna> comunas = new List<Comuna>();
            string query = "select * from COMUNAS";
            if (provinciaId > 0){
                query += " where COD_PROVINCIA = " + provinciaId;
            }
            query += " order by nombre_comuna asc";
            Conexion conexion = new Conexion();

            OracleDataReader dr = conexion.consultar(query);
            while(dr.Read()){
                Comuna comuna = new Comuna();
                comuna.cod_comuna = Int32.Parse(dr["cod_comuna"].ToString());
                comuna.nombre_comuna = dr["nombre_comuna"].ToString();
                comuna.cod_provincia = Int32.Parse(dr["cod_provincia"].ToString());
                comunas.Add(comuna);
            }
            conexion.cerrarConexion();

            if (llenarCombo)
            {
                comunas.Insert(0, new Comuna { cod_comuna = 0, nombre_comuna = "Seleccione"});
            }
            return comunas;
        }
    }
}
