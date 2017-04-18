namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnswer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "Answer_A", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "Answer_B", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "Answer_C", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "Answer_D", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "Answer_Model", c => c.String(nullable: false));

        }

        public override void Down()
        {
            
        }
    }
}
