namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class melhorias111016 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Remocao", "UnidadeId", "dbo.Unidade");
            DropIndex("dbo.Remocao", "IX_Remocao_FuncionarioId_UnidadeId_Preferencia");
            DropPrimaryKey("dbo.Remocao");
            AddColumn("dbo.Remocao", "VagaRemocaoId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Remocao", new[] { "VagaRemocaoId", "FuncionarioId" });
            CreateIndex("dbo.Remocao", new[] { "FuncionarioId", "VagaRemocaoId", "Preferencia" }, unique: true, name: "IX_Remocao_FuncionarioId_VagaRemocaoId_Preferencia");
            AddForeignKey("dbo.Remocao", "VagaRemocaoId", "dbo.VagaRemocao", "Id");
            DropColumn("dbo.Remocao", "Id");
            DropColumn("dbo.Remocao", "UnidadeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Remocao", "UnidadeId", c => c.Int(nullable: false));
            AddColumn("dbo.Remocao", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Remocao", "VagaRemocaoId", "dbo.VagaRemocao");
            DropIndex("dbo.Remocao", "IX_Remocao_FuncionarioId_VagaRemocaoId_Preferencia");
            DropPrimaryKey("dbo.Remocao");
            DropColumn("dbo.Remocao", "VagaRemocaoId");
            AddPrimaryKey("dbo.Remocao", "Id");
            CreateIndex("dbo.Remocao", new[] { "FuncionarioId", "UnidadeId", "Preferencia" }, unique: true, name: "IX_Remocao_FuncionarioId_UnidadeId_Preferencia");
            AddForeignKey("dbo.Remocao", "UnidadeId", "dbo.Unidade", "Id", cascadeDelete: true);
        }
    }
}
