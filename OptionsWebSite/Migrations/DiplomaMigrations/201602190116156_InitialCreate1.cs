namespace OptionsWebSite.Migrations.DiplomaMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Choices", "StudentId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Choices", "StudentId", c => c.String(maxLength: 9));
        }
    }
}
