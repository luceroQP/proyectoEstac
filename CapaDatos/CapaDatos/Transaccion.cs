using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class Transaccion
    {
        public int cod_transaccion { get; set; }
        public int monto { get; set; }
        public int cod_arriendo { get; set; }
        public int numero_tarjeta_origen { get; set; }
        public int numero_tarjeta_destino { get; set; }
        public Arriendo Arriendo { get; set; }
        public Tarjeta TarjetaOrigen { get; set; }
        public Tarjeta TarjetaDestino { get; set; }
        
        private Transaccion llenarObjeto(OracleDataReader dr)
        {
            Transaccion transaccion = new Transaccion();
            transaccion.cod_transaccion = Int32.Parse(dr["cod_transaccion"].ToString());
            transaccion.cod_arriendo = Int32.Parse(dr["cod_arriendo"].ToString());
            transaccion.monto = Int32.Parse(dr["monto"].ToString());
            transaccion.numero_tarjeta_destino = Int32.Parse(dr["numero_tarjeta_destino"].ToString());
            transaccion.numero_tarjeta_origen = Int32.Parse(dr["numero_tarjeta_origen"].ToString());
            return transaccion;
        }

        public int guardar(Transaccion transaccion)
        {
            Conexion conexion = new Conexion();
            int id = conexion.getSequenceValor("TRANSACCIONES_SEQ", 1);

            string query = "insert into TRANSACCIONES (COD_TRANSACCION, MONTO, COD_ARRIENDO, NUMERO_TARJETA_ORIGEN,NUMERO_TARJETA_DESTINO) values (";
            query += id + ",";
            query += transaccion.monto + ",";
            query += transaccion.cod_arriendo + ",";
            query += transaccion.numero_tarjeta_origen + ",";
            query += transaccion.numero_tarjeta_destino + ")";

            int filasIngresadas = conexion.ingresar(query);
            if (filasIngresadas > 0){
                this.actualizarSaldos(transaccion.numero_tarjeta_origen, 0, transaccion.monto);
                this.actualizarSaldos(transaccion.numero_tarjeta_destino, transaccion.monto, 0);
                return id;
            }else{
                return -1;
            }
        }

        public void actualizarSaldos(int codTarjeta, int saldoSuma = 0, int saldoResta = 0)
        {
            Tarjeta tarjeta = new Tarjeta().buscarPorPK(codTarjeta);
            Tarjeta nuevosDatos = new Tarjeta();
            int saldoActual = tarjeta.saldo;
            int nuevoSaldo = saldoActual + saldoSuma - saldoResta;
            nuevosDatos.cod_tarjeta = tarjeta.cod_tarjeta;
            nuevosDatos.saldo = nuevoSaldo;
            nuevosDatos.actualizar(nuevosDatos);
        }

        public Transaccion buscarPorCodArriendo(int codArriendo)
        {
            Transaccion transaccion = new Transaccion();
            Conexion conexion = new Conexion();
            string query = "select * from TRANSACCIONES where COD_ARRIENDO = " + codArriendo;

            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                transaccion = this.llenarObjeto(dr);
            }
            dr.Close();
            return transaccion;
        }

        public List<Transaccion> getTransaccionesTarjeta(int codTarjeta)
        {
            List<Transaccion> transacciones = new List<Transaccion>();
            Conexion conexion = new Conexion();
            string query = "select * from TRANSACCIONES where NUMERO_TARJETA_ORIGEN = " + codTarjeta + " or NUMERO_TARJETA_DESTINO = " + codTarjeta;

            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read())
            {
                Transaccion transaccion = this.llenarObjeto(dr);
                transaccion.Arriendo = new Arriendo().buscarPorPk(transaccion.cod_arriendo);
                transaccion.TarjetaOrigen = new Tarjeta().buscarPorPK(transaccion.numero_tarjeta_origen);
                transaccion.TarjetaDestino = new Tarjeta().buscarPorPK(transaccion.numero_tarjeta_destino);
                transacciones.Add(transaccion);
            }
            dr.Close();
            return transacciones;
        }
    }
}
