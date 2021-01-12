using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProductAssignmentWebAPI.Models;

namespace ProductAssignmentWebAPI.Controllers
{
    public class SmallImageDetailsController : ApiController
    {
        private ProductAssignmentEntities db = new ProductAssignmentEntities();

        // GET: api/SmallImageDetails
        public IQueryable<SmallImageDetail> GetSmallImageDetails()
        {
            return db.SmallImageDetails;
        }

        // GET: api/SmallImageDetails/5
        [ResponseType(typeof(SmallImageDetail))]
        public IHttpActionResult GetSmallImageDetail(int id)
        {
            SmallImageDetail smallImageDetail = db.SmallImageDetails.Find(id);
            if (smallImageDetail == null)
            {
                return NotFound();
            }

            return Ok(smallImageDetail);
        }

        // PUT: api/SmallImageDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSmallImageDetail(int id, SmallImageDetail smallImageDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != smallImageDetail.Id)
            {
                return BadRequest();
            }

            db.Entry(smallImageDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmallImageDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SmallImageDetails
        [ResponseType(typeof(SmallImageDetail))]
        public IHttpActionResult PostSmallImageDetail(SmallImageDetail smallImageDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SmallImageDetails.Add(smallImageDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = smallImageDetail.Id }, smallImageDetail);
        }

        // DELETE: api/SmallImageDetails/5
        [ResponseType(typeof(SmallImageDetail))]
        public IHttpActionResult DeleteSmallImageDetail(int id)
        {
            SmallImageDetail smallImageDetail = db.SmallImageDetails.Find(id);
            if (smallImageDetail == null)
            {
                return NotFound();
            }

            db.SmallImageDetails.Remove(smallImageDetail);
            db.SaveChanges();

            return Ok(smallImageDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SmallImageDetailExists(int id)
        {
            return db.SmallImageDetails.Count(e => e.Id == id) > 0;
        }
    }
}