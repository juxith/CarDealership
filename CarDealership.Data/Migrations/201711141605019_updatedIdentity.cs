namespace CarDealership.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedIdentity : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropColumn("dbo.AspNetRoles", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
