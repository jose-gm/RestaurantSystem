﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("ProductoDetalle")]
    public class ProductoDetalle
    {
        [Key]
        public int IdDetalle { get; set; }
        public int IdProducto { get; set; }
        public int IdIngrediente { get; set; }
        public int Cantidad { get; set; }
    }
}
