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
    public class AdminDetailController : ApiController
    {
        private readonly IAdminDetailManager _adminDetailManager;

        public AdminDetailController()
        {
            ;
        }

        public AdminDetailController(IAdminDetailManager adminDetailManager)
        {
            _adminDetailManager = adminDetailManager;
        }

        //GET: api/AdminDetail/1
        [HttpGet]
        [Route("api/AdminDetail/{id}")]
        public AdminDetail Get(int id)
        {
            return _adminDetailManager.GetAdminDetail(id);
        }

        //GET: api/AdminDetail
        [HttpGet]
        [Route("api/AdminDetail")]
        public List<AdminDetail> Get()
        {
            return _adminDetailManager.GetAllAdmins();
        }

        //POST: api/AdminDetail
        [HttpPost]
        [Route("api/AdminDetail")]
        public string Post([FromBody]AdminDetail model)
        {
            return _adminDetailManager.CreateAdmin(model);
        }

        //PUT: api/AdminDetail 
        [HttpPut]
        [Route("api/AdminDetail")]
        public string Put([FromBody]AdminDetail model)
        {
            return _adminDetailManager.UpdateAdmin(model);
        }

        //DELETE: api/AdminDetail/1
        [HttpDelete]
        [Route("api/AdminDetail/{id}")]
        public string Delete(int id)
        {
            return _adminDetailManager.DeleteAdmin(id);
        }
    }
}
