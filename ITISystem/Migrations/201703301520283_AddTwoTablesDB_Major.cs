namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTwoTablesDB_Major : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InstructorCourses",
                c => new
                    {
                        Instructor_Instructor_Id = c.Int(nullable: false),
                        Course_Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Instructor_Instructor_Id, t.Course_Course_Id })
                .ForeignKey("dbo.Instructors", t => t.Instructor_Instructor_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Course_Id, cascadeDelete: true)
                .Index(t => t.Instructor_Instructor_Id)
                .Index(t => t.Course_Course_Id);
            
            CreateTable(
                "dbo.CourseDepartments",
                c => new
                    {
                        Course_Course_Id = c.Int(nullable: false),
                        Department_Department_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Course_Course_Id, t.Department_Department_Id })
                .ForeignKey("dbo.Courses", t => t.Course_Course_Id, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.Department_Department_Id, cascadeDelete: true)
                .Index(t => t.Course_Course_Id)
                .Index(t => t.Department_Department_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseDepartments", "Department_Department_Id", "dbo.Departments");
            DropForeignKey("dbo.CourseDepartments", "Course_Course_Id", "dbo.Courses");
            DropForeignKey("dbo.InstructorCourses", "Course_Course_Id", "dbo.Courses");
            DropForeignKey("dbo.InstructorCourses", "Instructor_Instructor_Id", "dbo.Instructors");
            DropIndex("dbo.CourseDepartments", new[] { "Department_Department_Id" });
            DropIndex("dbo.CourseDepartments", new[] { "Course_Course_Id" });
            DropIndex("dbo.InstructorCourses", new[] { "Course_Course_Id" });
            DropIndex("dbo.InstructorCourses", new[] { "Instructor_Instructor_Id" });
            DropTable("dbo.CourseDepartments");
            DropTable("dbo.InstructorCourses");
        }
    }
}
