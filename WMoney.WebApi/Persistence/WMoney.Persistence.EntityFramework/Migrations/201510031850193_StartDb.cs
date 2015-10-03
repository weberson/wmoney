namespace WMoney.Persistence.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TbAccount",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.TbUser", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TbTransaction",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        TransactionTypeId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Date = c.DateTime(nullable: false, precision: 0),
                        CategoryId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(maxLength: 200, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.TbAccount", t => t.AccountId)
                .ForeignKey("dbo.TbCategory", t => t.CategoryId)
                .ForeignKey("dbo.TbTransactionType", t => t.TransactionTypeId)
                .Index(t => t.TransactionTypeId)
                .Index(t => t.CategoryId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.TbCategory",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.TbUser", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TbUser",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 50, storeType: "nvarchar"),
                        Password = c.String(maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.TbTransactionType",
                c => new
                    {
                        TransactionTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.TransactionTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TbAccount", "UserId", "dbo.TbUser");
            DropForeignKey("dbo.TbTransaction", "TransactionTypeId", "dbo.TbTransactionType");
            DropForeignKey("dbo.TbTransaction", "CategoryId", "dbo.TbCategory");
            DropForeignKey("dbo.TbCategory", "UserId", "dbo.TbUser");
            DropForeignKey("dbo.TbTransaction", "AccountId", "dbo.TbAccount");
            DropIndex("dbo.TbCategory", new[] { "UserId" });
            DropIndex("dbo.TbTransaction", new[] { "AccountId" });
            DropIndex("dbo.TbTransaction", new[] { "CategoryId" });
            DropIndex("dbo.TbTransaction", new[] { "TransactionTypeId" });
            DropIndex("dbo.TbAccount", new[] { "UserId" });
            DropTable("dbo.TbTransactionType");
            DropTable("dbo.TbUser");
            DropTable("dbo.TbCategory");
            DropTable("dbo.TbTransaction");
            DropTable("dbo.TbAccount");
        }
    }
}
