using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa_Fabricacion.Model
{
    [Table("Empleados")]
    public class Empleado
    {
        [Key]
        public int ClienteId;
        public string Dni;
        public string Nombre;
        public string Direccion;
        public string Telefono;

        public virtual Fabricacion Fabricaciones { get; set; }
    }
}
