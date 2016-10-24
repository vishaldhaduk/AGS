using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AdminGujaratiSamaj.DAL;
using AdminGujaratiSamaj.Models;

namespace AdminGujaratiSamaj.Controllers
{
    public class WebApiController : ApiController
    {
        private AGSDBContext db = new AGSDBContext();

        // GET: api/WebApi
        public IQueryable<EntryMaster> GetEntryMasters()
        {
            return db.EntryMasters;
        }

        // GET: api/WebApi/5
        [ResponseType(typeof(EntryMaster))]
        public async Task<IHttpActionResult> GetEntryMaster(int id)
        {
            EntryMaster entryMaster = await db.EntryMasters.FindAsync(id);
            if (entryMaster == null)
            {
                return NotFound();
            }

            return Ok(entryMaster);
        }

        // PUT: api/WebApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEntryMaster(int id, EntryMaster entryMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entryMaster.ID)
            {
                return BadRequest();
            }

            db.Entry(entryMaster).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryMasterExists(id))
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

        // POST: api/WebApi
        [ResponseType(typeof(EntryMaster))]
        public async Task<IHttpActionResult> PostEntryMaster(EntryMaster entryMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EntryMasters.Add(entryMaster);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = entryMaster.ID }, entryMaster);
        }

        // DELETE: api/WebApi/5
        [ResponseType(typeof(EntryMaster))]
        public async Task<IHttpActionResult> DeleteEntryMaster(int id)
        {
            EntryMaster entryMaster = await db.EntryMasters.FindAsync(id);
            if (entryMaster == null)
            {
                return NotFound();
            }

            db.EntryMasters.Remove(entryMaster);
            await db.SaveChangesAsync();

            return Ok(entryMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntryMasterExists(int id)
        {
            return db.EntryMasters.Count(e => e.ID == id) > 0;
        }
    }
}