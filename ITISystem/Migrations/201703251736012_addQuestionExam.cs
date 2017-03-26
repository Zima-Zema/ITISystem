namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addQuestionExam : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionExams",
                c => new
                    {
                        Question_Question_id = c.Int(nullable: false),
                        Exam_Exam_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Question_Question_id, t.Exam_Exam_id })
                .ForeignKey("dbo.Questions", t => t.Question_Question_id, cascadeDelete: false)
                .ForeignKey("dbo.Exams", t => t.Exam_Exam_id, cascadeDelete: false)
                .Index(t => t.Question_Question_id)
                .Index(t => t.Exam_Exam_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionExams", "Exam_Exam_id", "dbo.Exams");
            DropForeignKey("dbo.QuestionExams", "Question_Question_id", "dbo.Questions");
            DropIndex("dbo.QuestionExams", new[] { "Exam_Exam_id" });
            DropIndex("dbo.QuestionExams", new[] { "Question_Question_id" });
            DropTable("dbo.QuestionExams");
        }
    }
}
