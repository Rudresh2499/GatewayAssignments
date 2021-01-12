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
    public class LargeImageDetailsController : ApiController
    {
        private ProductAssignmentEntities db = new ProductAssignmentEntities();

        // GET: api/LargeImageDetails
        public IQueryable<LargeImageDetail> GetLargeImageDetails()
        {
            return db.LargeImageDetails;
        }

        // GET: api/LargeImageDetails/5
        [ResponseType(typeof(LargeImageDetail))]
        public IHttpActionResult GetLargeImageDetail(int id)
        {
            LargeImageDetail largeImageDetail = db.LargeImageDetails.Find(id);
            if (largeImageDetail == null)
            {
                return NotFound();
            }

            return Ok(largeImageDetail);
        }

        // PUT: api/LargeImageDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLargeImageDetail(int id, LargeImageDetail largeImageDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != largeImageDetail.Id)
            {
                return BadRequest();
            }

            db.Entry(largeImageDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LargeImageDetailExists(id))
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

        // POST: api/LargeImageDetails
        [ResponseType(typeof(LargeImageDetail))]
        public IHttpActionResult PostLargeImageDetail(LargeImageDetail largeImageDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LargeImageDetails.Add(largeImageDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = largeImageDetail.Id }, largeImageDetail);
        }

        // DELETE: api/LargeImageDetails/5
        [ResponseType(typeof(LargeImageDetail))]
        public IHttpActionResult DeleteLargeImageDetail(int id)
        {
            LargeImageDetail largeImageDetail = db.LargeImageDetails.Find(id);
            if (largeImageDetail == null)
            {
                return NotFound();
            }

            db.LargeImageDetails.Remove(largeImageDetail);
            db.SaveChanges();

            return Ok(largeImageDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LargeImageDetailExists(int id)
        {
            return db.LargeImageDetails.Count(e => e.Id == id) > 0;
        }
    }
}