namespace Empresa_Fabricacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migracion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Apellidos = c.String(),
                        Direccion = c.String(),
                        Telefono = c.String(),
                        Correo = c.String(),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.Fabricaciones",
                c => new
                    {
                        FabricacionId = c.Int(nullable: false, identity: true),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaAcaba = c.DateTime(nullable: false),
                        Fabricado = c.Boolean(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        ClienteId = c.Int(),
                    })
                .PrimaryKey(t => t.FabricacionId)
                .ForeignKey("dbo.Productos", t => t.ProductoId, cascadeDelete: true)
                .ForeignKey("dbo.Clientes", t => t.ClienteId)
                .Index(t => t.ProductoId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Empleados",
                c => new
                    {
                        EmpleadoId = c.Int(nullable: false, identity: true),
                        Dni = c.String(),
                        Nombre = c.String(),
                        Apellidos = c.String(),
                        Correo = c.String(),
                        Telefono = c.String(),
                        TipoCuenta = c.String(),
                        Usuario = c.String(),
                        Contraseña = c.String(),
                        FabricacionId = c.Int(),
                    })
                .PrimaryKey(t => t.EmpleadoId)
                .ForeignKey("dbo.Fabricaciones", t => t.FabricacionId)
                .Index(t => t.FabricacionId);
            
            CreateTable(
                "dbo.Materiales",
                c => new
                    {
                        MaterialId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Precio = c.Double(nullable: false),
                        Stock = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        PrecioTotal = c.Double(nullable: false),
                        Foto = c.String(),
                        ProveedorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaterialId)
                .ForeignKey("dbo.Proveedores", t => t.ProveedorId, cascadeDelete: true)
                .Index(t => t.ProveedorId);
            
            CreateTable(
                "dbo.Proveedores",
                c => new
                    {
                        ProveedorId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Direccion = c.String(),
                        Telefono = c.String(),
                        Contacto = c.String(),
                        Correo = c.String(),
                    })
                .PrimaryKey(t => t.ProveedorId);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        ProductoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Precio = c.Double(nullable: false),
                        Vendido = c.Boolean(nullable: false),
                        FechaVenta = c.DateTime(nullable: false),
                        Descripcion = c.String(),
                        ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductoId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.MaterialFabricacions",
                c => new
                    {
                        Material_MaterialId = c.Int(nullable: false),
                        Fabricacion_FabricacionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Material_MaterialId, t.Fabricacion_FabricacionId })
                .ForeignKey("dbo.Materiales", t => t.Material_MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.Fabricaciones", t => t.Fabricacion_FabricacionId, cascadeDelete: true)
                .Index(t => t.Material_MaterialId)
                .Index(t => t.Fabricacion_FabricacionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fabricaciones", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.Fabricaciones", "ProductoId", "dbo.Productos");
            DropForeignKey("dbo.Productos", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.Materiales", "ProveedorId", "dbo.Proveedores");
            DropForeignKey("dbo.MaterialFabricacions", "Fabricacion_FabricacionId", "dbo.Fabricaciones");
            DropForeignKey("dbo.MaterialFabricacions", "Material_MaterialId", "dbo.Materiales");
            DropForeignKey("dbo.Empleados", "FabricacionId", "dbo.Fabricaciones");
            DropIndex("dbo.MaterialFabricacions", new[] { "Fabricacion_FabricacionId" });
            DropIndex("dbo.MaterialFabricacions", new[] { "Material_MaterialId" });
            DropIndex("dbo.Productos", new[] { "ClienteId" });
            DropIndex("dbo.Materiales", new[] { "ProveedorId" });
            DropIndex("dbo.Empleados", new[] { "FabricacionId" });
            DropIndex("dbo.Fabricaciones", new[] { "ClienteId" });
            DropIndex("dbo.Fabricaciones", new[] { "ProductoId" });
            DropTable("dbo.MaterialFabricacions");
            DropTable("dbo.Productos");
            DropTable("dbo.Proveedores");
            DropTable("dbo.Materiales");
            DropTable("dbo.Empleados");
            DropTable("dbo.Fabricaciones");
            DropTable("dbo.Clientes");
        }
    }
}
