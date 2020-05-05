using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FileControl.Startup))]
namespace FileControl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
