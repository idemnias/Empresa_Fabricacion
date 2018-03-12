namespace Empresa_Fabricacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empleados", "TipoCuenta", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empleados", "TipoCuenta");
        }
    }
}
