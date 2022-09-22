using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NiceNeighbourPharmacy.Startup))]
namespace NiceNeighbourPharmacy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
