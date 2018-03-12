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
        public int EmpleadoId { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string TipoCuenta { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public int? FabricacionId { get; set; }

        public virtual Fabricacion Fabricaciones { get; set; }
    }
}
