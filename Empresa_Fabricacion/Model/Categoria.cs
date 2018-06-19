using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa_Fabricacion.Model
{
    [Table("Categoria")]
    public class Categoria : PropertyValidateModel
    {
        public Categoria()
        {
            Materiales = new HashSet<Material>();
        }

        [Key]
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Material> Materiales { get; set; }
    }
}
