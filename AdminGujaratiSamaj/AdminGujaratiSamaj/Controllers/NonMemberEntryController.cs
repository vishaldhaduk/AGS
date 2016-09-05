﻿using System;
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
    public class NonMemberEntryController : Controller
    {
        private UnitOfWork uow = new UnitOfWork();

        // GET: NonMemberEntry
        public ActionResult Index(int? page, string sortOrder)
        {
            ViewBag.PaidSortParm = String.IsNullOrEmpty(sortOrder) ? "paid_desc" : "";
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";

            IEnumerable<NonMemberEntryMaster> NMEntry = uow.NonMemberEntryRepository.GetAll();

            switch (sortOrder)
            {
                case "paid_desc":
                    NMEntry = NMEntry.OrderByDescending(s => s.Paid);
                    break;
                case "date_desc":
                    NMEntry = NMEntry.OrderByDescending(s => s.Date);
                    break;
                default:
                    NMEntry = NMEntry.OrderBy(s => s.Paid);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(NMEntry.ToPagedList(pageNumber, pageSize));
        }

        // GET: NonMemberEntry/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonMemberEntryMaster nonMemberEntryMaster = uow.NonMemberEntryRepository.GetByID(id);
            if (nonMemberEntryMaster == null)
            {
                return HttpNotFound();
            }
            return View(nonMemberEntryMaster);
        }

        // GET: NonMemberEntry/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NonMemberEntry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Paid,Date,Comment")] NonMemberEntryMaster nonMemberEntryMaster)
        {
            if (ModelState.IsValid)
            {
                uow.NonMemberEntryRepository.Add(nonMemberEntryMaster);
                uow.Save();
                return RedirectToAction("Index");
            }

            return View(nonMemberEntryMaster);
        }

        // GET: NonMemberEntry/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonMemberEntryMaster nonMemberEntryMaster = uow.NonMemberEntryRepository.GetByID(id);
            if (nonMemberEntryMaster == null)
            {
                return HttpNotFound();
            }
            return View(nonMemberEntryMaster);
        }

        // POST: NonMemberEntry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Paid,Date,Comment")] NonMemberEntryMaster nonMemberEntryMaster)
        {
            if (ModelState.IsValid)
            {
                uow.NonMemberEntryRepository.Update(nonMemberEntryMaster);
                uow.Save();
                return RedirectToAction("Index");
            }
            return View(nonMemberEntryMaster);
        }

        // GET: NonMemberEntry/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonMemberEntryMaster nonMemberEntryMaster = uow.NonMemberEntryRepository.GetByID(id);
            if (nonMemberEntryMaster == null)
            {
                return HttpNotFound();
            }
            return View(nonMemberEntryMaster);
        }

        // POST: NonMemberEntry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NonMemberEntryMaster nonMemberEntryMaster = uow.NonMemberEntryRepository.GetByID(id);
            uow.NonMemberEntryRepository.Delete(nonMemberEntryMaster);
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
