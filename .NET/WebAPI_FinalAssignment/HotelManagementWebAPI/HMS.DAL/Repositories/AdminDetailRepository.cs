using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Models;

namespace HMS.DAL.Repositories
{
    public class AdminDetailRepository : IAdminDetailRepository
    {
        private readonly DatabaseEntities.HotelManagementEntities _dbcontext;

        public AdminDetailRepository()
        {
            _dbcontext = new DatabaseEntities.HotelManagementEntities();
        }

        public AdminDetail GetAdminDetail(int id)
        {
            var entity = _dbcontext.AdminDetails.Find(id);
            AdminDetail tempObject = new AdminDetail();

            if(entity != null)
            {
                tempObject.Id = entity.Id;
                tempObject.EmailId = entity.EmailId;
                tempObject.Password = entity.Password;
            }

            return (tempObject);
        }

        public List<AdminDetail> GetAllAdmins()
        {
            var entities = _dbcontext.AdminDetails.ToList();
            List<AdminDetail> listOfAdmins = new List<AdminDetail>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    AdminDetail tempObject = new AdminDetail();

                    tempObject.Id = item.Id;
                    tempObject.EmailId = item.EmailId;
                    tempObject.Password = item.Password;

                    listOfAdmins.Add(tempObject);
                }
            }
            return (listOfAdmins);
        }

        public string CreateAdmin(AdminDetail model)
        {
            try
            {
                if(model != null)
                {
                    DatabaseEntities.AdminDetail entity = new DatabaseEntities.AdminDetail();

                    entity.EmailId = model.EmailId;
                    entity.Password = model.Password;

                    _dbcontext.AdminDetails.Add(entity);
                    _dbcontext.SaveChanges();
                    return ("Data Added");
                }
                return ("Could not add the data");
            }
            catch(Exception e)
            {
                return ("Exception Occured :" + e.Message);
            }
        }

        public string UpdateAdmin(AdminDetail model)
        {
            try
            {
                var entity = _dbcontext.AdminDetails.Find(model.Id);

                if(entity != null)
                {
                    entity.EmailId = model.EmailId;
                    entity.Password = model.Password;

                    _dbcontext.SaveChanges();
                    return ("Successfully Updated");
                }
                return ("Could not find the record");
            }
            catch(Exception e)
            {
                return ("Exception Occured :" + e.Message);
            }
        }

        public string DeleteAdmin(int id)
        {
            try
            {
                var entity = _dbcontext.AdminDetails.Find(id);

                if(entity != null)
                {
                    _dbcontext.AdminDetails.Remove(entity);
                    _dbcontext.SaveChanges();

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
