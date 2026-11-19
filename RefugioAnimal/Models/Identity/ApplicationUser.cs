using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RefugioAnimal.Web.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<ApplicationUser> manager)
        {
            return await manager.CreateIdentityAsync(
                this,
                DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}