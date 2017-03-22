namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentandDepartment2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Departments", new[] { "Name" });
            DropIndex("dbo.Students", new[] { "Email" });
            DropIndex("dbo.Students", new[] { "Telephone" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Students", "Telephone", unique: true);
            CreateIndex("dbo.Students", "Email", unique: true);
            CreateIndex("dbo.Departments", "Name", unique: true);
        }
    }
}
