using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Calificacion
    {
        public int cod_calificacion { get; set; }
        public int nota { get; set; }
        public string observacion { get; set; }
        public int cod_calificacion_tipo { get; set; }
        public int cod_usuario_calificador { get; set; }
        public int cod_usuario_calificado { get; set; }
        public CalificacionesTipo CalificacionTipo { get; set; }
        public Usuario UsuarioCalificador { get; set; }
        public Usuario UsuarioCalificado { get; set; }

        public Calificacion(){}
    }
}
