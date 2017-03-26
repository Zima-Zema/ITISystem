namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDepartment4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "manger_key", c => c.Int());
            CreateIndex("dbo.Departments", "manger_key");
            AddForeignKey("dbo.Departments", "manger_key", "dbo.Instructors", "Instructor_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "manger_key", "dbo.Instructors");
            DropIndex("dbo.Departments", new[] { "manger_key" });
            DropColumn("dbo.Departments", "manger_key");
        }
    }
}
