using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class Vehiculo
    {
        public int cod_vehiculo { get; set; }
        public string patente { get; set; }
        public string modelo { get; set; }
        public int estado { get; set; }
        public int cod_usuario { get; set; }
        public int cod_vehiculo_marca { get; set; }
        public VehiculoMarca vehiculoMarca { get; set; }
        public Usuario Usuario { get; set; }

        private Vehiculo llenarObjeto(OracleDataReader dr)
        {
            Vehiculo vehiculo = new Vehiculo();
            vehiculo.cod_vehiculo = Int32.Parse(dr["cod_vehiculo"].ToString());
            vehiculo.patente = dr["patente"].ToString();
            vehiculo.modelo = dr["modelo"].ToString();
            vehiculo.estado = Int32.Parse(dr["estado"].ToString());
            vehiculo.cod_usuario = Int32.Parse(dr["cod_usuario"].ToString());
            vehiculo.cod_vehiculo_marca = Int32.Parse(dr["cod_vehiculo_marca"].ToString());
            return vehiculo;
        }

        public int guardar(Vehiculo vehiculo)
        {
            Conexion conexion = new Conexion();
            int id = conexion.getSequenceValor("VEHICULOS_SEQ", 1);

            string query = "insert into VEHICULOS (cod_vehiculo, patente, modelo, estado,cod_usuario,cod_vehiculo_marca) values (";
            query += id + ",";
            query += "'" + vehiculo.patente + "',";
            query += "'" + vehiculo.modelo + "',";
            query += vehiculo.estado + ",";
            query += vehiculo.cod_usuario + ",";
            query += vehiculo.cod_vehiculo_marca + ")";

            int filasIngresadas = conexion.ingresar(query);
            if (filasIngresadas > 0)
            {
                return id;
            }
            else
            {
                return -1;
            }
        }

        public List<Vehiculo> buscarTodos(int codUsuario = 0)
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            Conexion conexion = new Conexion();
            string query = "select * from VEHICULOS";
            if (!codUsuario.Equals(0)) { query += " where COD_USUARIO=" + codUsuario; }

            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read())
            {
                Vehiculo vehiculo = this.llenarObjeto(dr);
                vehiculos.Add(vehiculo);
            }
            dr.Close();
            return vehiculos;
        }

        public List<Vehiculo> buscarVehiculosUsuario(int codUsuario, Boolean llenaCombo = false)
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            Conexion conexion = new Conexion();
            string query = "select * from VEHICULOS where COD_USUARIO = " + codUsuario;

            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read())
            {
                Vehiculo vehiculo = this.llenarObjeto(dr);
                vehiculos.Add(vehiculo);
            }
            dr.Close();

            if (llenaCombo)
            {
                vehiculos.Insert(0, new Vehiculo { cod_vehiculo = 0, patente = "Seleccione" });
            }
            return vehiculos;
        }

        public Vehiculo buscarPorPK(int codVehiculo, Boolean incluirAsoc = false)
        {
            Vehiculo vehiculo = new Vehiculo();
            Conexion conexion = new Conexion();
            string query = "select * from VEHICULOS where cod_vehiculo=" + codVehiculo;

            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                vehiculo = this.llenarObjeto(dr);

                if (incluirAsoc){
                    vehiculo.Usuario = new Usuario().buscarPorPK(vehiculo.cod_usuario, true);
                }
            }
            dr.Close();
            return vehiculo;
        }
        public Boolean actualizar(Vehiculo vehiculo)
        {
            Boolean guarda = false;
            Conexion conexion = new Conexion();
            string query = "update VEHICULOS set";
            query += " COD_VEHICULO = " + vehiculo.cod_vehiculo;
            if (!string.IsNullOrEmpty(vehiculo.patente)) { query += ",PATENTE = '" + vehiculo.patente + "'"; }
            if (!string.IsNullOrEmpty(vehiculo.modelo)) { query += ",MODELO = '" + vehiculo.modelo + "'"; }
            if (!vehiculo.cod_vehiculo_marca.Equals(0)) { query += ",COD_VEHICULO_MARCA = " + vehiculo.cod_vehiculo_marca; }
            query += " where COD_VEHICULO = '" + vehiculo.cod_vehiculo + "'";

            int filasActualizadas = conexion.ingresar(query);
            if (filasActualizadas > 0)
            {
                guarda = true;
            }
            return guarda;
        }
    }
}
