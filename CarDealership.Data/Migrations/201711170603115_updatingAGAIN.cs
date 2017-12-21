namespace CarDealership.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingAGAIN : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "VinNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "VinNumber", c => c.String(nullable: false));
        }
    }
}
