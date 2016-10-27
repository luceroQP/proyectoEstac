using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace CapaDatos
{
    public class CalificacionesTipo
    {
        public int cod_calificacion_tipo { get; set; }
        public string nombre_calificacion_tipo { get; set; }
        public List<Calificacion> calificaciones { get; set; }
    }
}
