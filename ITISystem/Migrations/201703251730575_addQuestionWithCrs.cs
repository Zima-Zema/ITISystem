namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addQuestionWithCrs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Question_id = c.Int(nullable: false, identity: true),
                        Header = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        Grade = c.Double(nullable: false),
                        Course_key = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Question_id)
                .ForeignKey("dbo.Courses", t => t.Course_key, cascadeDelete: true)
                .Index(t => t.Course_key);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "Course_key", "dbo.Courses");
            DropIndex("dbo.Questions", new[] { "Course_key" });
            DropTable("dbo.Questions");
        }
    }
}
