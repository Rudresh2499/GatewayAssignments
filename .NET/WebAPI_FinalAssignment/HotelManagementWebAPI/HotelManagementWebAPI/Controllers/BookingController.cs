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
    public class BookingController : ApiController
    {

        private readonly IBookingManager _bookingManager;

        public BookingController()
        {
            ;
        }

        public BookingController(IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
        }

        //GET: api/Booking

        [HttpGet]
        [Route("api/Booking")]
        public List<Booking> Get()
        {
            return _bookingManager.GetAllBookings();
        }

        //POST: api/Booking
        [HttpPost]
        [Route("api/Booking")]
        public IHttpActionResult Post([FromBody]Booking model)
        {
            var response = _bookingManager.CreateBooking(model);

            if(response.Equals("Data Added"))
            {
                return Ok("Room Booked");
            }
            else if(response.Equals("Cannot book a room for past date"))
            {
                return BadRequest("Cannot book a room for past date");
            }
            else if(response.StartsWith("Room is already booked for"))
            {
                return BadRequest("Room is already booked " + model.BookingDate.ToString());
            }
            else
            {
                return BadRequest();
            }
        }

        //PUT: api/Booking
        [HttpPut]
        [Route("api/Booking")]
        public IHttpActionResult Put([FromBody]Booking model)
        {
            string response = _bookingManager.UpdateBooking(model);

            if (response.Equals("Successfully Updated"))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/Booking/{id}")]
        public IHttpActionResult Put(int id,[FromBody] Booking model)
        {
            model.Id = id;
            string response = _bookingManager.UpdateBooking(model);
            if (response.Equals("Successfully Updated"))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return BadRequest();
            }
        }

        //DELETE: api/Booking/1
        [HttpDelete]
        [Route("api/Booking/{id}")]
        public IHttpActionResult Delete(int id)
        {
            string response = _bookingManager.DeleteBooking(id);

            if(response.Equals("Record Deleted successfully"))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
