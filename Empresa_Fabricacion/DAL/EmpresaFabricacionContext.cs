using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Empresa_Fabricacion.Model;
using System.Data.Entity.Migrations;
using Empresa_Fabricacion.Migrations;

namespace Empresa_Fabricacion.DAL
{
    public class EmpresaFabricacionContext : DbContext
    {
        public EmpresaFabricacionContext() : base("EmpresaFabricacionEntities")
        {

            if (Database.Exists())
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<EmpresaFabricacionContext, Configuration>());
            }
            else
            {
                Database.SetInitializer(new creardb());
            }
        }
        class creardb : CreateDatabaseIfNotExists<EmpresaFabricacionContext>
        {
            protected override void Seed(EmpresaFabricacionContext context)
            {
                context.Empleados.AddOrUpdate(
                    new Empleado { EmpleadoId = 001, Dni = "44457148b", Nombre = "David", Apellidos = "Blanco", Correo = "idemnias@gmail.com", Telefono = "669554433", TipoCuenta = "Administrador", Usuario = "d", Contraseña = "d" },
                    new Empleado { EmpleadoId = 002, Dni = "44457147c", Nombre = "Manolo", Apellidos = "Barbosa", Correo = "customcomputerdam@gmail.com", Telefono = "669889966", TipoCuenta = "Trabajador", Usuario = "s", Contraseña = "s" },
                    new Empleado { EmpleadoId = 003, Dni = "44457146a", Nombre = "Dosinda", Apellidos = "Cortiñas", Correo = "prueba3@gmail.com", Telefono = "654436598", TipoCuenta = "Vendedor", Usuario = "a", Contraseña = "a" }
                    );
                context.Proveedores.AddOrUpdate(
                    new Proveedor { ProveedorId = 0001, Nombre="Asus", Direccion="Madrid", Telefono="91456878", Contacto="Pablo", Correo="Pablo@asus.com"},
                    new Proveedor { ProveedorId = 0002, Nombre="Intel", Direccion="Madrid", Telefono="91789455", Contacto="Javier", Correo="Javier@intel.com"},
                    new Proveedor { ProveedorId = 0003, Nombre="Msi", Direccion="Barcelona", Telefono="935565588", Contacto="Xavi", Correo="Xavi@msi.com"},
                    new Proveedor { ProveedorId = 0004, Nombre="Crucial", Direccion="Ourense", Telefono="988225588", Contacto="Asunción", Correo="Asunción@crucial.com" },
                    new Proveedor { ProveedorId = 0005, Nombre="Custom Computer", Direccion="", Telefono="", Contacto="", Correo="Customcomputerdam@gmail.com" }
                    );
                context.Materiales.AddOrUpdate(
                    new Material { MaterialId = 1000, Nombre= "Montaje", Precio=50, Stock=1000, Foto= Environment.CurrentDirectory + @"\Imagenes\fabricacion.png", ProveedorId=0005 },
                    new Material { MaterialId = 1001, Nombre= "Asus ROG Strix Geforce GTX 1070 Ti Gaming Advance 8GB GDDR5", Precio=609.90, Stock=10, Foto= Environment.CurrentDirectory + @"\Imagenes\tarjetagrafica_asus.png", ProveedorId=0001 },
                    new Material { MaterialId = 1002, Nombre = "Asus Phoenix GeForce GTX 1060 3GB GDDR5", Precio = 255.90, Stock = 12, Foto = Environment.CurrentDirectory + @"\Imagenes\tarjetagrafica_asus.png", ProveedorId = 0001 },
                    new Material { MaterialId = 1003, Nombre = "Intel Core i7-8700K 3.7Ghz BOX", Precio = 325.90, Stock = 5, Foto = Environment.CurrentDirectory + @"\Imagenes\proc-intel-core-i7-8700k-37ghz.png", ProveedorId = 0002 },
                    new Material { MaterialId = 1004, Nombre = "Placa Base MSI X470 Gaming M7 AC", Precio = 274.90, Stock = 5, Foto = Environment.CurrentDirectory + @"\Imagenes\msi-placa-base-x470-gaming-pro-atx-am4.png", ProveedorId = 0003 },
                    new Material { MaterialId = 1005, Nombre = "Memoria Ram Crucial Ballistix Sport LT Single Rank DDR4 2666 PC4 - 21300 16GB CL16", Precio = 169, Stock = 20, Foto = Environment.CurrentDirectory + @"\Imagenes\memorias-ram-crucial-ddr4-16gb-2400mhz-ballistix-sport-lt-red-1g.png", ProveedorId = 0004 }
                    );
                context.Clientes.AddOrUpdate(
                    new Cliente { ClienteId=2001, Nombre="Pepe", Apellidos="Álvarez", NIF="44456148f", Direccion="Polvorín", Telefono="664871512", Correo="Pepe@gmail.com"},
                    new Cliente { ClienteId=2002, Nombre="Ariadna", Apellidos="Perez", NIF = "65145454s", Direccion ="Av.Habana", Telefono="699845782", Correo="Ariadna@gmail.com"},
                    new Cliente { ClienteId=2003, Nombre="Mónica", Apellidos="Naranjo", NIF = "444665588d", Direccion ="Juan XXIII", Telefono="655784848", Correo="Monica@gmail.com"}
                    );
                context.Productos.AddOrUpdate(
                    new Producto { ProductoId=3001, Nombre="La pecera", Precio=1800, Vendido=true, FechaVenta= new DateTime(2018, 06, 13),
                        Descripcion ="Ordenador gaming creado con las mejores piezas del mercado y con apariencia de una pecera",ClienteId=2001},
                    new Producto { ProductoId=3002, Nombre="La nevera", Precio=1800, Vendido=false, FechaVenta= DateTime.Today,
                        Descripcion ="Ordenador gaming creado con las mejores piezas del mercado y con apariencia de una nevera",ClienteId=2003}
                    );
                context.Fabricaciones.AddOrUpdate(
                    new Fabricacion { FabricacionId=4001,FechaInicio=new DateTime(2018,07,13), FechaAcaba=DateTime.Today ,Fabricado=false,ProductoId=3001,ClienteId=2001}
                    );
            }
        } 







public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Fabricacion> Fabricaciones { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }





        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
