namespace Empresa_Fabricacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _base : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Materiales", "FabricacionId", "dbo.Fabricaciones");
            DropIndex("dbo.Materiales", new[] { "FabricacionId" });
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
            
            AddColumn("dbo.Fabricaciones", "Fabricado", c => c.Boolean(nullable: false));
            DropColumn("dbo.Materiales", "FabricacionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Materiales", "FabricacionId", c => c.Int());
            DropForeignKey("dbo.MaterialFabricacions", "Fabricacion_FabricacionId", "dbo.Fabricaciones");
            DropForeignKey("dbo.MaterialFabricacions", "Material_MaterialId", "dbo.Materiales");
            DropIndex("dbo.MaterialFabricacions", new[] { "Fabricacion_FabricacionId" });
            DropIndex("dbo.MaterialFabricacions", new[] { "Material_MaterialId" });
            DropColumn("dbo.Fabricaciones", "Fabricado");
            DropTable("dbo.MaterialFabricacions");
            CreateIndex("dbo.Materiales", "FabricacionId");
            AddForeignKey("dbo.Materiales", "FabricacionId", "dbo.Fabricaciones", "FabricacionId");
        }
    }
}
