namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dadosFuncionario1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Funcionario", "HorarioTrabalho");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Funcionario", "HorarioTrabalho", c => c.String());
        }
    }
}
