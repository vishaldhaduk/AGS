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

namespace AdminGujaratiSamaj.Controllers
{
    public class EntryController : Controller
    {
        private UnitOfWork uow = new UnitOfWork();

        // GET: Entry
        public ActionResult Index()
        {
            return View(uow.EntryRepository.GetAll());
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
        public ActionResult Create([Bind(Include = "ID,MemberMasterID,Paid,SeatNo,DiwaliPass,Date,Comment")] EntryMaster entryMaster)
        {
            if (ModelState.IsValid)
            {
                uow.EntryRepository.Add(entryMaster);
                uow.Save();
                return RedirectToAction("Index");
            }

            ViewBag.MemberMasterID = new SelectList(uow.MemberRepository.GetAll(), "ID", "BarcodeId", entryMaster.MemberMasterID);
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
            ViewBag.MemberMasterID = new SelectList(uow.MemberRepository.GetAll(), "ID", "BarcodeId", entryMaster.MemberMasterID);
            return View(entryMaster);
        }

        // POST: Entry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MemberMasterID,Paid,SeatNo,DiwaliPass,Date,Comment")] EntryMaster entryMaster)
        {
            if (ModelState.IsValid)
            {
                uow.EntryRepository.Update(entryMaster);
                uow.Save();
                return RedirectToAction("Index");
            }
            ViewBag.MemberMasterID = new SelectList(uow.MemberRepository.GetAll(), "ID", "BarcodeId", entryMaster.MemberMasterID);
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
