namespace DataAccess.Migrations
{
    using Helper;
    using Model.Enums;
    using System.Data.Entity.Migrations;
    
    public partial class EventoNotificacaoAtributoMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsuarioAtributo",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false),
                        Atributo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioId, t.Atributo })
                .ForeignKey("dbo.UsuarioFuncionario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.UsuarioNotificacao",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false),
                        TipoEvento = c.Int(nullable: false),
                        Notificar = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioId, t.TipoEvento })
                .ForeignKey("dbo.UsuarioFuncionario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Evento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false),
                        Descricao = c.String(nullable: false),
                        Resumo = c.String(nullable: false),
                        TipoEvento = c.Int(nullable: false),
                        EnviarEmail = c.Boolean(nullable: false),
                        Url = c.String(),
                        DtEvento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioNotificacao", "UsuarioId", "dbo.UsuarioFuncionario");
            DropForeignKey("dbo.UsuarioAtributo", "UsuarioId", "dbo.UsuarioFuncionario");
            DropIndex("dbo.UsuarioNotificacao", new[] { "UsuarioId" });
            DropIndex("dbo.UsuarioAtributo", new[] { "UsuarioId" });
            DropTable("dbo.Evento");
            DropTable("dbo.UsuarioNotificacao");
            DropTable("dbo.UsuarioAtributo");
        }
    }
}
