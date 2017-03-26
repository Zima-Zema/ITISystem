namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeptCrsInstr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dept_Crs_Instr",
                c => new
                    {
                        Department_key = c.Int(nullable: false),
                        Course_key = c.Int(nullable: false),
                        Instructor_key = c.Int(nullable: false),
                        Full_evaluation = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Department_key, t.Course_key, t.Instructor_key })
                .ForeignKey("dbo.Courses", t => t.Course_key, cascadeDelete: false)
                .ForeignKey("dbo.Departments", t => t.Department_key, cascadeDelete: false)
                .ForeignKey("dbo.Instructors", t => t.Instructor_key, cascadeDelete: false)
                .Index(t => t.Department_key)
                .Index(t => t.Course_key)
                .Index(t => t.Instructor_key);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dept_Crs_Instr", "Instructor_key", "dbo.Instructors");
            DropForeignKey("dbo.Dept_Crs_Instr", "Department_key", "dbo.Departments");
            DropForeignKey("dbo.Dept_Crs_Instr", "Course_key", "dbo.Courses");
            DropIndex("dbo.Dept_Crs_Instr", new[] { "Instructor_key" });
            DropIndex("dbo.Dept_Crs_Instr", new[] { "Course_key" });
            DropIndex("dbo.Dept_Crs_Instr", new[] { "Department_key" });
            DropTable("dbo.Dept_Crs_Instr");
        }
    }
}
