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
    public class Producto : PropertyValidateModel
    {
        public Producto()
        {
            Fabricaciones = new HashSet<Fabricacion>();
        }

        [Key]
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public bool Vendido { get; set; }
        public DateTime FechaVenta { get; set; }
        public string Descripcion { get; set; }

        public int ClienteId { get; set; }

        public virtual Cliente Clientes { get; set; }
        public virtual ICollection<Fabricacion> Fabricaciones { get; set; }
        

    }
}
