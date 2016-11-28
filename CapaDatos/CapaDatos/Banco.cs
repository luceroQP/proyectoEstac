using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class Banco
    {
        public int cod_banco { set; get; }
        public string nombre_banco { set; get; }
        public List<Tarjeta> tarjetas { set; get; }

        private Banco llenarObjeto(OracleDataReader dr)
        {
            Banco banco = new Banco();
            banco.cod_banco = Int32.Parse(dr["cod_banco"].ToString());
            banco.nombre_banco = dr["nombre_banco"].ToString();
            return banco;
        }

        public List<Banco> buscarTodos(bool llenaCombo = false){
            List<Banco> bancos = new List<Banco>();
            Conexion conexion = new Conexion();
            string query = "select * from BANCOS";

            OracleDataReader dr = conexion.consultar(query);
            while (dr.Read())
            {
                Banco banco = this.llenarObjeto(dr);
                bancos.Add(banco);
            }
            dr.Close();

            if (llenaCombo)
            {
                bancos.Insert(0, new Banco { cod_banco = 0, nombre_banco = "Seleccione"});
            }
            return bancos;
        }

        public Banco buscarPorPk(int codBanco)
        {
            Banco banco = new Banco();
            Conexion conexion = new Conexion();
            string query = "select * from BANCOS where cod_banco = "+ codBanco;

            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                banco = this.llenarObjeto(dr);
            }
            dr.Close();
            return banco;
        }
    }
}
