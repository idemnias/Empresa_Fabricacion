using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Empresa_Fabricacion.Repositorios;

namespace Empresa_Fabricacion.DAL
{
    public class UnitOfWork
    {
        private EmpresaFabricacionContext context = new EmpresaFabricacionContext();
        private RepositorioCliente cliente;
        private RepositorioEmpleado empleado;
        private RepositorioFabricacion fabricacion;
        private RepositorioMaterial material;
        private RepositorioProducto producto;
        private RepositorioProveedor proveedor;

        public RepositorioCliente RepositorioCliente
        {
            get
            {
                if (this.cliente == null)
                {
                    this.cliente = new RepositorioCliente(context);
                }
                return cliente;
            }
        }

        public RepositorioEmpleado RepositorioEmpleado
        {
            get
            {
                if (this.empleado == null)
                {
                    this.empleado = new RepositorioEmpleado(context);
                }
                return empleado;
            }
        }

        public RepositorioFabricacion RepositorioFabricacion
        {
            get
            {
                if (this.fabricacion == null)
                {
                    this.fabricacion = new RepositorioFabricacion(context);
                }
                return fabricacion;
            }
        }

        public RepositorioMaterial RepositorioMaterial
        {
            get
            {
                if (this.material == null)
                {
                    this.material = new RepositorioMaterial(context);
                }
                return material;
            }
        }

        public RepositorioProducto RepositorioProducto
        {
            get
            {
                if (this.producto == null)
                {
                    this.producto = new RepositorioProducto(context);
                }
                return producto;
            }
        }

        public RepositorioProveedor RepositorioProveedor
        {
            get
            {
                if (this.proveedor == null)
                {
                    this.proveedor = new RepositorioProveedor(context);
                }
                return proveedor;
            }
        }


    }
}
