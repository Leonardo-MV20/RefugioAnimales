namespace RefugioAnimal.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarSolicitudAdopcion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SolicitudesAdopcion",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AdoptanteId = c.Guid(nullable: false),
                        MascotaId = c.Guid(nullable: false),
                        FechaSolicitud = c.DateTime(nullable: false),
                        Estado = c.String(nullable: false, maxLength: 30),
                        Motivo = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adoptantes", t => t.AdoptanteId)
                .ForeignKey("dbo.Mascotas", t => t.MascotaId)
                .Index(t => t.AdoptanteId)
                .Index(t => t.MascotaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SolicitudesAdopcion", "MascotaId", "dbo.Mascotas");
            DropForeignKey("dbo.SolicitudesAdopcion", "AdoptanteId", "dbo.Adoptantes");
            DropIndex("dbo.SolicitudesAdopcion", new[] { "MascotaId" });
            DropIndex("dbo.SolicitudesAdopcion", new[] { "AdoptanteId" });
            DropTable("dbo.SolicitudesAdopcion");
        }
    }
}
