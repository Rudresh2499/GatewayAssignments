using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.DAL.DatabaseEntities;

namespace HotelManagementWebAPI
{
    public class WebAPIAuthentication
    {
        public static bool Login(string emailId, string password)
        {
            HotelManagementEntities _dbContext = new HotelManagementEntities();

            return (_dbContext.AdminDetails.Any(admin => admin.EmailId.Equals(emailId) && admin.Password.Equals(password)));
        }
    }
}