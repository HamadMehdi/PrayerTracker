namespace PrayerTracker1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_Prayer1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prayers", "IsMissed", c => c.Boolean(nullable: false));
            DropColumn("dbo.Prayers", "IsPending");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prayers", "IsPending", c => c.Boolean(nullable: false));
            DropColumn("dbo.Prayers", "IsMissed");
        }
    }
}
