using HMS.BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.DAL.Repositories;
using HMS.Models;

namespace HMS.BAL
{
    public class AdminDetailManager : IAdminDetailManager
    {
        private readonly IAdminDetailRepository _adminDetailRepository;

        public AdminDetailManager(IAdminDetailRepository adminDetailRepository)
        {
            _adminDetailRepository = adminDetailRepository;
        }

        public AdminDetail GetAdminDetail(int id)
        {
            return _adminDetailRepository.GetAdminDetail(id);
        }

        public List<AdminDetail> GetAllAdmins()
        {
            return _adminDetailRepository.GetAllAdmins();
        }

        public string CreateAdmin(AdminDetail model)
        {
            return _adminDetailRepository.CreateAdmin(model);
        }

        public string UpdateAdmin(AdminDetail model)
        {
            return _adminDetailRepository.UpdateAdmin(model);
        }

        public string DeleteAdmin(int id)
        {
            return _adminDetailRepository.DeleteAdmin(id);
        }
    }
}
