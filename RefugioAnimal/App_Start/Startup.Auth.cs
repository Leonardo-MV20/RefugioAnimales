using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using RefugioAnimal.Web.Models.Identity;

namespace RefugioAnimal
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);

            app.CreatePerOwinContext<ApplicationUserManager>(
                ApplicationUserManager.Create);

            app.UseCookieAuthentication(
                new CookieAuthenticationOptions
                {
                    AuthenticationType =
                        DefaultAuthenticationTypes.ApplicationCookie,

                    LoginPath =
                        new PathString("/Account/Login"),

                    ExpireTimeSpan =
                        System.TimeSpan.FromMinutes(30),

                    SlidingExpiration = true
                });
        }
    }
}