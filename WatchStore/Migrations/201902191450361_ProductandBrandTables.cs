namespace WatchStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductandBrandTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        BrandId = c.Int(nullable: false),
                        ProductName = c.String(nullable: false),
                        ProductDescription = c.String(nullable: false),
                        ProductImage = c.Binary(nullable: false),
                        ProductPrice = c.Double(nullable: false),
                        ProductDate = c.DateTime(nullable: false),
                        ProductSale = c.Double(nullable: false),
                        ProductQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .Index(t => t.BrandId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropTable("dbo.Products");
            DropTable("dbo.Brands");
        }
    }
}
