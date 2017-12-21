namespace CarDealership.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Models", new[] { "User_Id" });
            DropIndex("dbo.Makes", new[] { "User_Id" });
            DropIndex("dbo.Purchases", new[] { "User_Id" });
            DropColumn("dbo.Models", "UserId");
            DropColumn("dbo.Makes", "UserId");
            DropColumn("dbo.Purchases", "UserId");
            RenameColumn(table: "dbo.Models", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Makes", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Purchases", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Models", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Makes", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Purchases", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Models", "UserId");
            CreateIndex("dbo.Makes", "UserId");
            CreateIndex("dbo.Purchases", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Purchases", new[] { "UserId" });
            DropIndex("dbo.Makes", new[] { "UserId" });
            DropIndex("dbo.Models", new[] { "UserId" });
            AlterColumn("dbo.Purchases", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Makes", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Models", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Purchases", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Makes", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Models", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Purchases", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Makes", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Models", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Purchases", "User_Id");
            CreateIndex("dbo.Makes", "User_Id");
            CreateIndex("dbo.Models", "User_Id");
        }
    }
}
