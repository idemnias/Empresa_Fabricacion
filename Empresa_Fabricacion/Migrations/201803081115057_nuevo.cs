namespace Empresa_Fabricacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nuevo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productos", "Descripcion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productos", "Descripcion");
        }
    }
}
