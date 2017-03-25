namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Std_Exam_Quest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Std_Exam_Quest",
                c => new
                    {
                        Student_key = c.Int(nullable: false),
                        Exam_key = c.Int(nullable: false),
                        Question_key = c.Int(nullable: false),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => new { t.Student_key, t.Exam_key, t.Question_key })
                .ForeignKey("dbo.Exams", t => t.Exam_key, cascadeDelete: false)
                .ForeignKey("dbo.Questions", t => t.Question_key, cascadeDelete: false)
                .ForeignKey("dbo.Students", t => t.Student_key, cascadeDelete: false)
                .Index(t => t.Student_key)
                .Index(t => t.Exam_key)
                .Index(t => t.Question_key);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Std_Exam_Quest", "Student_key", "dbo.Students");
            DropForeignKey("dbo.Std_Exam_Quest", "Question_key", "dbo.Questions");
            DropForeignKey("dbo.Std_Exam_Quest", "Exam_key", "dbo.Exams");
            DropIndex("dbo.Std_Exam_Quest", new[] { "Question_key" });
            DropIndex("dbo.Std_Exam_Quest", new[] { "Exam_key" });
            DropIndex("dbo.Std_Exam_Quest", new[] { "Student_key" });
            DropTable("dbo.Std_Exam_Quest");
        }
    }
}
