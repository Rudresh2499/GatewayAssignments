using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly DatabaseEntities.HotelManagementEntities _dbContext;

        public HotelRepository()
        {
            _dbContext = new DatabaseEntities.HotelManagementEntities();
        }

        public Hotel GetHotelDetails(int id)
        {
            var entity = _dbContext.Hotels.Find(id);
            Hotel tempObject = new Hotel();

            if(entity != null)
            {
                tempObject.Id = entity.Id;
                tempObject.Name = entity.Name;
                tempObject.Address = entity.Address;
                tempObject.City = entity.City;
                tempObject.PinCode = entity.PinCode;
                tempObject.ContactNumber = entity.ContactNumber;
                tempObject.ContactPerson = entity.ContactPerson;
                tempObject.Website = entity.Website;
                tempObject.Facebook = entity.Facebook;
                tempObject.Twitter = entity.Twitter;
                tempObject.IsActive = entity.IsActive;
                tempObject.CreatedDate = entity.CreatedDate;
                tempObject.CreatedBy = entity.CreatedBy;
                tempObject.UpdatedDate = entity.UpdatedDate;
                tempObject.UpdatedBy = entity.UpdatedBy;
            }

            return (tempObject);
        }

        public List<Hotel> GetAllHotelDetails()
        {
            var entities = _dbContext.Hotels.OrderBy(hotel => hotel.Name).ToList();
            List<Hotel> listOfHotels = new List<Hotel>();

            if(entities != null)
            {
                foreach(var item in entities)
                {
                    Hotel tempObject = new Hotel();

                    tempObject.Id = item.Id;
                    tempObject.Name = item.Name;
                    tempObject.Address = item.Address;
                    tempObject.City = item.City;
                    tempObject.PinCode = item.PinCode;
                    tempObject.ContactNumber = item.ContactNumber;
                    tempObject.ContactPerson = item.ContactPerson;
                    tempObject.Website = item.Website;
                    tempObject.Facebook = item.Facebook;
                    tempObject.Twitter = item.Twitter;
                    tempObject.IsActive = item.IsActive;
                    tempObject.CreatedDate = item.CreatedDate;
                    tempObject.CreatedBy = item.CreatedBy;
                    tempObject.UpdatedDate = item.UpdatedDate;

                    listOfHotels.Add(tempObject);
                }
            }
            return (listOfHotels);
        }

        public string CreateHotel(Hotel model)
        {
            try
            {
                if(model != null)
                {
                    DatabaseEntities.Hotel entity = new DatabaseEntities.Hotel();

                    entity.Name = model.Name;
                    entity.Address = model.Address;
                    entity.City = model.City;
                    entity.PinCode = model.PinCode;
                    entity.ContactNumber = model.ContactNumber;
                    entity.ContactPerson = model.ContactPerson;
                    entity.Website = model.Website;
                    entity.Facebook = model.Facebook;
                    entity.Twitter = model.Twitter;
                    entity.IsActive = model.IsActive;
                    entity.CreatedDate = model.CreatedDate;
                    entity.CreatedBy = model.CreatedBy;
                    entity.UpdatedDate = model.UpdatedDate;
                    entity.UpdatedBy = model.UpdatedBy;

                    _dbContext.Hotels.Add(entity);
                    _dbContext.SaveChanges();

                    return ("Data Added");
                }
                return ("Could not add the data");
            }
            catch(Exception e)
            {
                return ("Exception Occured :" + e.Message);
            }
        }

        public string UpdateHotel(Hotel model)
        {
            try
            {
                var entity = _dbContext.Hotels.Find(model.Id);

                if(entity != null)
                {
                    entity.Name = model.Name;
                    entity.Address = model.Address;
                    entity.City = model.City;
                    entity.PinCode = model.PinCode;
                    entity.ContactNumber = model.ContactNumber;
                    entity.ContactPerson = model.ContactPerson;
                    entity.Website = model.Website;
                    entity.Facebook = model.Facebook;
                    entity.Twitter = model.Twitter;
                    entity.IsActive = model.IsActive;
                    entity.CreatedDate = model.CreatedDate;
                    entity.CreatedBy = model.CreatedBy;
                    entity.UpdatedDate = model.UpdatedDate;
                    entity.UpdatedBy = model.UpdatedBy;

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

        public string DeleteHotel(int id)
        {
            try
            {
                var entity = _dbContext.Hotels.Find(id);

                if(entity != null)
                {
                    _dbContext.Hotels.Remove(entity);
                    _dbContext.SaveChanges();

                    return ("Record Deleted Successfully");
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
