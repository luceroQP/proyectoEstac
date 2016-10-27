using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class Usuario : Conexion
    {
        public int cod_usuario { get; set; }
        public int rut { get; set; }
        public string dv { get; set; }
        public string nombres { get; set; }
        public string apellido_pat { get; set; }
        public string apellido_mat { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string sexo { get; set; }
        public string password { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public int estado { get; set; }
        public int cod_usuario_tipo { get; set; }
        public int cod_comuna { get; set; }
        public UsuarioTipo UsuarioTipo { get; set; }
        public Comuna Comuna { get; set; }
        //public List<Calificaciones> calificaciones { get; set; }
        //public List<Calificaciones> calificacionesHechas { get; set; }
        public List<Estacionamiento> estacionamientos { get; set; }
        public List<Vehiculo> vehiculos { get; set; }
        public List<Tarjeta> tarjetas { get; set; }
		
		public Usuario login(string rut = "", string password = "")
        {
            Usuario usuario = new Usuario();
            usuario.cod_usuario = -1;

            string[] rutSeparado = rut.Split('-');
            int rutSinDV = 0;

            try
            {
                rutSinDV = Int32.Parse(rutSeparado[0]);
            }catch(Exception e){ }
            
            Conexion conexion = new Conexion();
            string query = "select * from usuarios where rut='" + rutSinDV + "' and password='" + this.encriptarMD5(password) + "'";

            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                usuario.cod_usuario = Int32.Parse(dr["cod_usuario"].ToString());
                usuario.dv = dr["dv"].ToString();
                usuario.rut = Int32.Parse(dr["rut"].ToString());
                usuario.nombres = dr["nombres"].ToString();
                usuario.apellido_pat = dr["apellido_pat"].ToString();
                usuario.apellido_mat = dr["apellido_mat"].ToString();
                usuario.fecha_nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"].ToString());
                usuario.sexo = dr["sexo"].ToString();
                usuario.password = dr["password"].ToString();
                usuario.direccion = dr["direccion"].ToString();
                usuario.telefono = dr["telefono"].ToString();
                usuario.email = dr["email"].ToString();
                usuario.estado = Int32.Parse(dr["estado"].ToString());
                usuario.cod_usuario_tipo = Int32.Parse(dr["cod_usuario_tipo"].ToString());
                usuario.cod_comuna = Int32.Parse(dr["cod_comuna"].ToString());
            }
            conexion.cerrarConexion();
            return usuario;
        }

        public int guardar(Usuario usuario)
        {
            Conexion conexion = new Conexion();
            int id = conexion.getSequenceValor("usuarios_seq", 1);
            conexion.cerrarConexion();

            string query = "insert into usuarios (cod_usuario, rut, dv, nombres, apellido_pat, apellido_mat, fecha_nacimiento, sexo, password, direccion, telefono, email, estado, cod_usuario_tipo, cod_comuna) values (";
            query += id + ",";
            query += usuario.rut + ",";
            query += "'" + usuario.dv + "',";
            query += "'" + usuario.nombres + "',";
            query += "'" + usuario.apellido_pat + "',";
            query += "'" + usuario.apellido_mat + "',";
            query += " DATE '" + usuario.fecha_nacimiento.Date.ToString("yyyy-MM-dd") + "',";
            query += "'" + usuario.sexo + "',";
            query += "'" + this.encriptarMD5(usuario.password) + "',";
            query += "'" + usuario.direccion + "',";
            query += "'" + usuario.telefono + "',";
            query += "'" + usuario.email + "',";
            query += usuario.estado + ",";
            query += usuario.cod_usuario_tipo + ",";
            query += usuario.cod_comuna+ ")";

            int filasIngresadas = conexion.ingresar(query);
            conexion.cerrarConexion();
            if (filasIngresadas > 0)
            {
                return id;
            }
            else
            {
                return -1;
            }
        }

        private string encriptarMD5(string cadena)
        {
            MD5 md5Hash = MD5.Create();
            //ASCIIEncoding encoding = new ASCIIEncoding();
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = md5Hash.ComputeHash(encoding.GetBytes(cadena));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++){
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /*
        public Usuario buscarPorId()
        {
            Usuario usuario = new Usuario();
            Object resultado = base.buscarPorId("usuarios", "cod_usuarios", "1");
            usuario.cod_usuario = Int32.Parse(resultado["cod_usuario"];
        }
         * */
    }
}
