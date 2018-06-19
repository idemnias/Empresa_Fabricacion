using Empresa_Fabricacion.DAL;
using Empresa_Fabricacion.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa_Fabricacion.Repositorios
{
    public class RepositorioCategoria : RepositorioGenerico<Categoria>
    {
        public RepositorioCategoria(EmpresaFabricacionContext context) : base(context)
        {
        }
    }
}
