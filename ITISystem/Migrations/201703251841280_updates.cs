namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Std_Exam_Ques", "Exam_key", "dbo.Exams");
            DropForeignKey("dbo.Std_Exam_Ques", "Question_key", "dbo.Questions");
            DropForeignKey("dbo.Std_Exam_Ques", "Student_key", "dbo.Students");
            DropIndex("dbo.Std_Exam_Ques", new[] { "Student_key" });
            DropIndex("dbo.Std_Exam_Ques", new[] { "Exam_key" });
            DropIndex("dbo.Std_Exam_Ques", new[] { "Question_key" });
            DropTable("dbo.Std_Exam_Ques");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Std_Exam_Ques",
                c => new
                    {
                        Student_key = c.Int(nullable: false),
                        Exam_key = c.Int(nullable: false),
                        Question_key = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_key, t.Exam_key, t.Question_key });
            
            CreateIndex("dbo.Std_Exam_Ques", "Question_key");
            CreateIndex("dbo.Std_Exam_Ques", "Exam_key");
            CreateIndex("dbo.Std_Exam_Ques", "Student_key");
            AddForeignKey("dbo.Std_Exam_Ques", "Student_key", "dbo.Students", "Student_Id", cascadeDelete: true);
            AddForeignKey("dbo.Std_Exam_Ques", "Question_key", "dbo.Questions", "Question_id", cascadeDelete: true);
            AddForeignKey("dbo.Std_Exam_Ques", "Exam_key", "dbo.Exams", "Exam_id", cascadeDelete: true);
        }
    }
}
