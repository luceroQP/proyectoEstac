using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class Region
    {
        public int cod_region { get; set; }
        public string nombre_region { get; set; }
        public List<Provincia> provincias { get; set; }

        public List<CapaDatos.Region> buscarTodos(bool llenarCombo = false)
        {
            List<CapaDatos.Region> regiones = new List<CapaDatos.Region>();
            string query = "select * from REGIONES order by NOMBRE_REGION asc";
            Conexion conexion = new Conexion();

            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read())
            {
                CapaDatos.Region region = new CapaDatos.Region();
                region.cod_region = Int32.Parse(dr["cod_region"].ToString());
                region.nombre_region = dr["nombre_region"].ToString();
                regiones.Add(region);
            }
            conexion.cerrarConexion();

            if (llenarCombo)
            {
                regiones.Insert(0, new CapaDatos.Region { cod_region = 0, nombre_region = "Seleccione" });
            }
            return regiones;
        }
    }
}
