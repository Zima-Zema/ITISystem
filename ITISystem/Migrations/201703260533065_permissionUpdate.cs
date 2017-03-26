namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class permissionUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permisions",
                c => new
                    {
                        Student_key = c.Int(nullable: false),
                        Instructor_key = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_key, t.Instructor_key })
                .ForeignKey("dbo.Instructors", t => t.Instructor_key, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_key, cascadeDelete: true)
                .Index(t => t.Student_key)
                .Index(t => t.Instructor_key);
            
            AddColumn("dbo.Instructors", "Premissions", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "Premissions", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permisions", "Student_key", "dbo.Students");
            DropForeignKey("dbo.Permisions", "Instructor_key", "dbo.Instructors");
            DropIndex("dbo.Permisions", new[] { "Instructor_key" });
            DropIndex("dbo.Permisions", new[] { "Student_key" });
            DropColumn("dbo.Students", "Premissions");
            DropColumn("dbo.Instructors", "Premissions");
            DropTable("dbo.Permisions");
        }
    }
}
