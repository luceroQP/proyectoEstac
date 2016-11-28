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

        private CapaDatos.Region llenarObjeto(OracleDataReader dr)
        {
            CapaDatos.Region region = new CapaDatos.Region();
            region.cod_region = Int32.Parse(dr["cod_region"].ToString());
            region.nombre_region = dr["nombre_region"].ToString();
            return region;
        }

        public List<CapaDatos.Region> buscarTodos(bool llenarCombo = false)
        {
            List<CapaDatos.Region> regiones = new List<CapaDatos.Region>();
            string query = "select * from REGIONES order by NOMBRE_REGION asc";
            Conexion conexion = new Conexion();

            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read())
            {
                CapaDatos.Region region = this.llenarObjeto(dr);
                regiones.Add(region);
            }
            dr.Close();

            if (llenarCombo)
            {
                regiones.Insert(0, new CapaDatos.Region { cod_region = 0, nombre_region = "Seleccione" });
            }
            return regiones;
        }

        public CapaDatos.Region buscarPorPK(int codRegion)
        {
            CapaDatos.Region region = new CapaDatos.Region();
            Conexion conexion = new Conexion();
            string query = "select * from REGIONES where COD_REGION =" + codRegion;

            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                region = this.llenarObjeto(dr);
            }
            dr.Close();
            return region;
        }
    }
}
