namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNumberOfQuestionToExam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exams", "NumberOfQuestion", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exams", "NumberOfQuestion");
        }
    }
}
