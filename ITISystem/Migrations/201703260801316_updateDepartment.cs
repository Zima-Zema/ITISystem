namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDepartment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Instructors", "Department_Department_Id", "dbo.Departments");
            DropIndex("dbo.Instructors", new[] { "Department_Key" });
            DropIndex("dbo.Instructors", new[] { "Department_Department_Id" });
            DropColumn("dbo.Instructors", "Department_Key");
            RenameColumn(table: "dbo.Instructors", name: "Department_Department_Id", newName: "Department_Key");
            AlterColumn("dbo.Instructors", "Department_Key", c => c.Int(nullable: false));
            CreateIndex("dbo.Instructors", "Department_Key");
            AddForeignKey("dbo.Instructors", "Department_Key", "dbo.Departments", "Department_Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Instructors", "Department_Key", "dbo.Departments");
            DropIndex("dbo.Instructors", new[] { "Department_Key" });
            AlterColumn("dbo.Instructors", "Department_Key", c => c.Int());
            RenameColumn(table: "dbo.Instructors", name: "Department_Key", newName: "Department_Department_Id");
            AddColumn("dbo.Instructors", "Department_Key", c => c.Int(nullable: false));
            CreateIndex("dbo.Instructors", "Department_Department_Id");
            CreateIndex("dbo.Instructors", "Department_Key");
            AddForeignKey("dbo.Instructors", "Department_Department_Id", "dbo.Departments", "Department_Id");
        }
    }
}
