namespace CarDealership.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactUs", "ContactName", c => c.String(nullable: false));
            AlterColumn("dbo.ContactUs", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.ContactUs", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.ContactUs", "Message", c => c.String(nullable: false));
            AlterColumn("dbo.Vehicles", "VinNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Models", "ModelName", c => c.String(nullable: false));
            AlterColumn("dbo.Makes", "MakeName", c => c.String(nullable: false));
            AlterColumn("dbo.Specials", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Specials", "SpecialDescription", c => c.String(nullable: false));
            AlterColumn("dbo.Purchases", "CustomerFirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Purchases", "CustomerLastName", c => c.String(nullable: false));
            AlterColumn("dbo.Purchases", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Purchases", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Purchases", "Street1", c => c.String(nullable: false));
            AlterColumn("dbo.Purchases", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Purchases", "StateAbrv", c => c.String(nullable: false));
            AlterColumn("dbo.Purchases", "Zipcode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Purchases", "Zipcode", c => c.String());
            AlterColumn("dbo.Purchases", "StateAbrv", c => c.String());
            AlterColumn("dbo.Purchases", "City", c => c.String());
            AlterColumn("dbo.Purchases", "Street1", c => c.String());
            AlterColumn("dbo.Purchases", "Phone", c => c.String());
            AlterColumn("dbo.Purchases", "Email", c => c.String());
            AlterColumn("dbo.Purchases", "CustomerLastName", c => c.String());
            AlterColumn("dbo.Purchases", "CustomerFirstName", c => c.String());
            AlterColumn("dbo.Specials", "SpecialDescription", c => c.String());
            AlterColumn("dbo.Specials", "Title", c => c.String());
            AlterColumn("dbo.Makes", "MakeName", c => c.String());
            AlterColumn("dbo.Models", "ModelName", c => c.String());
            AlterColumn("dbo.Vehicles", "VinNumber", c => c.String());
            AlterColumn("dbo.ContactUs", "Message", c => c.String());
            AlterColumn("dbo.ContactUs", "Phone", c => c.String());
            AlterColumn("dbo.ContactUs", "Email", c => c.String());
            AlterColumn("dbo.ContactUs", "ContactName", c => c.String());
        }
    }
}
