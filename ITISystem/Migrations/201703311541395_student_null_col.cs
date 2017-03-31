namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class student_null_col : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Std_Crs_Instr", "Instr_evaluation", c => c.Int());
            AlterColumn("dbo.Std_Crs_Instr", "Crs_evaluation", c => c.Int());
            AlterColumn("dbo.Std_Crs_Instr", "Labs_Grade", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Std_Crs_Instr", "Labs_Grade", c => c.Int(nullable: false));
            AlterColumn("dbo.Std_Crs_Instr", "Crs_evaluation", c => c.Int(nullable: false));
            AlterColumn("dbo.Std_Crs_Instr", "Instr_evaluation", c => c.Int(nullable: false));
        }
    }
}
