using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class Usuario
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

        public string nombre_completo { get; set; }
        public string rut_completo { get; set; }

        public UsuarioTipo UsuarioTipo { get; set; }
        public Comuna Comuna { get; set; }
        //public List<Calificaciones> calificaciones { get; set; }
        //public List<Calificaciones> calificacionesHechas { get; set; }
        public List<Estacionamiento> estacionamientos { get; set; }
        public List<Vehiculo> vehiculos { get; set; }
        public Tarjeta tarjeta { get; set; }

        private Usuario llenarObjeto(OracleDataReader dr)
        {
            Usuario usuario = new Usuario();
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
            usuario.nombre_completo = usuario.nombres + " " + usuario.apellido_pat + " " + usuario.apellido_mat;
            usuario.rut_completo = usuario.rut.ToString() + "-" + usuario.dv;
            usuario.cod_usuario_tipo = Int32.Parse(dr["cod_usuario_tipo"].ToString());
            usuario.cod_comuna = Int32.Parse(dr["cod_comuna"].ToString());
            return usuario;
        }

        public List<Usuario> buscarTodos(Boolean soloActivos = false)
        {
            List<Usuario> usuarios = new List<Usuario>();
            Conexion conexion = new Conexion();
            string query = "select * from USUARIOS";
            if (soloActivos){
                query += " where nombres <> 'a' and apellido_pat <> 'a' and apellido_mat <> 'a' ";
            }

            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read())
            {
                Usuario usuario = this.llenarObjeto(dr);
                usuarios.Add(usuario);
            }
            dr.Close();
            return usuarios;
        }
		
		public Usuario login(string rut = "", string password = "")
        {
            Usuario usuario = new Usuario();
            Conexion conexion = new Conexion();
            usuario.cod_usuario = -1;

            string[] rutSeparado = rut.Split('-');
            int rutSinDV = 0;

            try{
                rutSinDV = Int32.Parse(rutSeparado[0]);
            }catch(Exception e){ }
            
            string query = "select * from usuarios where rut='" + rutSinDV + "' and password='" + this.encriptarMD5(password) + "' and estado = 1";

            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                usuario = this.llenarObjeto(dr);
            }
            dr.Close();
            return usuario;
        }

        public int guardar(Usuario usuario, int usuarioId = 0)
        {
            Conexion conexion = new Conexion();
            int id;
            if (usuarioId.Equals(0)) {
                id = conexion.getSequenceValor("usuarios_seq", 1);
            }else{
                id = usuarioId;
            }

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
            if (filasIngresadas > 0){
                return id;
            }else{
                return -1;
            }
        }

        public Boolean registrarUsuario(Usuario usuario)
        {
            Boolean almacena = false;
            Conexion conexion = new Conexion();
            int usuarioId = conexion.getSequenceValor("usuarios_seq", 1);

            Tarjeta tarjeta = usuario.tarjeta;
            tarjeta.cod_usuario = usuarioId;

            if (this.guardar(usuario, usuarioId) > 0 && tarjeta.guardar(tarjeta) > 0)
            {
                almacena = true;
            }
            return almacena;
        }

        private string encriptarMD5(string cadena)
        {
            MD5 md5Hash = MD5.Create();
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = md5Hash.ComputeHash(encoding.GetBytes(cadena));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++){
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public Usuario buscarPorPK(int codUsuario, Boolean incluirAsoc = false)
        {
            Usuario usuario = new Usuario();
            Conexion conexion = new Conexion();
            string query = "select * from usuarios where cod_usuario = " + codUsuario;

            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                usuario = this.llenarObjeto(dr);
                if (incluirAsoc){
                    usuario.tarjeta = new Tarjeta().buscarPorUsuarioId(usuario.cod_usuario);
                    usuario.Comuna = new Comuna().buscarPorPK(usuario.cod_comuna);
                    usuario.Comuna.Provincia = new Provincia().buscarPorPK(usuario.Comuna.cod_provincia);
                    usuario.Comuna.Provincia.Region = new Region().buscarPorPK(usuario.Comuna.Provincia.cod_region);
                }
            }
            dr.Close();
            return usuario;
        }

        public Boolean actualizar(Usuario usuario)
        {
            Boolean guarda = false;
            Conexion conexion = new Conexion();
            string query = "update USUARIOS set";
            query += " COD_USUARIO = " + usuario.cod_usuario;

            if (!usuario.rut.Equals(0)){ query += ",rut = '" + usuario.rut + "'"; }
            if (!string.IsNullOrEmpty(usuario.dv)){ query += ",dv = '" + usuario.dv + "'"; }
            if (!string.IsNullOrEmpty(usuario.nombres)){ query += ",nombres = '" + usuario.nombres + "'"; }
            if (!string.IsNullOrEmpty(usuario.apellido_pat)){ query += ",apellido_pat = '" + usuario.apellido_pat + "'"; }
            if (!string.IsNullOrEmpty(usuario.apellido_mat)){ query += ",apellido_mat = '" + usuario.apellido_mat + "'"; }
            if (default(DateTime) != usuario.fecha_nacimiento) { query += ",fecha_nacimiento = DATE '" + usuario.fecha_nacimiento.Date.ToString("yyyy-MM-dd") + "'"; }
            if (!string.IsNullOrEmpty(usuario.sexo)) { query += ",sexo = '" + usuario.sexo + "'"; }
            if (!string.IsNullOrEmpty(usuario.password)) { query += ",password = '" + this.encriptarMD5(usuario.password) + "'"; }
            if (!string.IsNullOrEmpty(usuario.direccion)) { query += ",direccion = '" + usuario.direccion + "'"; }
            if (!string.IsNullOrEmpty(usuario.telefono)) { query += ",telefono = '" + usuario.telefono + "'"; }
            if (!string.IsNullOrEmpty(usuario.email)) { query += ",email = '" + usuario.email + "'"; }
            if (!usuario.estado.Equals(0)) { query += ",estado = " + usuario.estado; }
            if (!usuario.cod_usuario_tipo.Equals(0)) { query += ",cod_usuario_tipo = " + usuario.cod_usuario_tipo; }
            if (!usuario.cod_comuna.Equals(0)) { query += ",cod_comuna = " + usuario.cod_comuna; }
            query += " where COD_USUARIO = " + usuario.cod_usuario;

            int filasActualizadas = conexion.ingresar(query);
            if (filasActualizadas > 0){
                guarda = true;
            }
            return guarda;
        }
    }
}
