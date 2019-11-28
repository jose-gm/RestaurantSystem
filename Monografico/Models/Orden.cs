﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("Orden", Schema = "dbo")]
    public class Orden
    {
        public int IdOrden { get; set; }
        public int IdCuenta { get; set; }
        public DateTime Fecha { get; set; }
        public List<OrdenDetalle> Detalle { get; set; }
    }
}
