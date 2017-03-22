namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentandDepartment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                {
                    Department_Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    Capacity = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Department_Id);
                
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Student_Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Attend_Balance = c.Int(nullable: false),
                        Telephone = c.Int(nullable: false),
                        Address_Street = c.String(),
                        Address_City = c.String(),
                        Address_Country = c.String(),
                        Department_Key = c.Int(),
                    })
                .PrimaryKey(t => t.Student_Id)
                .ForeignKey("dbo.Departments", t => t.Department_Key)
                .Index(t => t.Department_Key);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "Department_Key", "dbo.Departments");
            DropIndex("dbo.Students", new[] { "Department_Key" });
            DropIndex("dbo.Students", new[] { "Telephone" });
            DropIndex("dbo.Students", new[] { "UserName" });
            DropIndex("dbo.Students", new[] { "Email" });
            DropIndex("dbo.Departments", new[] { "Name" });
            DropTable("dbo.Students");
            DropTable("dbo.Departments");
        }
    }
}
