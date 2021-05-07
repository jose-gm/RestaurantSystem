﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class RolesViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}
