namespace BookKeeping.Domain.Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WhenApplied = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Expenses");
        }
    }
}
