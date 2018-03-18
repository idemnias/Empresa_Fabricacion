namespace Empresa_Fabricacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idempleado : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Fabricaciones", "EmpleadoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fabricaciones", "EmpleadoId", c => c.Int(nullable: false));
        }
    }
}
