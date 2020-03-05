namespace Tech_Events_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateEventTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Organiser = c.String(nullable: false, maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                        Date = c.DateTime(nullable: false),
                        ImageTitle = c.String(nullable: false, maxLength: 30),
                        ImagePath = c.String(nullable: false, maxLength: 50),
                        Website = c.String(nullable: false),
                        Postcode = c.String(nullable: false),
                        Latitude = c.Double(),
                        Longitude = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Events");
        }
    }
}
