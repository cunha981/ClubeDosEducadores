namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FaqMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PerguntaFrequente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pergunta = c.String(nullable: false),
                        Resposta = c.String(nullable: false),
                        Tags = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PerguntaFrequente");
        }
    }
}
