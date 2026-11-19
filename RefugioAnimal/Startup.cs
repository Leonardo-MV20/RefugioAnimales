using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RefugioAnimal.Startup))]

namespace RefugioAnimal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}