namespace SchoolLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ISBN = c.Int(nullable: false),
                        Title = c.String(unicode: false),
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
                "dbo.Users",
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
            DropTable("dbo.Users");
            DropTable("dbo.Books");
        }
    }
}
