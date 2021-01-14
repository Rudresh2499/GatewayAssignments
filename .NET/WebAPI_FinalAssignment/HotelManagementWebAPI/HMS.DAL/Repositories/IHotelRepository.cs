using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repositories
{
    public interface IHotelRepository
    {
        Hotel GetHotelDetails(int id);
        List<Hotel> GetAllHotelDetails();
        string CreateHotel(Hotel model);
        string UpdateHotel(Hotel model);
        string DeleteHotel(int id);
    }
}
