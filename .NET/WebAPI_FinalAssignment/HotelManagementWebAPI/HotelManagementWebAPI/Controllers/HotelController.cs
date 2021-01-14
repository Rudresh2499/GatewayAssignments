using HMS.BAL.Interface;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelManagementWebAPI.Controllers
{
    public class HotelController : ApiController
    {
        private readonly IHotelManager _hotelManager;

        public HotelController()
        {
            ;
        }

        public HotelController(IHotelManager hotelManager)
        {
            _hotelManager = hotelManager;
        }

        //GET: api/Hotel/1
        public Hotel Get(int id)
        {
            return _hotelManager.GetHotelDetails(id);
        }

        //GET: api/Hotel
        public List<Hotel> Get()
        {
            return _hotelManager.GetAllHotelDetails();
        }

        //POST: api/Hotel
        public string Post([FromBody]Hotel model)
        {
            return _hotelManager.CreateHotel(model);
        }

        //PUT: api/Hotel
        public string Put([FromBody]Hotel model)
        {
            return _hotelManager.UpdateHotel(model);
        }

        //DELETE: api/Hotel/1
        public string Delete(int id)
        {
            return _hotelManager.DeleteHotel(id);
        }
    }
}
