namespace SchoolLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBowedItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BorrowedItems", "BorrowerId", c => c.Int(nullable: false));
            AddColumn("dbo.BorrowedItems", "Returned", c => c.Boolean(nullable: false));
            DropColumn("dbo.BorrowedItems", "StudentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BorrowedItems", "StudentId", c => c.Int(nullable: false));
            DropColumn("dbo.BorrowedItems", "Returned");
            DropColumn("dbo.BorrowedItems", "BorrowerId");
        }
    }
}
