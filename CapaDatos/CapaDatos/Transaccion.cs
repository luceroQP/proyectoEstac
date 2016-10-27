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
        public int numero_tajeta_origen { get; set; }
        public int numero_tajeta_destino { get; set; }
        public Arriendo Arriendo { get; set; }
        public Tarjeta TarjetaOrigen { get; set; }
        public Tarjeta TarjetaDestino { get; set; }
    }
}
