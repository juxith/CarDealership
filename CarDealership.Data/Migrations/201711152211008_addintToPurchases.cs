namespace CarDealership.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addintToPurchases : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "DateOfPurchase", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "DateOfPurchase");
        }
    }
}
