using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa_Fabricacion.Model
{
    [Table ("Materiales")]
    public class Material : PropertyValidateModel
    {
        public Material()
        {
            Fabricaciones = new HashSet<Fabricacion>();
        }

        [Key]
        public int MaterialId { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
        public string Foto { get; set; }

        public int ProveedorId { get; set; }

        public virtual Proveedor Proveedores { get; set; }
        public virtual ICollection<Fabricacion> Fabricaciones { get; set; }
    }
}
