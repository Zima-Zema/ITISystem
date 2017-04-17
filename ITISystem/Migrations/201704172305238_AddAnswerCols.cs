namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnswerCols : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Answer_A", c => c.String());
            AddColumn("dbo.Questions", "Answer_B", c => c.String());
            AddColumn("dbo.Questions", "Answer_C", c => c.String());
            AddColumn("dbo.Questions", "Answer_D", c => c.String());
            AddColumn("dbo.Questions", "Answer_Model", c => c.String());

        }

        public override void Down()
        {
            
        }
    }
}
