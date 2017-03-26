namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_Std_Exam : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Std_Exam",
                c => new
                    {
                        Student_key = c.Int(nullable: false),
                        Exam_key = c.Int(nullable: false),
                        Exam_grade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_key, t.Exam_key })
                .ForeignKey("dbo.Exams", t => t.Exam_key, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_key, cascadeDelete: true)
                .Index(t => t.Student_key)
                .Index(t => t.Exam_key);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Std_Exam", "Student_key", "dbo.Students");
            DropForeignKey("dbo.Std_Exam", "Exam_key", "dbo.Exams");
            DropIndex("dbo.Std_Exam", new[] { "Exam_key" });
            DropIndex("dbo.Std_Exam", new[] { "Student_key" });
            DropTable("dbo.Std_Exam");
        }
    }
}
