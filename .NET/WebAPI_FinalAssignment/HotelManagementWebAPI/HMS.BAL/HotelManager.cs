using HMS.BAL.Interface;
using HMS.DAL.Repositories;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BAL
{
    public class HotelManager : IHotelManager
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelManager(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public Hotel GetHotelDetails(int id)
        {
            return _hotelRepository.GetHotelDetails(id);
        }
        
        public List<Hotel> GetAllHotelDetails()
        {
            return _hotelRepository.GetAllHotelDetails();
        }
        
        public string CreateHotel(Hotel model)
        {
            return _hotelRepository.CreateHotel(model);
        }
        
        public string UpdateHotel(Hotel model)
        {
            return _hotelRepository.UpdateHotel(model);
        }
        
        public string DeleteHotel(int id)
        {
            return _hotelRepository.DeleteHotel(id);
        }
    }
}
