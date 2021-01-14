using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repositories
{
    public interface IAdminDetailRepository
    {
        AdminDetail GetAdminDetail(int id);
        List<AdminDetail> GetAllAdmins();
        string CreateAdmin(AdminDetail model);
        string UpdateAdmin(AdminDetail model);
        string DeleteAdmin(int id);
    }
}
