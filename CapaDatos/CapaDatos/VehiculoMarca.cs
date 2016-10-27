using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class VehiculoMarca
    {
        public int cod_vehiculo_marca { get; set; }
        public string nombre_vehiculo_marca { get; set; }

        public List<VehiculoMarca> buscarTodos(bool llenarCombo = false)
        {
            List<VehiculoMarca> marcas = new List<VehiculoMarca>();
            Conexion conexion = new Conexion();
            string query = "select * from VEHICULO_MARCAS order by NOMBRE_VEHICULO_MARCA asc";

            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read())
            {
                VehiculoMarca marca = new VehiculoMarca();
                marca.cod_vehiculo_marca = Int32.Parse(dr["cod_vehiculo_marca"].ToString());
                marca.nombre_vehiculo_marca = dr["nombre_vehiculo_marca"].ToString();
                marcas.Add(marca);
            }
            conexion.cerrarConexion();

            if (llenarCombo)
            {
                marcas.Insert(0, new VehiculoMarca { cod_vehiculo_marca = 0, nombre_vehiculo_marca = "Seleccione" });
            }
            return marcas;
        }
    }
}
