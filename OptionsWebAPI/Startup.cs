using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Cors;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(OptionsWebAPI.Startup))]

namespace OptionsWebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
