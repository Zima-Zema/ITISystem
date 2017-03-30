namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class last : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Instructors", "Department_Key", "dbo.Departments");
            DropIndex("dbo.Instructors", new[] { "Department_Key" });
            AlterColumn("dbo.Instructors", "Department_Key", c => c.Int());
            CreateIndex("dbo.Instructors", "Department_Key");
            AddForeignKey("dbo.Instructors", "Department_Key", "dbo.Departments", "Department_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Instructors", "Department_Key", "dbo.Departments");
            DropIndex("dbo.Instructors", new[] { "Department_Key" });
            AlterColumn("dbo.Instructors", "Department_Key", c => c.Int(nullable: false));
            CreateIndex("dbo.Instructors", "Department_Key");
            AddForeignKey("dbo.Instructors", "Department_Key", "dbo.Departments", "Department_Id", cascadeDelete: true);
        }
    }
}
