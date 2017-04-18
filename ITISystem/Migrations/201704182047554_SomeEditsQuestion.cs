namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeEditsQuestion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "Answer_C", c => c.String());
            AlterColumn("dbo.Questions", "Answer_D", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questions", "Answer_D", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "Answer_C", c => c.String(nullable: false));
        }
    }
}
