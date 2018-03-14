namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DtAtividadeNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UsuarioFuncionario", "DtAtividade", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UsuarioFuncionario", "DtAtividade", c => c.DateTime(nullable: false));
        }
    }
}
