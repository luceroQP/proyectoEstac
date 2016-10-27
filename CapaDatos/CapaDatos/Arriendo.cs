using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class Arriendo
    {
        public int cod_arriendo {get; set;}
        public DateTime inicio_arriendo { get; set; }
        public DateTime fin_arriendo { get; set; }
        public int horas_usadas { get; set; }
        public int cod_estacionamiento { get; set; }
        public int cod_vehiculo { get; set; }
        public Estacionamiento Estacionamiento {get; set;}
        public Vehiculo Vehiculo { get; set; }
    }
}
