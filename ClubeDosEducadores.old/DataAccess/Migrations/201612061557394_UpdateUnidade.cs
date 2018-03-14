namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUnidade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Unidade", "Url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Unidade", "Url");
        }
    }
}
