using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa_Fabricacion.Model
{
    [Table ("Proveedores")]
    public class Proveedor : PropertyValidateModel
    {
        public Proveedor()
        {
            Materiales = new HashSet<Material>();
        }

        [Key]
        public int ProveedorId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contacto { get; set; }
        public string Correo { get; set; }

        public virtual ICollection<Material> Materiales { get; set; }
    }
}
