namespace Empresa_Fabricacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migracion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.CategoriaId);
            
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
                        CategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaterialId)
                .ForeignKey("dbo.Categoria", t => t.CategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.Proveedores", t => t.ProveedorId, cascadeDelete: true)
                .Index(t => t.ProveedorId)
                .Index(t => t.CategoriaId);
            
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
                .ForeignKey("dbo.Clientes", t => t.ClienteId)
                .ForeignKey("dbo.Productos", t => t.ProductoId, cascadeDelete: true)
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
                "dbo.Productos",
                c => new
                    {
                        ProductoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Precio = c.Double(nullable: false),
                        Pagado = c.Boolean(nullable: false),
                        FechaVenta = c.DateTime(nullable: false),
                        Descripcion = c.String(),
                        ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductoId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Apellidos = c.String(),
                        NIF = c.String(),
                        Direccion = c.String(),
                        Telefono = c.String(),
                        Correo = c.String(),
                    })
                .PrimaryKey(t => t.ClienteId);
            
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
                "dbo.FabricacionMaterials",
                c => new
                    {
                        Fabricacion_FabricacionId = c.Int(nullable: false),
                        Material_MaterialId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Fabricacion_FabricacionId, t.Material_MaterialId })
                .ForeignKey("dbo.Fabricaciones", t => t.Fabricacion_FabricacionId, cascadeDelete: true)
                .ForeignKey("dbo.Materiales", t => t.Material_MaterialId, cascadeDelete: true)
                .Index(t => t.Fabricacion_FabricacionId)
                .Index(t => t.Material_MaterialId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Materiales", "ProveedorId", "dbo.Proveedores");
            DropForeignKey("dbo.Fabricaciones", "ProductoId", "dbo.Productos");
            DropForeignKey("dbo.Productos", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.Fabricaciones", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.FabricacionMaterials", "Material_MaterialId", "dbo.Materiales");
            DropForeignKey("dbo.FabricacionMaterials", "Fabricacion_FabricacionId", "dbo.Fabricaciones");
            DropForeignKey("dbo.Empleados", "FabricacionId", "dbo.Fabricaciones");
            DropForeignKey("dbo.Materiales", "CategoriaId", "dbo.Categoria");
            DropIndex("dbo.FabricacionMaterials", new[] { "Material_MaterialId" });
            DropIndex("dbo.FabricacionMaterials", new[] { "Fabricacion_FabricacionId" });
            DropIndex("dbo.Productos", new[] { "ClienteId" });
            DropIndex("dbo.Empleados", new[] { "FabricacionId" });
            DropIndex("dbo.Fabricaciones", new[] { "ClienteId" });
            DropIndex("dbo.Fabricaciones", new[] { "ProductoId" });
            DropIndex("dbo.Materiales", new[] { "CategoriaId" });
            DropIndex("dbo.Materiales", new[] { "ProveedorId" });
            DropTable("dbo.FabricacionMaterials");
            DropTable("dbo.Proveedores");
            DropTable("dbo.Clientes");
            DropTable("dbo.Productos");
            DropTable("dbo.Empleados");
            DropTable("dbo.Fabricaciones");
            DropTable("dbo.Materiales");
            DropTable("dbo.Categoria");
        }
    }
}
