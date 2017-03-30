namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FloatFull_evaluation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Dept_Crs_Instr", "Full_evaluation", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Dept_Crs_Instr", "Full_evaluation", c => c.Int());
        }
    }
}
