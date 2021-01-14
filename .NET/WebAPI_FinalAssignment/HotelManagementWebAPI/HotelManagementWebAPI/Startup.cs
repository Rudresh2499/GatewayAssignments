using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

[assembly: OwinStartup(typeof(HotelManagementWebAPI.Startup))]

namespace HotelManagementWebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
        }
    }
}
