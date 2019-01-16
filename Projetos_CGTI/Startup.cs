using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projetos_CGTI.Startup))]
namespace Projetos_CGTI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
