namespace SchoolLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BooksModelUpdate : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("Books");
            DropPrimaryKey("BorrowedItems");
            AddColumn("Books", "AccessionNumber", c => c.String(unicode: false));
            AlterColumn("Books", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("Books", "ISBN", c => c.String(unicode: false));
            AlterColumn("BorrowedItems", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("Books", "Id");
            AddPrimaryKey("BorrowedItems", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("BorrowedItems");
            DropPrimaryKey("Books");
            AlterColumn("BorrowedItems", "Id", c => c.Int(nullable: false));
            AlterColumn("Books", "ISBN", c => c.Int(nullable: false));
            AlterColumn("Books", "Id", c => c.Int(nullable: false));
            DropColumn("Books", "AccessionNumber");
            AddPrimaryKey("BorrowedItems", "Id");
            AddPrimaryKey("Books", "Id");
        }
    }
}
