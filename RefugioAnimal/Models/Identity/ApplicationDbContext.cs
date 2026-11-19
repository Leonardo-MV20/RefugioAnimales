using Microsoft.AspNet.Identity.EntityFramework;

namespace RefugioAnimal.Web.Models.Identity
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("RefugioIdentityConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}