using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Data;

namespace CapaDatos
{
    public class Conexion
    {
        string conexionString = "Data Source=(DESCRIPTION =" +
        "(ADDRESS = (PROTOCOL = TCP)(HOST=192.168.202.138)(PORT = 1521))" +
        "(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = XE)));USER ID=PARKIN;PASSWORD=123456;";
        private OracleConnection conexion;

        public string tabla;

        public Conexion()
        {
            this.conexion = new OracleConnection(this.conexionString);
        }

        public OracleDataReader consultar(string query, Boolean conexionAbierta = false)
        {
            if (!conexionAbierta)
            {
                this.conexion.Open();
            }
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = this.conexion;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public int ingresar(string query)
        {
            this.conexion.Open();
            int lineasAfectadas = 0;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = this.conexion;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            lineasAfectadas = cmd.ExecuteNonQuery();
            return lineasAfectadas;
        }

        public int getSequenceValor(string nombre, int tipo = 0)
        {
            int valor = 0;
            string query = "";
            this.conexion.Open();
            switch (tipo)
            {
                case 0:
                    //Valor actual
                    query = "select " + nombre + ".CURRVAL from dual";
                    break;
                case 1:
                    //Valor siguiente
                    query = "select " + nombre + ".NEXTVAL from dual";
                    break;
            }
            OracleDataReader dr = this.consultar(query, true);
            if (dr.Read())
            {
                valor = Int32.Parse(dr[0].ToString());
            }
            return valor;
        }

        public void cerrarConexion()
        {
            this.conexion.Close();
        }

        public Object buscarPorId(string tabla, string campoId, string valorId)
        {
            Object resultado = new Object();
            string query = "select * from " + tabla + " where " + campoId + "=" + valorId;
            OracleDataReader dr = this.consultar(query);
            if (dr.Read())
            {
                resultado = dr;
            }
            this.cerrarConexion();
            return resultado;
        }
    }
}
