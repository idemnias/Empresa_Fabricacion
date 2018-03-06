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
    public class Fabricacion
    {
        public Fabricacion()
        {
            Materiales = new HashSet<Material>();
        }

        [Key]
        public int FabricacionId;
        public DateTime FechaInicio;
        public DateTime FechaAcaba;

        public virtual Empleado Empleados { get; set; }
        public virtual Producto Productos { get; set; }
        public virtual ICollection<Material> Materiales { get; set; }

    }
}
