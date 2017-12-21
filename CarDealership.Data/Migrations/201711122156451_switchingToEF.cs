namespace CarDealership.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class switchingToEF : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "IsFeatured", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Vehicles", "IsNewType", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "IsNewType", c => c.Boolean(nullable: false));
            DropColumn("dbo.Vehicles", "IsFeatured");
        }
    }
}
