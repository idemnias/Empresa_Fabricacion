namespace Empresa_Fabricacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class otroid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Materiales", "FabricacionId", "dbo.Fabricaciones");
            DropIndex("dbo.Materiales", new[] { "FabricacionId" });
            AlterColumn("dbo.Materiales", "FabricacionId", c => c.Int());
            CreateIndex("dbo.Materiales", "FabricacionId");
            AddForeignKey("dbo.Materiales", "FabricacionId", "dbo.Fabricaciones", "FabricacionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Materiales", "FabricacionId", "dbo.Fabricaciones");
            DropIndex("dbo.Materiales", new[] { "FabricacionId" });
            AlterColumn("dbo.Materiales", "FabricacionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Materiales", "FabricacionId");
            AddForeignKey("dbo.Materiales", "FabricacionId", "dbo.Fabricaciones", "FabricacionId", cascadeDelete: true);
        }
    }
}
