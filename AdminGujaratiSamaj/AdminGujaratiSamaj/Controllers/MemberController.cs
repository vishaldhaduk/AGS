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
    public class MemberController : Controller
    {
        private UnitOfWork uow = new UnitOfWork();

        // GET: Member
        public ActionResult Index()
        {
            return View(uow.MemberRepository.GetAll());
        }


        [HttpGet]
        //Get : Products/Alternative
        public ActionResult SearchMembers(string lName)
        {
            lName = WebUtility.UrlDecode(lName);
            MemberMaster mem = uow.MemberRepository.Get(
                filter: d => d.LName == lName,
                includeProperties: "MemberDetails"
               ).First();

            return View(mem);
        }


        // GET: Member/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberMaster memberMaster = uow.MemberRepository.GetByID(id);
            if (memberMaster == null)
            {
                return HttpNotFound();
            }
            return View(memberMaster);
        }

        // GET: Member/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BarcodeId,LName,FName,IsPrimary,FamilyId")] MemberMaster memberMaster)
        {
            if (ModelState.IsValid)
            {
                uow.MemberRepository.Add(memberMaster);
                uow.Save();
                return RedirectToAction("Index");
            }

            return View(memberMaster);
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberMaster memberMaster = uow.MemberRepository.GetByID(id);
            if (memberMaster == null)
            {
                return HttpNotFound();
            }
            return View(memberMaster);
        }

        // POST: Member/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BarcodeId,LName,FName,IsPrimary,FamilyId")] MemberMaster memberMaster)
        {
            if (ModelState.IsValid)
            {
                uow.MemberRepository.Update(memberMaster);
                uow.Save();
                return RedirectToAction("Index");
            }
            return View(memberMaster);
        }

        // GET: Member/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberMaster memberMaster = uow.MemberRepository.GetByID(id);
            if (memberMaster == null)
            {
                return HttpNotFound();
            }
            return View(memberMaster);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberMaster memberMaster = uow.MemberRepository.GetByID(id);
            uow.MemberRepository.Delete(memberMaster);
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
