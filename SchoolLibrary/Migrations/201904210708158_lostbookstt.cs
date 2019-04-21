namespace SchoolLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lostbookstt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LostBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookID = c.Int(nullable: false),
                        LossReason = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LostBooks");
        }
    }
}
