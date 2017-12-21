namespace CarDealership.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updating : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Models", "ModelName", c => c.String());
            AlterColumn("dbo.Makes", "MakeName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Makes", "MakeName", c => c.String(nullable: false));
            AlterColumn("dbo.Models", "ModelName", c => c.String(nullable: false));
        }
    }
}
