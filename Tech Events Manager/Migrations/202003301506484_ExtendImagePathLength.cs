namespace Tech_Events_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendImagePathLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "ImagePath", c => c.String(nullable: false, maxLength: 400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "ImagePath", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
