namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Std_Crs_Instr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Std_Crs_Instr",
                c => new
                    {
                        Student_key = c.Int(nullable: false),
                        Course_key = c.Int(nullable: false),
                        Instructor_key = c.Int(nullable: false),
                        Instr_evaluation = c.Int(nullable: false),
                        Crs_evaluation = c.Int(nullable: false),
                        Labs_Grade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_key, t.Course_key, t.Instructor_key })
                .ForeignKey("dbo.Courses", t => t.Course_key, cascadeDelete: true)
                .ForeignKey("dbo.Instructors", t => t.Instructor_key, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_key, cascadeDelete: true)
                .Index(t => t.Student_key)
                .Index(t => t.Course_key)
                .Index(t => t.Instructor_key);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Std_Crs_Instr", "Student_key", "dbo.Students");
            DropForeignKey("dbo.Std_Crs_Instr", "Instructor_key", "dbo.Instructors");
            DropForeignKey("dbo.Std_Crs_Instr", "Course_key", "dbo.Courses");
            DropIndex("dbo.Std_Crs_Instr", new[] { "Instructor_key" });
            DropIndex("dbo.Std_Crs_Instr", new[] { "Course_key" });
            DropIndex("dbo.Std_Crs_Instr", new[] { "Student_key" });
            DropTable("dbo.Std_Crs_Instr");
        }
    }
}
