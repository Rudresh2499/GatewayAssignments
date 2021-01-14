using HMS.BAL.Interface;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace HotelManagementWebAPI.Controllers
{
    public class RoomController : ApiController
    {
        private readonly IRoomManager _roomManager;

        public RoomController()
        {
            ;
        }

        public RoomController(IRoomManager roomManager)
        {
            _roomManager = roomManager;
        }

        //GET: api/Room/1
        [HttpGet]
        [Route("api/Room/{id}")]
        [ResponseType(typeof(Room))]
        public IHttpActionResult Get(int id)
        {
            Room entity = _roomManager.GetRoomDetails(id);
            if(entity!=null)
            {
                return Ok(entity);
            }
            return NotFound();
        }

        //GET: api/Room
        [HttpGet]
        [Route("api/Room")]
        public List<Room> Get(string City = null, string PinCode = null, float Price = 0f, int Category = 0)
        {
            return _roomManager.GetAllRoomDetails(City, PinCode, Price, Category);
        }

        //GET: api/Room/1
        [HttpGet]
        [Route("api/Room/{id}")]
        public bool Get(int id, string date)
        {
            return _roomManager.isRoomAvailable(id, date);
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        public IHttpActionResult Post([FromBody]Room model)
        {
            string response = _roomManager.CreateRoom(model);
            if(response.Equals("Data Added"))
            {
                return Ok("Room Created");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/Room")]
        public IHttpActionResult Put([FromBody]Room model)
        {
            string response = _roomManager.UpdateRoom(model);
            if(response.Equals("Successfully Updated"))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("api/Room/{id}")]
        public IHttpActionResult Delete(int id)
        {
            string response = _roomManager.DeleteRoom(id);
            if(response.Equals("Record Deleted Successfully"))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
