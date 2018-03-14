namespace DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class SendEmailPermutaMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UsuarioFuncionario", "SendEmailPermuta", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UsuarioFuncionario", "SendEmailPermuta");
        }
    }
}
