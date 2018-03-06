using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Empresa_Fabricacion.Model;

namespace Empresa_Fabricacion.DAL
{
    public class EmpresaFabricacionContext: DbContext
    {
        public EmpresaFabricacionContext() : base("EmpresaFabricacionEntities") { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Fabricacion> Fabricaciones { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Venta> Ventas { get; set; }





        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
