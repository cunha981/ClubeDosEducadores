namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ControlUserAccess : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UsuarioFuncionario", "SessionId", c => c.String());
            AddColumn("dbo.UsuarioFuncionario", "DtAtividade", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UsuarioFuncionario", "DtAtividade");
            DropColumn("dbo.UsuarioFuncionario", "SessionId");
        }
    }
}
