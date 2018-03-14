namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MailProvider : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MailTemplate",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MailTemplate");
        }
    }
}
