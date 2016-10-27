using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class Provincia
    {
        public int cod_provincia { get; set; }
        public string nombre_provincia { get; set; }
        public int cod_region { get; set; }
        public Region Region { get; set; }
        public List<Comuna> comunas { get; set; }

        public List<Provincia> buscarTodos(int regionId = 0, bool llenarCombo = false)
        {
            List<Provincia> provincias = new List<Provincia>();
            string query = "select * from PROVINCIAS";
            if (regionId > 0){
                query += " where COD_REGION = " + regionId;
            }
            query += " order by NOMBRE_PROVINCIA asc";

            Conexion conexion = new Conexion();
            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read())
            {
                Provincia provincia = new Provincia();
                provincia.cod_provincia = Int32.Parse(dr["cod_provincia"].ToString());
                provincia.cod_region = Int32.Parse(dr["cod_region"].ToString());
                provincia.nombre_provincia = dr["nombre_provincia"].ToString();
                provincias.Add(provincia);
            }
            conexion.cerrarConexion();
            if (llenarCombo)
            {
                provincias.Insert(0, new Provincia { cod_provincia = 0, nombre_provincia = "Seleccione" });
            }
            return provincias;
        }
    }
}
