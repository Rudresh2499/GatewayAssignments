using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.BAL.Interface;
using HMS.DAL.Repositories;
using HMS.Models;

namespace HMS.BAL
{
    public class RoomManager:IRoomManager
    {
        private readonly IRoomRepository _roomRepository;

        public RoomManager(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public Room GetRoomDetails(int id)
        {
            return _roomRepository.GetRoomDetails(id);
        }

        public List<Room> GetAllRoomDetails(string City = null, string PinCode = null, float Price = 0f, int Category = 0)
        {
            return _roomRepository.GetAllRoomDetails(City, PinCode, Price, Category);
        }

        public string CreateRoom(Room model)
        {
            return _roomRepository.CreateRoom(model);
        }

        public string UpdateRoom(Room model)
        {
            return _roomRepository.UpdateRoom(model);
        }

        public string DeleteRoom(int id)
        {
            return _roomRepository.DeleteRoom(id);
        }

        public bool isRoomAvailable(int id, string date)
        {
            string[] dateSplit = date.Split('-');
            DateTime dateTime = new DateTime(Int32.Parse(dateSplit[0]), Int32.Parse(dateSplit[1]), Int32.Parse(dateSplit[2]));
            return _roomRepository.isRoomAvailable(id, dateTime);
        }
    }
}
