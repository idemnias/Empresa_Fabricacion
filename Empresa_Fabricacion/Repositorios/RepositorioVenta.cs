using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Empresa_Fabricacion.DAL;
using Empresa_Fabricacion.Model;

namespace Empresa_Fabricacion.Repositorios
{
    public class RepositorioVenta : RepositorioGenerico<Venta>
    {
        public RepositorioVenta(EmpresaFabricacionContext context) : base(context)
        {
        }
    }
}
