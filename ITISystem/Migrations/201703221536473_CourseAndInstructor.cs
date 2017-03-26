namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseAndInstructor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                {
                    Course_Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Lec_Duration = c.Int(nullable: false),
                    Lab_Duration = c.Int(nullable: false),
                    Status = c.Int(nullable: false),
                    Duration = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Course_Id)
                .Index(t => t.Name, unique: true);

            CreateTable(
                "dbo.Instructors",
                c => new
                {
                    Instructor_Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    BirthDate = c.DateTime(nullable: false),
                    Degree = c.String(nullable: false),
                    Graduation_Year = c.Int(nullable: false),
                    Work_Status = c.Int(nullable: false),
                    Department_Key = c.Int(),
                })
                .PrimaryKey(t => t.Instructor_Id)
                .ForeignKey("dbo.Departments", t => t.Department_Key)
                .Index(t => t.Department_Key);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Instructors", "Department_Key", "dbo.Departments");
            DropIndex("dbo.Instructors", new[] { "Department_Key" });
            DropIndex("dbo.Courses", new[] { "Name" });
            DropTable("dbo.Instructors");
            DropTable("dbo.Courses");
        }
    }
}
