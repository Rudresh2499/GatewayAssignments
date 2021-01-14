﻿using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BAL.Interface
{
    public interface IBookingManager
    {
        List<Booking> GetAllBookings();
        string CreateBooking(Booking model);
        string UpdateBooking(Booking model);
        string DeleteBooking(int id);
    }
}
