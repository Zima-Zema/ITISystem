namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDepartment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Instructors", "Department_Key", "dbo.Departments");
            AddColumn("dbo.Instructors", "Department_Department_Id", c => c.Int());
            CreateIndex("dbo.Instructors", "Department_Department_Id");
            AddForeignKey("dbo.Instructors", "Department_Department_Id", "dbo.Departments", "Department_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Instructors", "Department_Department_Id", "dbo.Departments");
            DropIndex("dbo.Instructors", new[] { "Department_Department_Id" });
            DropColumn("dbo.Instructors", "Department_Department_Id");
            AddForeignKey("dbo.Instructors", "Department_Key", "dbo.Departments", "Department_Id", cascadeDelete: false);
        }
    }
}
