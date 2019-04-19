namespace SchoolLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            
            
            
            CreateTable(
                "dbo.Borrowers",
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
                .ForeignKey("dbo.BorrowerTypes", t => t.TypeName_Id, cascadeDelete: true)
                .Index(t => t.TypeName_Id);
            
            CreateTable(
                "dbo.BorrowerTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
  
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Borrowers", "TypeName_Id", "dbo.BorrowerTypes");
            DropIndex("dbo.Borrowers", new[] { "TypeName_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.BorrowerTypes");
            DropTable("dbo.Borrowers");
            DropTable("dbo.BorrowedItems");
            DropTable("dbo.Books");
        }
    }
}
