namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUnidadeAvalicao : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UnidadeAvaliacaos", newName: "UnidadeAvaliacao");
            DropIndex("dbo.UnidadeAvaliacao", new[] { "FuncionarioId" });
            DropIndex("dbo.UnidadeAvaliacao", new[] { "UnidadeId" });
            CreateIndex("dbo.UnidadeAvaliacao", new[] { "FuncionarioId", "UnidadeId" }, unique: true, name: "IX_UnidadeAvaliacao_FuncionarioId_UnidadeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UnidadeAvaliacao", "IX_UnidadeAvaliacao_FuncionarioId_UnidadeId");
            CreateIndex("dbo.UnidadeAvaliacao", "UnidadeId");
            CreateIndex("dbo.UnidadeAvaliacao", "FuncionarioId");
            RenameTable(name: "dbo.UnidadeAvaliacao", newName: "UnidadeAvaliacaos");
        }
    }
}
