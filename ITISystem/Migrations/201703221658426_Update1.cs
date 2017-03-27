namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Courses", new[] { "Name" });
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.Courses", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Courses", new[] { "Name" });
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.Courses", "Name", unique: true);
        }
    }
}
