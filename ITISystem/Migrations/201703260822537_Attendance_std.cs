namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Attendance_std : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        Student_key = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Arrive_time = c.DateTime(nullable: false),
                        Leave_time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Student_key)
                .ForeignKey("dbo.Students", t => t.Student_key)
                .Index(t => t.Student_key);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "Student_key", "dbo.Students");
            DropIndex("dbo.Attendances", new[] { "Student_key" });
            DropTable("dbo.Attendances");
        }
    }
}
