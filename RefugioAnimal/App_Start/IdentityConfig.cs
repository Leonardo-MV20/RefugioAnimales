using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using RefugioAnimal.Web.Models.Identity;

namespace RefugioAnimal
{
    public class ApplicationUserManager
        : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(
            IUserStore<ApplicationUser> store)
            : base(store)
        {
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireNonLetterOrDigit = false
            };
        }

        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            return new ApplicationUserManager(
                new UserStore<ApplicationUser>(
                    context.Get<ApplicationDbContext>()));
        }
    }
}