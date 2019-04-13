namespace SchoolLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BorrowedItems",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        BorrowDate = c.DateTime(nullable: false, precision: 0),
                        ReturnDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Borrowers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FirstName = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        IdentifierNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("Borrowers");
            DropTable("BorrowedItems");
        }
    }
}
