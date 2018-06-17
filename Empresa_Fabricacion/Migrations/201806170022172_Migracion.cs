namespace Empresa_Fabricacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migracion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Materiales", "Cantidad", c => c.Int(nullable: true));
            AlterColumn("dbo.Materiales", "PrecioTotal", c => c.Double(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Materiales", "PrecioTotal", c => c.Double());
            AlterColumn("dbo.Materiales", "Cantidad", c => c.Int());
        }
    }
}
