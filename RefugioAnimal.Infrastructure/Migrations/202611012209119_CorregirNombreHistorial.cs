namespace RefugioAnimal.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorregirNombreHistorial : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.HistorialCuidadoes", newName: "HistorialCuidados");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.HistorialCuidados", newName: "HistorialCuidadoes");
        }
    }
}
