﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class TarjetaTipo
    {
        public int cod_tarjeta_tipo { get; set; }
        public string nombre_tarjeta_tipo { get; set; }
        public List<Tarjeta> tarjetas { get; set; }

        private TarjetaTipo llenarObjeto(OracleDataReader dr)
        {
            TarjetaTipo tarjetaTipo = new TarjetaTipo();
            tarjetaTipo.cod_tarjeta_tipo = Int32.Parse(dr["cod_tarjeta_tipo"].ToString());
            tarjetaTipo.nombre_tarjeta_tipo = dr["nombre_tarjeta_tipo"].ToString();
            return tarjetaTipo;
        }

        public List<TarjetaTipo> buscarTodos(bool llenaSelect = false)
        { 
            List<TarjetaTipo> tipoTarjetas = new List<TarjetaTipo>();
            Conexion conexion = new Conexion();
            string query = "select * from TARJETA_TIPOS";
            OracleDataReader dr = conexion.consultar(query);
            while(dr.Read()){
                TarjetaTipo tarjetaTipo = this.llenarObjeto(dr);
                tipoTarjetas.Add(tarjetaTipo);
            }
            dr.Close();

            if (llenaSelect)
            {
                tipoTarjetas.Insert(0, new TarjetaTipo { cod_tarjeta_tipo=0, nombre_tarjeta_tipo="Seleccione"});
            }
            return tipoTarjetas;
        }

        public TarjetaTipo buscarPorPk(int tarjetaTipoId)
        {
            TarjetaTipo tipo = new TarjetaTipo();
            Conexion conexion = new Conexion();
            string query = "select * from TARJETA_TIPOS where cod_tarjeta_tipo = " + tarjetaTipoId;

            OracleDataReader dr = conexion.consultar(query);
            if (dr.Read())
            {
                tipo = this.llenarObjeto(dr);
            }
            dr.Close();
            return tipo;
        }
    }
}
