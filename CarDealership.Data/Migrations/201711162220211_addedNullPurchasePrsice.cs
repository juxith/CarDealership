namespace CarDealership.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedNullPurchasePrsice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Purchases", "PurchasePrice", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Purchases", "PurchasePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
