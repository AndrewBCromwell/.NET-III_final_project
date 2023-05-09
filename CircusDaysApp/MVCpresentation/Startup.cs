using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCpresentation.Startup))]
namespace MVCpresentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
