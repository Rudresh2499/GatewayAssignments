using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DatabaseEntities.HotelManagementEntities _dbContext;

        public BookingRepository()
        {
            _dbContext = new DatabaseEntities.HotelManagementEntities();
        }

        public List<Booking> GetAllBookings()
        {
            var entities = _dbContext.Bookings.ToList();
            List<Booking> listOfBookings = new List<Booking>();

            if(entities != null)
            {
                foreach(var item in entities)
                {
                    Booking tempObject = new Booking();

                    tempObject.Id = item.Id;
                    tempObject.BookingDate = item.BookingDate;
                    tempObject.RoomId = item.RoomId;
                    tempObject.StatusOfBooking = item.StatusOfBooking;

                    listOfBookings.Add(tempObject);
                }
            }
            return (listOfBookings);
        }

        public string CreateBooking(Booking model)
        {
            try
            {
                if(model != null)
                {
                    if(DateTime.Now.Date > model.BookingDate.Date)
                    {
                        return ("Cannot book a room for past date");
                    }

                    DatabaseEntities.Booking entity = new DatabaseEntities.Booking();

                    entity.BookingDate = model.BookingDate;
                    entity.RoomId = model.RoomId;
                    entity.StatusOfBooking = model.StatusOfBooking;

                    var previousBooking = _dbContext.Bookings.Where(book => book.BookingDate == model.BookingDate && book.RoomId == model.RoomId && book.StatusOfBooking != 1).FirstOrDefault();
                    if(previousBooking == null)
                    {
                        _dbContext.Bookings.Add(entity);
                        _dbContext.SaveChanges();
                        return ("Data Added");
                    }
                    else
                    {
                        return ("Room is already booked for " + model.BookingDate + " date");
                    }
                }
                return ("Could Not Add the data");
            }
            catch(Exception e)
            {
                return ("Exception Occured " + e.Message);
            }
        }

        public string UpdateBooking(Booking model)
        {
            try
            {
                var entity = _dbContext.Bookings.Find(model.Id);
                
                if(entity != null && model != null)
                {
                    if(model.BookingDate != entity.BookingDate && model.BookingDate >= DateTime.Now.Date)
                    {
                        entity.BookingDate = model.BookingDate;
                    }
                    else
                    {
                        entity.StatusOfBooking = model.StatusOfBooking;
                    }

                    _dbContext.SaveChanges();
                    return ("Successfully Updated");
                }
                return ("Could not find the record");
            }
            catch(Exception e)
            {
                return ("Exception Occured :" + e.Message);
            }
        }

        public string DeleteBooking(int id)
        {
            try
            {
                var entity = _dbContext.Bookings.Find(id);

                if(entity != null)
                {
                    entity.StatusOfBooking = 3;

                    _dbContext.SaveChanges();
                    return ("Record Deleted successfully");
                }
                return ("Could not find the record");
            }
            catch(Exception e)
            {
                return ("Exception Occured :" + e.Message);
            }
        }
    }
}
