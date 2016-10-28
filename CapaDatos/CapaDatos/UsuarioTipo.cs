using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class UsuarioTipo
    {
        public int cod_usuario_tipo { get; set; }
        public string nombre_usuario_tipo { get; set; }

        public List<UsuarioTipo> buscarTodos(Boolean llenarCombo = false)
        {
            List<UsuarioTipo> tipos = new List<UsuarioTipo>();
            Conexion conexion = new Conexion();
            string query = "select * from usuario_tipos";
            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read())
            {
                UsuarioTipo tipo = new UsuarioTipo();
                tipo.cod_usuario_tipo = Int32.Parse(dr["cod_usuario_tipo"].ToString());
                tipo.nombre_usuario_tipo = dr["nombre_usuario_tipo"].ToString();
                tipos.Add(tipo);
            }
            conexion.cerrarConexion();
            if (llenarCombo)
            {
                tipos.Insert(0, new UsuarioTipo { cod_usuario_tipo = 0, nombre_usuario_tipo = "Seleccione"});
            }
            return tipos;
        }
    }
}
