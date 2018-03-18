using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa_Fabricacion.Model
{
    [Table("Fabricaciones")]
    public class Fabricacion : PropertyValidateModel
    {
        public Fabricacion()
        {
            Materiales = new HashSet<Material>();
            Empleados = new HashSet<Empleado>();
        }

        [Key]
        public int FabricacionId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaAcaba { get; set; }

        public int ProductoId { get; set; }

        public virtual Producto Productos { get; set; }
        public virtual ICollection<Empleado> Empleados { get; set; }
        public virtual ICollection<Material> Materiales { get; set; }

    }
}
