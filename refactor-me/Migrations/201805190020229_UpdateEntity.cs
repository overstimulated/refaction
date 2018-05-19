namespace refactor_me.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AlterColumn("dbo.ProductOption", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));

        }

        public override void Down()
        {
            AlterColumn("dbo.ProductOption", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Product", "Id", c => c.Guid(nullable: false));
        }
    }
}
