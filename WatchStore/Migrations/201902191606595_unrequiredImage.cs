namespace WatchStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unrequiredImage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ProductImage", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ProductImage", c => c.Binary(nullable: false));
        }
    }
}
