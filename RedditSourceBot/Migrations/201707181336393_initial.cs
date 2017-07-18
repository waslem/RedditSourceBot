namespace RedditSourceBot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Author = c.String(),
                        Content = c.String(),
                        Created = c.DateTime(nullable: false),
                        Post_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.Post_ID)
                .Index(t => t.Post_ID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Post_ID", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "Post_ID" });
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
        }
    }
}
