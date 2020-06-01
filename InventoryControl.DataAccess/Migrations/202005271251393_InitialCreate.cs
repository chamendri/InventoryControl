namespace InventoryControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventoryPart",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        AvailabeNoOfUnits = c.Int(nullable: false),
                        ReorderLevel = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InventoryPart");
        }
    }
}
