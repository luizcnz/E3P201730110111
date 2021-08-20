using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace E3P201730110111.Models
{
    public class Pagos
    {
        [PrimaryKey, AutoIncrement]
        public int id_pago { get; set; }

        [MaxLength(100)]
        public string Descripcion { get; set; }


        public double Monto { get; set; }

        [MaxLength(100)]
        public DateTime Fecha { get; set; }


        public byte[] Photo_recibo { get; set; }

        [MaxLength(200)]
        public string foto_ruta { get; set; }


    }
}
