namespace PrayerTracker1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_Prayer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prayers", "IsPending", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prayers", "IsPending");
        }
    }
}
