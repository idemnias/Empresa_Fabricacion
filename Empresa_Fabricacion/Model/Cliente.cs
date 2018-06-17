﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa_Fabricacion.Model
{
    [Table("Clientes")]
    public class Cliente : PropertyValidateModel
    {
        public Cliente()
        {
            Productos = new HashSet<Producto>();
            Fabricaciones = new HashSet<Fabricacion>();
        }

        [Key]
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string NIF { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }


        public virtual ICollection<Producto> Productos { get; set; }
        public virtual ICollection<Fabricacion> Fabricaciones { get; set; }

    }
}
