using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminGujaratiSamaj.DAL;
using AdminGujaratiSamaj.Models;
using PagedList;

namespace AdminGujaratiSamaj.Controllers
{
    public class EntryController : Controller
    {
        private UnitOfWork uow = new UnitOfWork();

        // GET: Entry
        public ActionResult Index(int? page, string sortOrder)
        {
            ViewBag.PaidSortParm = String.IsNullOrEmpty(sortOrder) ? "paid_desc" : "";
            ViewBag.SeatNoSortParm = String.IsNullOrEmpty(sortOrder) ? "seat_desc" : "";
            ViewBag.DiwaliPassSortParm = String.IsNullOrEmpty(sortOrder) ? "diwalipass_desc" : "";
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";

            IEnumerable<EntryMaster> EntryM = uow.EntryRepository.GetAll();

            switch (sortOrder)
            {
                case "paid_desc":
                    EntryM = EntryM.OrderByDescending(s => s.Paid);
                    break;
                case "seat_desc":
                    EntryM = EntryM.OrderByDescending(s => s.SeatNo);
                    break;
                case "diwalipass_desc":
                    EntryM = EntryM.OrderByDescending(s => s.DiwaliPass);
                    break;
                case "date_desc":
                    EntryM = EntryM.OrderByDescending(s => s.Date);
                    break;
                default:
                    EntryM = EntryM.OrderBy(s => s.SeatNo);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(EntryM.ToPagedList(pageNumber, pageSize));
        }

        // GET: Entry/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryMaster entryMaster = uow.EntryRepository.GetByID(id);
            if (entryMaster == null)
            {
                return HttpNotFound();
            }
            return View(entryMaster);
        }

        // GET: Entry/Create
        public ActionResult Create()
        {
            ViewBag.MemberMasterID = new SelectList(uow.MemberRepository.GetAll(), "ID", "BarcodeId");
            return View();
        }

        // POST: Entry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MemberID,Paid,SeatNo,DiwaliPass,Date,Comment")] EntryMaster entryMaster)
        {
            if (ModelState.IsValid)
            {
                uow.EntryRepository.Add(entryMaster);
                uow.Save();
                return RedirectToAction("Index");
            }

            ViewBag.MemberMasterID = new SelectList(uow.MemberRepository.GetAll(), "ID", "BarcodeId", entryMaster.MemberID);
            return View(entryMaster);
        }

        // GET: Entry/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryMaster entryMaster = uow.EntryRepository.GetByID(id);
            if (entryMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberMasterID = new SelectList(uow.MemberRepository.GetAll(), "ID", "BarcodeId", entryMaster.MemberID);
            return View(entryMaster);
        }

        // POST: Entry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MemberID,Paid,SeatNo,DiwaliPass,Date,Comment")] EntryMaster entryMaster)
        {
            if (ModelState.IsValid)
            {
                uow.EntryRepository.Update(entryMaster);
                uow.Save();
                return RedirectToAction("Index");
            }
            ViewBag.MemberMasterID = new SelectList(uow.MemberRepository.GetAll(), "ID", "BarcodeId", entryMaster.MemberID);
            return View(entryMaster);
        }

        // GET: Entry/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryMaster entryMaster = uow.EntryRepository.GetByID(id);
            if (entryMaster == null)
            {
                return HttpNotFound();
            }
            return View(entryMaster);
        }

        // POST: Entry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntryMaster entryMaster = uow.EntryRepository.GetByID(id);
            uow.EntryRepository.Delete(entryMaster);
            uow.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
