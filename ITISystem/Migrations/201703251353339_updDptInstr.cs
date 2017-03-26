namespace ITISystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updDptInstr : DbMigration
    {
        public override void Up()
        {
          //  DropColumn("dbo.Departments", "instr_key");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departments", "instr_key", c => c.Int());
        }
    }
}
