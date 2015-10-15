using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EducationSystem.Startup))]
namespace EducationSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
