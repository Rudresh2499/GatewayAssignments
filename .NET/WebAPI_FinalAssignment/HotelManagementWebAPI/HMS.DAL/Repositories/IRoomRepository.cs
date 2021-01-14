using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repositories
{
    public interface IRoomRepository
    {
        Room GetRoomDetails(int id);
        List<Room> GetAllRoomDetails(string City = null, string PinCode = null, float Price = 0f, int Category = 0);
        string CreateRoom(Room model);
        string UpdateRoom(Room model);
        string DeleteRoom(int id);
        bool isRoomAvailable(int id, DateTime date);
    }
}
