using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DentalClinicManagementSystem.Startup))]
namespace DentalClinicManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
