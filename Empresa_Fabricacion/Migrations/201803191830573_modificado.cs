namespace Empresa_Fabricacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Materiales", "Foto", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Materiales", "Foto");
        }
    }
}
