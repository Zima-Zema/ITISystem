namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_phone2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Telephone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Telephone", c => c.Int(nullable: false));
        }
    }
}
