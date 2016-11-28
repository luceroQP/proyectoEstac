using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class Tarjeta
    {
        public int cod_tarjeta { get; set; }
        public string numero_tarjeta { get; set; }
        public int saldo { get; set; }
        public int cod_banco { get; set; }
        public int cod_tarjeta_tipo { get; set; }
        public int cod_usuario { get; set; }
        public TarjetaTipo TarjetaTipo { get; set; }
        public Usuario Usuario { get; set; }
        public Banco Banco { get; set; }
        public List<Transaccion> Transacciones { get; set; }

        private Tarjeta llenarObjeto(OracleDataReader dr)
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.saldo = Int32.Parse(dr["saldo"].ToString());
            tarjeta.numero_tarjeta = dr["numero_tarjeta"].ToString();
            tarjeta.cod_tarjeta = Int32.Parse(dr["cod_tarjeta"].ToString());
            tarjeta.cod_tarjeta_tipo = Int32.Parse(dr["cod_tarjeta_tipo"].ToString());
            tarjeta.cod_banco = Int32.Parse(dr["cod_banco"].ToString());
            tarjeta.cod_usuario = Int32.Parse(dr["cod_usuario"].ToString());
            return tarjeta;
        }

        public int guardar(Tarjeta tarjeta)
        {
            Conexion conexion = new Conexion();
            int cod = conexion.getSequenceValor("TARJETAS_SEQ", 1);

            string query = "insert into TARJETAS (COD_TARJETA, NUMERO_TARJETA, SALDO, COD_BANCO, COD_TARJETA_TIPO,COD_USUARIO) values (";
            query += cod + ",";
            query += "'" + tarjeta.numero_tarjeta + "',";
            query += tarjeta.saldo + ",";
            query += tarjeta.cod_banco + ",";
            query += tarjeta.cod_tarjeta_tipo + ",";
            query += tarjeta.cod_usuario + ")";

            int filasIngresadas = conexion.ingresar(query);
            if (filasIngresadas > 0)
            {
                return cod;
            }
            else
            {
                return -1;
            }
        }

        public List<Tarjeta> buscarTodos(int codUsuario = 0)
        {
            List<Tarjeta> tarjetas = new List<Tarjeta>();
            Conexion conexion = new Conexion();

            string query = "select * from TARJETAS";
            if (!codUsuario.Equals(0)) { query += " where COD_USUARIO=" + codUsuario; }

            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read())
            {
                Tarjeta tarjeta = this.llenarObjeto(dr);
                tarjetas.Add(tarjeta);
            }
            dr.Close();
            return tarjetas;
        }

        public Tarjeta buscarPorUsuarioId(int usuarioId)
        {
            Tarjeta tarjeta = new Tarjeta();
            Conexion conexion = new Conexion();
            string query = "select * from tarjetas where cod_usuario = " + usuarioId;

            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                tarjeta = this.llenarObjeto(dr);
            }
            dr.Close();
            return tarjeta;
        }

        public Tarjeta buscarPorPK(int codTarjeta)
        {
            Tarjeta tarjeta = new Tarjeta();
            Conexion conexion = new Conexion();
            string query = "select * from tarjetas where COD_TARJETA = " + codTarjeta;
            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                tarjeta = this.llenarObjeto(dr);
            }
            dr.Close();
            return tarjeta;
        }

        public Boolean actualizar(Tarjeta tarjeta)
        {
            Boolean guarda = false;
            Conexion conexion = new Conexion();

            string query = "update tarjetas set";
            query += " COD_TARJETA = " + tarjeta.cod_tarjeta;

            if (!tarjeta.saldo.Equals(0)) { query += ",saldo = " + tarjeta.saldo; }
            if (!tarjeta.cod_tarjeta_tipo.Equals(0)) { query += ",cod_tarjeta_tipo = " + tarjeta.cod_tarjeta_tipo; }
            if (!tarjeta.cod_banco.Equals(0)) { query += ",cod_banco = " + tarjeta.cod_banco; }
            if (!string.IsNullOrEmpty(tarjeta.numero_tarjeta)) { query += ",numero_tarjeta = '" + tarjeta.numero_tarjeta + "'"; }
            query += " where COD_TARJETA = " + tarjeta.cod_tarjeta;

            int filasActualizadas = conexion.ingresar(query);
            if (filasActualizadas > 0)
            {
                guarda = true;
            }
            return guarda;
        }

        public Tarjeta getReporteTarjeta(int usuarioId)
        {
            Tarjeta tarjeta = new Tarjeta();
            Conexion conexion = new Conexion();
            string query = "select * from tarjetas where cod_usuario = " + usuarioId;

            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                tarjeta = this.llenarObjeto(dr);
                tarjeta.Banco = new Banco().buscarPorPk(tarjeta.cod_banco);
                tarjeta.TarjetaTipo = new TarjetaTipo().buscarPorPk(tarjeta.cod_tarjeta_tipo);
                tarjeta.Transacciones = new Transaccion().getTransaccionesTarjeta(tarjeta.cod_tarjeta);
            }
            dr.Close();
            return tarjeta;
        }
    }
}
