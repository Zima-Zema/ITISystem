namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentNameIndex : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Departments", "Name", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Departments", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Departments", new[] { "Name" });
            AlterColumn("dbo.Departments", "Name", c => c.String(nullable: false));
        }
    }
}
