using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
using Unity.WebApi;
using HMS.BAL.Interface;
using HMS.BAL;
using HMS.BAL.Helper;
using System.Web.Http;

namespace HotelManagementWebAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IAdminDetailManager, AdminDetailManager>();
            container.RegisterType<IHotelManager, HotelManager>();
            container.RegisterType<IRoomManager, RoomManager>();
            container.RegisterType<IBookingManager, BookingManager>();
            container.AddNewExtension<UnityRepositoryHelper>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}