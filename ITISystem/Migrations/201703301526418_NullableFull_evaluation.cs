namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableFull_evaluation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Dept_Crs_Instr", "Full_evaluation", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Dept_Crs_Instr", "Full_evaluation", c => c.Int(nullable: false));
        }
    }
}
