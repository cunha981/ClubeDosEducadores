namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Permuta : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permuta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ObservacaoFuncionario = c.String(nullable: false),
                        ObservacaoPermuta = c.String(nullable: false),
                        DtPublicacao = c.DateTime(nullable: false),
                        DtExclusao = c.DateTime(),
                        MotivoExclusao = c.String(),
                        FuncionarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funcionario", t => t.FuncionarioId, cascadeDelete: true)
                .Index(t => t.FuncionarioId);
            
            CreateTable(
                "dbo.PermutaRegiaoUnidade",
                c => new
                    {
                        PermutaId = c.Int(nullable: false),
                        RegiaoUnidadeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermutaId, t.RegiaoUnidadeId })
                .ForeignKey("dbo.Permuta", t => t.PermutaId, cascadeDelete: true)
                .ForeignKey("dbo.RegiaoUnidade", t => t.RegiaoUnidadeId, cascadeDelete: true)
                .Index(t => t.PermutaId)
                .Index(t => t.RegiaoUnidadeId);
            
            CreateTable(
                "dbo.PermutaTipoUnidade",
                c => new
                    {
                        PermutaId = c.Int(nullable: false),
                        TipoUnidadeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermutaId, t.TipoUnidadeId })
                .ForeignKey("dbo.Permuta", t => t.PermutaId, cascadeDelete: true)
                .ForeignKey("dbo.TipoUnidade", t => t.TipoUnidadeId, cascadeDelete: true)
                .Index(t => t.PermutaId)
                .Index(t => t.TipoUnidadeId);
            
            CreateIndex("dbo.Funcionario", "UnidadeTrabalhoId");
            AddForeignKey("dbo.Funcionario", "UnidadeTrabalhoId", "dbo.Unidade", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Funcionario", "UnidadeTrabalhoId", "dbo.Unidade");
            DropForeignKey("dbo.PermutaRegiaoUnidade", "RegiaoUnidadeId", "dbo.RegiaoUnidade");
            DropForeignKey("dbo.PermutaTipoUnidade", "TipoUnidadeId", "dbo.TipoUnidade");
            DropForeignKey("dbo.PermutaTipoUnidade", "PermutaId", "dbo.Permuta");
            DropForeignKey("dbo.PermutaRegiaoUnidade", "PermutaId", "dbo.Permuta");
            DropForeignKey("dbo.Permuta", "FuncionarioId", "dbo.Funcionario");
            DropIndex("dbo.PermutaTipoUnidade", new[] { "TipoUnidadeId" });
            DropIndex("dbo.PermutaTipoUnidade", new[] { "PermutaId" });
            DropIndex("dbo.PermutaRegiaoUnidade", new[] { "RegiaoUnidadeId" });
            DropIndex("dbo.PermutaRegiaoUnidade", new[] { "PermutaId" });
            DropIndex("dbo.Permuta", new[] { "FuncionarioId" });
            DropIndex("dbo.Funcionario", new[] { "UnidadeTrabalhoId" });
            DropTable("dbo.PermutaTipoUnidade");
            DropTable("dbo.PermutaRegiaoUnidade");
            DropTable("dbo.Permuta");
        }
    }
}
