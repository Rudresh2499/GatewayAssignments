using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BAL.Interface
{
    public interface IRoomManager
    {
        Room GetRoomDetails(int id);
        List<Room> GetAllRoomDetails(string City = "", string PinCode = "", float Price = 0f, int Category = 0);
        string CreateRoom(Room model);
        string UpdateRoom(Room model);
        string DeleteRoom(int id);
        bool isRoomAvailable(int id, string date);
    }
}
