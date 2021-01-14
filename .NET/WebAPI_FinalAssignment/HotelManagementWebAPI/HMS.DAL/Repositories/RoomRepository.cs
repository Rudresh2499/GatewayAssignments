using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DatabaseEntities.HotelManagementEntities _dbContext;

        public RoomRepository()
        {
            _dbContext = new DatabaseEntities.HotelManagementEntities();
        }

        public Room GetRoomDetails(int id)
        {
            var entity = _dbContext.Rooms.Find(id);
            Room tempObject = new Room();

            if (entity != null)
            {
                tempObject.Id = entity.Id;
                tempObject.HotelId = entity.HotelId;
                tempObject.RoomName = entity.RoomName;
                tempObject.RoomCategory = entity.RoomCategory;
                tempObject.RoomPrice = entity.RoomPrice;
                tempObject.IsActive = entity.IsActive;
                tempObject.CreatedDate = entity.CreatedDate;
                tempObject.CreatedBy = entity.CreatedBy;
                tempObject.UpdatedDate = entity.UpdatedDate;
                tempObject.UpdatedBy = entity.UpdatedBy;
            }

            return (tempObject);
        }

        public List<Room> GetAllRoomDetails(string City = "", string PinCode = "", float Price = 0f, int Category = 0)
        {
            var entities = _dbContext.Rooms.OrderBy(room => room.RoomPrice).ToList();
            var hotelEntities = _dbContext.Hotels;

            List<Room> listOfRooms = new List<Room>();

            if (entities != null)
            {
                foreach (var item in entities)
                {
                    Room tempObject = new Room();

                    tempObject.Id = item.Id;
                    tempObject.HotelId = item.HotelId;
                    tempObject.RoomName = item.RoomName;
                    tempObject.RoomCategory = item.RoomCategory;
                    tempObject.RoomPrice = item.RoomPrice;
                    tempObject.IsActive = item.IsActive;
                    tempObject.CreatedDate = item.CreatedDate;
                    tempObject.CreatedBy = item.CreatedBy;
                    tempObject.UpdatedDate = item.UpdatedDate;
                    tempObject.UpdatedBy = item.UpdatedBy;

                    listOfRooms.Add(tempObject);
                }
            }
            listOfRooms.OrderBy(room => room.RoomPrice);
            if ((City == null || City == "") && (PinCode == null || PinCode.Equals("")) && Price == 0 && Category == 0)
            {
                return listOfRooms;
            }
            else if (City!=null && !City.Equals(""))
            {
                return listOfRooms.Where(room => _dbContext.Hotels.Find(room.HotelId).City.Equals(City)).ToList();
            }
            else if (Int32.TryParse(PinCode, out int convertedPinCode) && convertedPinCode != 0)
            {
                return listOfRooms.Where(room => _dbContext.Hotels.Find(room.HotelId).PinCode == PinCode).ToList();
            }
            else if (Category != 0)
            {
                return listOfRooms.Where(room => room.RoomCategory == Category).ToList();
            }
            else
            {
                return listOfRooms;
            }
        }

        public string CreateRoom(Room model)
        {
            try
            {
                if (model != null)
                {
                    DatabaseEntities.Room entity = new DatabaseEntities.Room();

                    entity.HotelId = model.HotelId;
                    entity.RoomName = model.RoomName;
                    entity.RoomCategory = model.RoomCategory;
                    entity.RoomPrice = model.RoomPrice;
                    entity.IsActive = model.IsActive;
                    entity.CreatedDate = model.CreatedDate;
                    entity.CreatedBy = model.CreatedBy;
                    entity.UpdatedDate = model.UpdatedDate;
                    entity.UpdatedBy = model.UpdatedBy;

                    _dbContext.Rooms.Add(entity);
                    _dbContext.SaveChanges();

                    return ("Data Added");
                }
                return ("Could not add the data");
            }
            catch (Exception e)
            {
                return ("Exception Occured :" + e.Message);
            }
        }

        public string UpdateRoom(Room model)
        {
            try
            {
                var entity = _dbContext.Rooms.Find(model.Id);

                if(entity != null)
                {
                    entity.HotelId = model.HotelId;
                    entity.RoomName = model.RoomName;
                    entity.RoomCategory = model.RoomCategory;
                    entity.RoomPrice = model.RoomPrice;
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
            catch (Exception e)
            {
                return ("Exception Occured :" + e.Message);
            }
        }

        public string DeleteRoom(int id)
        {
            try
            {
                var entity = _dbContext.Rooms.Find(id);

                if(entity != null)
                {
                    _dbContext.Rooms.Remove(entity);
                    _dbContext.SaveChanges();

                    return ("Record Deleted Successfully");
                }
                return ("Could not find the record");
            }
            catch(Exception e)
            {
                return("Exception Occured :" + e.Message);
            }
        }

        public bool isRoomAvailable(int id, DateTime date)
        {
            var entity = _dbContext.Bookings.ToList();

            if(entity != null)
            {
                foreach(var booking in entity)
                {
                    if(booking.RoomId == id && booking.BookingDate == date && (booking.StatusOfBooking != 0 || booking.StatusOfBooking != 2 || booking.StatusOfBooking != 3))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
