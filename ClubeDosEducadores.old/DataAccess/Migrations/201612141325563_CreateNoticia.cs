namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNoticia : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Noticia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false),
                        Conteudo = c.String(nullable: false),
                        DtPublicacao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Noticia");
        }
    }
}
