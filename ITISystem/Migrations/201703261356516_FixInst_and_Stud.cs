namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixInst_and_Stud : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "Premissions");
            DropColumn("dbo.Instructors", "Premissions");
        }
        
        public override void Down()
        {
        }
    }
}
