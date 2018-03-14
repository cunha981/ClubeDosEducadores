namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventoDtCadastroMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Evento", "DtCadastro", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Evento", "DtCadastro");
        }
    }
}
