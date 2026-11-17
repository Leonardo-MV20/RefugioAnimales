namespace RefugioAnimal.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarAdoptante : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adoptantes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NombreCompleto = c.String(nullable: false, maxLength: 150),
                        Identificacion = c.String(maxLength: 30),
                        Telefono = c.String(nullable: false, maxLength: 30),
                        Correo = c.String(nullable: false, maxLength: 150),
                        Direccion = c.String(maxLength: 250),
                        Observaciones = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Adoptantes");
        }
    }
}
