namespace RefugioAnimal.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrearBaseInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistorialCuidadoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MascotaId = c.Guid(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Tipo = c.String(nullable: false, maxLength: 50),
                        Descripcion = c.String(nullable: false, maxLength: 500),
                        Responsable = c.String(maxLength: 150),
                        ProximaRevision = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mascotas", t => t.MascotaId)
                .Index(t => t.MascotaId);
            
            CreateTable(
                "dbo.Mascotas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Especie = c.String(nullable: false, maxLength: 50),
                        Raza = c.String(maxLength: 100),
                        EdadAproximada = c.Int(),
                        Sexo = c.String(nullable: false, maxLength: 20),
                        Estado = c.String(nullable: false, maxLength: 30),
                        FechaIngreso = c.DateTime(nullable: false),
                        Descripcion = c.String(maxLength: 500),
                        EstaActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistorialCuidadoes", "MascotaId", "dbo.Mascotas");
            DropIndex("dbo.HistorialCuidadoes", new[] { "MascotaId" });
            DropTable("dbo.Mascotas");
            DropTable("dbo.HistorialCuidadoes");
        }
    }
}
