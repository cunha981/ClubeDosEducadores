namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vagas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VagaRemocao", "VagasPotenciais", c => c.Int());
            AddColumn("dbo.VagaRemocao", "VagasIniciais", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.VagaRemocao", "VagasIniciais");
            DropColumn("dbo.VagaRemocao", "VagasPotenciais");
        }
    }
}
