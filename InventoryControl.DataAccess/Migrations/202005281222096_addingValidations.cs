namespace InventoryControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingValidations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InventoryPart", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InventoryPart", "Name", c => c.String(maxLength: 50));
        }
    }
}
