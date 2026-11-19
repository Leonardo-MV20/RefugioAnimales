namespace RefugioAnimal.Migrations.Identity
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RefugioAnimal.Web.Models.Identity;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration
        : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Identity";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!roleManager.RoleExists("Administrador"))
            {
                roleManager.Create(
                    new IdentityRole("Administrador"));
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            const string correoAdministrador =
                "admin@refugio.com";

            var administrador =
                userManager.FindByEmail(correoAdministrador);

            if (administrador == null)
            {
                administrador = new ApplicationUser
                {
                    UserName = correoAdministrador,
                    Email = correoAdministrador,
                    EmailConfirmed = true
                };

                var resultado = userManager.Create(
                    administrador,
                    "Admin123");

                if (!resultado.Succeeded)
                {
                    throw new InvalidOperationException(
                        string.Join("; ", resultado.Errors));
                }
            }

            if (!userManager.IsInRole(
                administrador.Id,
                "Administrador"))
            {
                userManager.AddToRole(
                    administrador.Id,
                    "Administrador");
            }
        }
    }
}