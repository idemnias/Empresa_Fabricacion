using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa_Fabricacion.Model
{
    [Table ("Productos")]
    public class Producto
    {
        public Producto()
        {
            Fabricaciones = new HashSet<Fabricacion>();
        }

        [Key]
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }

        public virtual Venta Ventas { get; set; }
        public virtual ICollection<Fabricacion> Fabricaciones { get; set; }
        

    }
}
