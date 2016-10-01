﻿using System;
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
                string[] data = null;
                string search = null;
                if (!string.IsNullOrEmpty(searchString))
                    data = searchString.Split(new string[] { " : " }, StringSplitOptions.None);


                switch (searchBy)
                {
                    case "1":
                        searchField = "LName";
                        search = data[2].ToString();
                        Members = uow.MemberRepository.GetMany(s => s.LName.Contains(search));
                        break;
                    case "2":
                        searchField = "BarcodeId";
                        search = data[0].ToString();
                        Members = uow.MemberRepository.GetMany(s => s.BarcodeId.Contains(search));
                        break;
                    case "3":
                        searchField = "FamilyId";
                        search = data[0].ToString();
                        Members = uow.MemberRepository.GetMany(s => s.BarcodeId.Contains(search));
                        break;
                    default:
                        searchField = "FName";
                        search = data[1].ToString();
                        Members = uow.MemberRepository.GetMany(s => s.FName.Contains(search));
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
                    var result = uow.MemberRepository.GetNames(p => p.LName.StartsWith(term)).Select(m => new { label = string.Concat(m.BarcodeId, " : ", m.FName, " : ", m.LName), id = m.ID });
                    return Json(result, JsonRequestBehavior.AllowGet);
                case "2":
                    var result1 = uow.MemberRepository.GetNames(p => p.BarcodeId.StartsWith(term)).Select(m => new { label = string.Concat(m.BarcodeId, " : ", m.FName, " : ", m.LName), id = m.ID });
                    return Json(result1, JsonRequestBehavior.AllowGet);
                case "3":
                    var result2 = uow.MemberRepository.GetNames(p => p.BarcodeId.StartsWith(term)).Select(m => new { label = string.Concat(m.BarcodeId, " : ", m.FName, " : ", m.LName), id = m.ID });
                    //var result2 = uow.MemberRepository.GetNames(p => p.FamilyId.StartsWith(term)).Select(m => new { label = m.FamilyId, id = m.ID });
                    return Json(result2, JsonRequestBehavior.AllowGet);
                default:
                    var result3 = uow.MemberRepository.GetNames(p => p.FName.StartsWith(term)).Select(m => new { label = string.Concat(m.BarcodeId, " : ", m.FName, " : ", m.LName), id = m.ID });
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
            ViewBag.FamilyId = memberMaster.FamilyId;
            IEnumerable<MemberMaster> member = uow.MemberRepository.GetMemberByFamilyId(memberMaster.FamilyId);

            IEnumerable<MemberDetailMaster> memberDetail = uow.MemberDetailRepository.GetMemberDetail(id);
            MemberDetailMaster memberDetailMaster = new MemberDetailMaster();

            IEnumerable<MemberAccountMaster> memberAccountDetail = uow.MemberAccountRepository.GetMemberAccountDetail(id);
            MemberAccountMaster memberAccountMaster = new MemberAccountMaster();
            if (memberDetail.Count() > 0)
            {
                if (memberAccountDetail.Count() > 0)
                {
                    memberAccountMaster = memberAccountDetail.Where(p => p.MemberID == id).First();
                }
                //.............

                memberDetailMaster = memberDetail.First();
                memberDetailMaster = memberDetail.Where(p => p.MemberID == id).First();
                var tuple = new Tuple<MemberMaster, IEnumerable<MemberMaster>, MemberDetailMaster, MemberAccountMaster>
                    (memberMaster, member, memberDetailMaster, memberAccountMaster);
                return View(tuple);
            }
            else
            {
                //memberDetailMaster = memberDetail.Where(p => p.MemberID == id).First();
                var tuple = new Tuple<MemberMaster, IEnumerable<MemberMaster>, MemberDetailMaster, MemberAccountMaster>
                    (memberMaster, member, memberDetailMaster, memberAccountMaster);
                return View(tuple);
            }

            if (memberMaster == null)
            {
                return HttpNotFound();
            }

            return View("~/Views/Member/AddDetails.cshtml");
        }

        // GET: Member/Create
        public ActionResult Create(int? id)
        {
            IEnumerable<MemberMaster> Members = uow.MemberRepository.GetAll();
            int fId = Members.Max(i => i.FamilyId);
            int mId = Members.Max(i => i.ID);
            mId = mId + 1;

            if (id == null)
            {
                fId = fId + 1;
                ViewData["FamilyId"] = fId;
                ViewData["BarcodeId"] = string.Concat(mId, "+", fId);
            }
            else
            {
                ViewData["FamilyId"] = id;
                ViewData["BarcodeId"] = string.Concat(mId, "+", id);
            }

            return View();
        }

        // POST: Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,BarcodeId,LName,FName,IsPrimary,FamilyId")] MemberMaster memberMaster)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<MemberMaster> Members = uow.MemberRepository.GetAll();
                int max = Members.Max(i => i.ID);
                int newid = max + 1;
                memberMaster.ID = max + 1;
                uow.MemberRepository.Add(memberMaster);
                uow.Save();
                return RedirectToAction("Details", new { id = newid });
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

        #region MemberDetail
        // GET: Member/Create
        public ActionResult AddDetails(int? id)
        {
            if (id != null)
                ViewData["MID"] = id;

            return View();
        }

        // POST: Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDetails([Bind(Include = "MemberID,Address,DOB,Sex,Email,THome,TBusiness,TFax,NewsLetter,MemberType")] MemberDetailMaster memberDetailMaster)
        {
            if (ModelState.IsValid)
            {
                //IEnumerable<MemberDetailMaster> MembersDetails = uow.MemberDetailRepository.GetAll();
                //int max = MembersDetails.Max(i => i.ID);
                //int newid = max + 1;
                //memberDetailMaster.ID = max + 1;
                uow.MemberDetailRepository.Add(memberDetailMaster);
                uow.Save();
                return RedirectToAction("Details", new { id = memberDetailMaster.MemberID });
                //return RedirectToAction("Details", new { id = newid });
            }
            return View(memberDetailMaster);
        }

        public ActionResult EditDetails(int? id)
        {
            if (id != null)
                ViewData["EMID"] = id;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<MemberDetailMaster> memberDetail = uow.MemberDetailRepository.GetMemberDetail(id);
            MemberDetailMaster memberDetailMaster;
            //if (memberDetail.Count() != 0)
            if (memberDetail.Count() > 0)
            {
                memberDetailMaster = memberDetail.First();
                memberDetailMaster = memberDetail.Where(p => p.MemberID == id).First();
                ViewData["DID"] = memberDetailMaster.ID;
                return View(memberDetailMaster);
            }

            if (memberDetail == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Member/AddDetails.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDetails([Bind(Include = "ID,MemberID,Address,DOB,Sex,Email,THome,TBusiness,TFax,NewsLetter,MemberType")] MemberDetailMaster memberDetailMaster)
        {
            if (ModelState.IsValid)
            {
                uow.MemberDetailRepository.Update(memberDetailMaster);
                uow.Save();
                return RedirectToAction("Details", new { id = memberDetailMaster.MemberID });
                //return RedirectToAction("Details", new { id = newid });
            }
            return View(memberDetailMaster);
        }
        #endregion

        #region MemberAccountDetail
        // GET: Member/Create
        public ActionResult AddAccountDetails(int? id)
        {
            if (id != null)
                ViewData["MAID"] = id;

            return View();
        }

        // POST: Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAccountDetails([Bind(Include = "MemberID,Paid,Amount,DepositDate,PaymentType,Comment")] MemberAccountMaster memberAccount)
        {
            if (ModelState.IsValid)
            {
                //IEnumerable<MemberAccountMaster> MembersAccount = uow.MemberAccountRepository.GetAll();
                //int max = MembersAccount.Max(i => i.ID);
                //int newid = max + 1;
                //memberMaster.ID = max + 1;
                uow.MemberAccountRepository.Add(memberAccount);
                uow.Save();
                return RedirectToAction("Details", new { id = memberAccount.MemberID });
                //return RedirectToAction("Details", new { id = newid });
            }
            return View(memberAccount);
        }

        public ActionResult EditAccountDetails(int? id)
        {
            if (id != null)
                ViewData["EMAID"] = id;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<MemberAccountMaster> memberAccountDetail = uow.MemberAccountRepository.GetMemberAccountDetail(id);
            MemberAccountMaster memberAccountMaster;
            if (memberAccountDetail.Count() > 0)
            {
                memberAccountMaster = memberAccountDetail.First();
                memberAccountMaster = memberAccountDetail.Where(p => p.MemberID == id).First();
                ViewData["DAID"] = memberAccountMaster.ID;
                return View(memberAccountMaster);
            }

            if (memberAccountDetail == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Member/AddDetails.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountDetails([Bind(Include = "ID,MemberID,Paid,Amount,DepositDate,PaymentType,Comment")] MemberAccountMaster memberAccountMaster)
        {
            if (ModelState.IsValid)
            {
                uow.MemberAccountRepository.Update(memberAccountMaster);
                uow.Save();
                return RedirectToAction("Details", new { id = memberAccountMaster.MemberID });
                //return RedirectToAction("Details", new { id = newid });
            }
            return View(memberAccountMaster);
        }
        #endregion
    }
}
