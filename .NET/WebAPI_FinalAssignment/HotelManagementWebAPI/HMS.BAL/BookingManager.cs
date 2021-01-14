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
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingManager()
        {
            ;
        }

        public BookingManager(IBookingRepository bookingManager)
        {
            _bookingRepository = bookingManager;
        }

        public List<Booking> GetAllBookings()
        {
            return _bookingRepository.GetAllBookings();
        }

        public string CreateBooking(Booking model)
        {
            return _bookingRepository.CreateBooking(model);
        }

        public string UpdateBooking(Booking model)
        {
            return _bookingRepository.UpdateBooking(model);
        }

        public string DeleteBooking(int id)
        {
            return _bookingRepository.DeleteBooking(id);
        }
    }
}
