namespace SchoolLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ISBN = c.String(unicode: false),
                        AccessionNumber = c.String(unicode: false),
                        Title = c.String(nullable: false, unicode: false),
                        Author = c.String(unicode: false),
                        Edition = c.String(unicode: false),
                        BookshelfNo = c.String(unicode: false),
                        RowNo = c.String(unicode: false),
                        ColumnPosition = c.String(unicode: false),
                        Count = c.Int(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BorrowedItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BorrowerId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        BorrowDate = c.DateTime(nullable: false, precision: 0),
                        ReturnDate = c.DateTime(nullable: false, precision: 0),
                        Returned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
            CreateTable(
                "Borrowers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, unicode: false),
                        LastName = c.String(nullable: false, unicode: false),
                        EmailAddress = c.String(unicode: false),
                        TypeName_Id = c.Int(nullable: false),
                        IdentificationNumber = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BorrowerTypes", t => t.TypeName_Id, cascadeDelete: true)
                .Index(t => t.TypeName_Id);
            
            CreateTable(
                "BorrowerTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Users",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(unicode: false),
                        Password = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Borrowers", "TypeName_Id", "BorrowerTypes");
            DropForeignKey("BorrowedItems", "BookId", "Books");
            DropIndex("Borrowers", new[] { "TypeName_Id" });
            DropIndex("BorrowedItems", new[] { "BookId" });
            DropTable("Users");
            DropTable("BorrowerTypes");
            DropTable("Borrowers");
            DropTable("BorrowedItems");
            DropTable("Books");
        }
    }
}
