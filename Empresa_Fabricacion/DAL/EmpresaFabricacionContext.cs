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
                    new Empleado { EmpleadoId = 001, Dni = "44457678b", Nombre = "David", Apellidos = "Blanco", Correo = "miaplicacion@gmail.com", Telefono = "669554433", TipoCuenta = "Administrador", Usuario = "d", Contraseña = "d" },
                    new Empleado { EmpleadoId = 002, Dni = "44455777c", Nombre = "Manolo", Apellidos = "Barbosa", Correo = "customcomputerdam@gmail.com", Telefono = "669889966", TipoCuenta = "Trabajador", Usuario = "s", Contraseña = "s" },
                    new Empleado { EmpleadoId = 003, Dni = "44457146a", Nombre = "Dosinda", Apellidos = "Cortiñas", Correo = "prueba3@gmail.com", Telefono = "654436598", TipoCuenta = "Vendedor", Usuario = "a", Contraseña = "a" }
                    );
                context.Categorias.AddOrUpdate(
                    new Categoria { CategoriaId=5000, Nombre="Montaje"},
                    new Categoria { CategoriaId=5001, Nombre="Placa base"},
                    new Categoria { CategoriaId=5002, Nombre="Procesador"},
                    new Categoria { CategoriaId=5003, Nombre="Memoria"},
                    new Categoria { CategoriaId=5004, Nombre="Tarjeta gráfica"},
                    new Categoria { CategoriaId=5005, Nombre="Fuente de alimentación"},
                    new Categoria { CategoriaId=5006, Nombre="Disco duro"},
                    new Categoria { CategoriaId=5007, Nombre="Periféricos"},
                    new Categoria { CategoriaId=5008, Nombre="Torres y cajas"}
                    );
                context.Proveedores.AddOrUpdate(
                    new Proveedor { ProveedorId = 0001, Nombre="Asus", Direccion="Madrid", Telefono="91456878", Contacto="Pablo", Correo="Pablo@asus.com"},
                    new Proveedor { ProveedorId = 0002, Nombre="Intel", Direccion="Madrid", Telefono="91789455", Contacto="Javier", Correo="Javier@intel.com"},
                    new Proveedor { ProveedorId = 0003, Nombre="Msi", Direccion="Barcelona", Telefono="935565588", Contacto="Xavi", Correo="Xavi@msi.com"},
                    new Proveedor { ProveedorId = 0004, Nombre="Crucial", Direccion="Ourense", Telefono="988225588", Contacto="Asunción", Correo="Asunción@crucial.com" },
                    new Proveedor { ProveedorId = 0005, Nombre="Custom Computer", Direccion="", Telefono="", Contacto="", Correo="Customcomputerdam@gmail.com" },
                    new Proveedor { ProveedorId = 0006, Nombre="Barracuda", Direccion="Ourense", Telefono="988153545", Contacto="Jason", Correo="Jason@barracuda.com" },
                    new Proveedor { ProveedorId = 0007, Nombre="MetalBlade", Direccion="Ourense", Telefono="988884516", Contacto="Carlos", Correo="Carlos@metalblade.com" },
                    new Proveedor { ProveedorId = 0008, Nombre="Samsung", Direccion="Ourense", Telefono="988359988", Contacto="Morfeo", Correo="Morfeo@samsung.com" },
                    new Proveedor { ProveedorId = 0009, Nombre="Razer", Direccion="Ourense", Telefono="988486544", Contacto="Manu", Correo="Manu@razer.com" }
                    );
                context.Materiales.AddOrUpdate(
                    new Material { MaterialId = 1000, Nombre= "Montaje", Precio=50, Stock=1000, Foto= Environment.CurrentDirectory + @"\Imagenes\fabricacion.png", ProveedorId=0005, CategoriaId=5000 },
                    new Material { MaterialId = 1001, Nombre= "Asus Geforce GTX 1070 Ti Gaming 8GB GDDR5", Precio=609.90, Stock=10, Foto= Environment.CurrentDirectory + @"\Imagenes\Tarjetagrafica.png", ProveedorId=0001, CategoriaId=5004},
                    new Material { MaterialId = 1002, Nombre = "Asus Phoenix GeForce GTX 1060 3GB GDDR5", Precio = 255.90, Stock = 0, Foto = Environment.CurrentDirectory + @"\Imagenes\tarjetagrafica_asus.png", ProveedorId = 0001, CategoriaId = 5004},
                    new Material { MaterialId = 1003, Nombre = "Intel Core i7-8700K 3.7Ghz BOX", Precio = 325.90, Stock = 5, Foto = Environment.CurrentDirectory + @"\Imagenes\proc-intel-core-i7-8700k-37ghz.png", ProveedorId = 0002, CategoriaId=5002 },
                    new Material { MaterialId = 1004, Nombre = "Placa Base MSI X470 Gaming M7 AC", Precio = 274.90, Stock = 5, Foto = Environment.CurrentDirectory + @"\Imagenes\msi-placa-base-x470-gaming-pro-atx-am4.png", ProveedorId = 0003, CategoriaId=5001},
                    new Material { MaterialId = 1005, Nombre = "Memoria Ram Crucial Ballistix Sport DDR4 2666 16GB CL16", Precio = 169, Stock = 20, Foto = Environment.CurrentDirectory + @"\Imagenes\memorias-ram-crucial-ddr4-16gb-2400mhz-ballistix-sport-lt-red-1g.png", ProveedorId = 0004, CategoriaId=5003},
                    new Material { MaterialId = 1006, Nombre= "Fuente de alimentacion MetalBlade 750W", Precio = 74, Stock =100, Foto= Environment.CurrentDirectory + @"\Imagenes\Fuente de alimentacion.png", ProveedorId=0007, CategoriaId=5005},
                    new Material { MaterialId = 1007, Nombre= "Disco duro 1tb Barracuda", Precio = 44.59, Stock =75, Foto= Environment.CurrentDirectory + @"\Imagenes\discoduronormal.png", ProveedorId=0006, CategoriaId=5006},
                    new Material { MaterialId = 1008, Nombre= "Disco duro m.2 Samsung EVO 850 1tb ", Precio = 330, Stock =10, Foto= Environment.CurrentDirectory + @"\Imagenes\discom2.png", ProveedorId=0008, CategoriaId=5006},
                    new Material { MaterialId = 1009, Nombre= "Caja Custom Pecera", Precio = 130, Stock =2, Foto= Environment.CurrentDirectory + @"\Imagenes\pecera.png", ProveedorId=0005, CategoriaId=5008 },
                    new Material { MaterialId = 1010, Nombre= "Caja Custom Cubo", Precio = 170, Stock =5, Foto= Environment.CurrentDirectory + @"\Imagenes\iconofactura.png", ProveedorId=0009, CategoriaId=5008 }
                    );
                context.Clientes.AddOrUpdate(
                    new Cliente { ClienteId=2001, Nombre="Pepe", Apellidos="Álvarez", NIF="44456148f", Direccion="Polvorín", Telefono="664871512", Correo="Pepe@gmail.com"},
                    new Cliente { ClienteId=2002, Nombre="Ariadna", Apellidos="Perez", NIF = "65145454s", Direccion ="Av.Habana", Telefono="699845782", Correo="Ariadna@gmail.com"},
                    new Cliente { ClienteId=2003, Nombre="Mónica", Apellidos="Naranjo", NIF = "444665588d", Direccion ="Juan XXIII", Telefono="655784848", Correo="Monica@gmail.com"}
                    );
                context.Productos.AddOrUpdate(
                    new Producto { ProductoId=3001, Nombre="La pecera", Precio=1800, Pagado=true, FechaVenta= new DateTime(2018, 06, 13),
                        Descripcion ="Ordenador gaming creado con las mejores piezas del mercado y con apariencia de una pecera",ClienteId=2001},
                    new Producto { ProductoId=3002, Nombre="The cube", Precio=1800, Pagado=false, FechaVenta= DateTime.Today,
                        Descripcion ="Ordenador gaming creado con las mejores piezas del mercado y con apariencia de un cubo",ClienteId=2003}
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
        public DbSet<Categoria> Categorias { get; set; }





        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
