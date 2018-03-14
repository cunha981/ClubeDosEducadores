namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnidadeAvaliacao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UnidadeAvaliacaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comentario = c.String(),
                        Nota = c.Int(nullable: false),
                        DtAvaliacao = c.DateTime(nullable: false),
                        FuncionarioId = c.Int(nullable: false),
                        UnidadeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funcionario", t => t.FuncionarioId, cascadeDelete: true)
                .ForeignKey("dbo.Unidade", t => t.UnidadeId, cascadeDelete: true)
                .Index(t => t.FuncionarioId)
                .Index(t => t.UnidadeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UnidadeAvaliacaos", "UnidadeId", "dbo.Unidade");
            DropForeignKey("dbo.UnidadeAvaliacaos", "FuncionarioId", "dbo.Funcionario");
            DropIndex("dbo.UnidadeAvaliacaos", new[] { "UnidadeId" });
            DropIndex("dbo.UnidadeAvaliacaos", new[] { "FuncionarioId" });
            DropTable("dbo.UnidadeAvaliacaos");
        }
    }
}
