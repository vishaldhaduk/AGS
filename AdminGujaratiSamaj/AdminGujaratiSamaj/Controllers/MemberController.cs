using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminGujaratiSamaj.DAL;
using AdminGujaratiSamaj.Models;
using PagedList;
using System.Linq;
using System.Linq.Expressions;

namespace AdminGujaratiSamaj.Controllers
{
    public class MemberController : Controller
    {
        private UnitOfWork uow = new UnitOfWork();

        // GET: Member
        public ActionResult Index(int? page, string sortOrder, string currentFilter, string searchString, string searchBy, string MemberId)
        {
            ViewBag.LNameSortParm = String.IsNullOrEmpty(sortOrder) ? "lname_desc" : "";
            ViewBag.FNameSortParm = String.IsNullOrEmpty(sortOrder) ? "fname_asc" : "";
            ViewBag.FIDSortParm = String.IsNullOrEmpty(sortOrder) ? "family_id" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            IEnumerable<MemberMaster> Members = SearhMembers(searchString, searchBy);

            switch (sortOrder)
            {
                case "lname_desc":
                    Members = Members.OrderByDescending(s => s.LName);
                    break;
                case "fname_asc":
                    Members = Members.OrderBy(s => s.FName);
                    break;
                case "family_id":
                    Members = Members.OrderByDescending(s => s.FamilyId);
                    break;
                default:
                    Members = Members.OrderBy(s => s.LName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Members.ToPagedList(pageNumber, pageSize));
        }

        private IEnumerable<MemberMaster> SearhMembers(string searchString, string searchBy)
        {
            ViewBag.SearchBy = searchBy;
            string searchField = "FName";
            IEnumerable<MemberMaster> Members;
            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchBy)
                {
                    case "1":
                        searchField = "LName";
                        Members = uow.MemberRepository.GetMany(s => s.LName.Contains(searchString));
                        break;
                    case "2":
                        searchField = "BarcodeId";
                        Members = uow.MemberRepository.GetMany(s => s.BarcodeId.Contains(searchString));
                        break;
                    case "3":
                        searchField = "FamilyId";
                        Members = uow.MemberRepository.GetMany(s => s.BarcodeId.Contains(searchString));
                        break;
                    default:
                        searchField = "FName";
                        Members = uow.MemberRepository.GetMany(s => s.FName.Contains(searchString));
                        break;
                }
            }
            else
                Members = uow.MemberRepository.GetAll();

            ViewBag.SearchBy = searchField;
            ViewBag.CurrentFilter = searchString;

            return Members;
        }

        public JsonResult UpdateSearchCriteria(string term)
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchMemberAutoComplete(string term, string searchBy)
        {
            string a = GetCookieValue("search_criteria");
            switch (a)
            {
                case "1":
                    var result = uow.MemberRepository.GetNames(p => p.LName.StartsWith(term)).Select(m => new { label = m.LName, id = m.ID });
                    return Json(result, JsonRequestBehavior.AllowGet);
                case "2":
                    var result1 = uow.MemberRepository.GetNames(p => p.BarcodeId.StartsWith(term)).Select(m => new { label = m.BarcodeId, id = m.ID });
                    return Json(result1, JsonRequestBehavior.AllowGet);
                case "3":
                    var result2 = uow.MemberRepository.GetNames(p => p.BarcodeId.StartsWith(term)).Select(m => new { label = m.BarcodeId, id = m.ID });
                    //var result2 = uow.MemberRepository.GetNames(p => p.FamilyId.StartsWith(term)).Select(m => new { label = m.FamilyId, id = m.ID });
                    return Json(result2, JsonRequestBehavior.AllowGet);
                default:
                    var result3 = uow.MemberRepository.GetNames(p => p.FName.StartsWith(term)).Select(m => new { label = m.FName, id = m.ID });
                    return Json(result3, JsonRequestBehavior.AllowGet);
            }

            //uow.MemberRepository.GetNames(p => p.FName.StartsWith(term)).Select(m => new { label = m.FName, id = m.ID });
            //return Json(result, JsonRequestBehavior.AllowGet);
        }

        public string GetCookieValue(string key)
        {
            if (Request.Cookies[key] != null)
            {
                string userSettings;
                if (Request.Cookies[key] != null)
                {
                    userSettings = Request.Cookies[key].Value.ToString();
                    return userSettings;
                }
            }
            return "0";
        }

        [HttpGet]
        //Get : SEarch Members
        public ActionResult SearchMembers(string fName, string criteria, int? page)
        {
            //lName = "Patel";
            if (fName != null)
            {
                fName = WebUtility.UrlDecode(fName);
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                //IEnumerable<MemberMaster> Members = uow.MemberRepository.GetMany(d => d.FName.ToLower().Contains(fName.ToLower())).ToPagedList(pageNumber, pageSize);
                return View("~/Views/Home/Index.cshtml", uow.MemberRepository.GetMany(d => d.FName.ToLower().Contains(fName.ToLower())).ToPagedList(pageNumber, pageSize));
                //return RedirectToAction(Members);
            }
            else
                return View();
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
