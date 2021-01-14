using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Extension;
using HMS.DAL.Repositories;

namespace HMS.BAL.Helper
{
    public class UnityRepositoryHelper : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IAdminDetailRepository, AdminDetailRepository>();
            Container.RegisterType<IHotelRepository, HotelRepository>();
            Container.RegisterType<IRoomRepository, RoomRepository>();
            Container.RegisterType<IBookingRepository, BookingRepository>();
        }
    }
}
