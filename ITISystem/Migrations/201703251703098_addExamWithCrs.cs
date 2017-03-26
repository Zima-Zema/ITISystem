namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addExamWithCrs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Exam_id = c.Int(nullable: false, identity: true),
                        Duration = c.Int(nullable: false),
                        Subject = c.String(nullable: false),
                        from = c.DateTime(nullable: false),
                        to = c.DateTime(nullable: false),
                        Course_key = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Exam_id)
                .ForeignKey("dbo.Courses", t => t.Course_key, cascadeDelete: true)
                .Index(t => t.Course_key);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exams", "Course_key", "dbo.Courses");
            DropIndex("dbo.Exams", new[] { "Course_key" });
            DropTable("dbo.Exams");
        }
    }
}
