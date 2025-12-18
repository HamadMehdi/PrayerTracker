using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PrayerTracker1.Startup))]
namespace PrayerTracker1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
