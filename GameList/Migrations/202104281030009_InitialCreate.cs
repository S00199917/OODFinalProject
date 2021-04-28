namespace GameList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameID = c.Int(nullable: false, identity: true),
                        GameName = c.String(),
                        GameDescription = c.String(),
                        GamePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GameRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageURL = c.String(),
                    })
                .PrimaryKey(t => t.GameID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Games");
        }
    }
}
