namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dadosFuncionario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Funcionario", "UnidadeTrabalhoId", c => c.Int());
            AddColumn("dbo.Funcionario", "HorarioTrabalho", c => c.String());
            AddColumn("dbo.Funcionario", "Telefone", c => c.String());
            AddColumn("dbo.Funcionario", "Celular", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Funcionario", "Celular");
            DropColumn("dbo.Funcionario", "Telefone");
            DropColumn("dbo.Funcionario", "HorarioTrabalho");
            DropColumn("dbo.Funcionario", "UnidadeTrabalhoId");
        }
    }
}
