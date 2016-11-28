using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class EstacionamientoEstado
    {
        public int cod_estacionamiento_estado { get; set; }
        public string nombre_estacionamiento_estado { get; set; }
        //public List<Estacionamiento> estacionamientos { get; set; }

        private EstacionamientoEstado llenarObjeto(OracleDataReader dr)
        {
            EstacionamientoEstado estado = new EstacionamientoEstado();
            estado.cod_estacionamiento_estado = Int32.Parse(dr["cod_estacionamiento_estado"].ToString());
            estado.nombre_estacionamiento_estado = dr["nombre_estacionamiento_estado"].ToString();
            return estado;
        }

        public EstacionamientoEstado buscarPorPk(int cod)
        {
            EstacionamientoEstado estacionamientoEstado = new EstacionamientoEstado();
            Conexion conexion = new Conexion();
            string query = "select * from ESTACIONAMIENTO_ESTADOS where COD_ESTACIONAMIENTO_ESTADO = "+cod;
            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                estacionamientoEstado = this.llenarObjeto(dr);
            }
            dr.Close();
            return estacionamientoEstado;
        }

        public List<EstacionamientoEstado> buscarTodos(Boolean llenarCombo = false)
        {
            List<EstacionamientoEstado> estados = new List<EstacionamientoEstado>();
            Conexion conexion = new Conexion();
            string query = "select * from ESTACIONAMIENTO_ESTADOS";

            OracleDataReader dr = conexion.consultar(query);
            while(dr.Read()){
                EstacionamientoEstado estado = this.llenarObjeto(dr);
                estados.Add(estado);
            }
            dr.Close();
            if (llenarCombo)
            {
                estados.Insert(0, new EstacionamientoEstado { cod_estacionamiento_estado=0,nombre_estacionamiento_estado="Seleccione"});
            }
            return estados;
        }
    }
}
