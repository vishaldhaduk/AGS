using AdminGujaratiSamaj.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminGujaratiSamaj.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork uow = new UnitOfWork();
        public ActionResult Index()
        {
            ViewBag.lName = Request.Form["lName"];
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public JsonResult SearchMemberAutoComplete(string term)
        {
            //var result = (from r in db.Customers
            //              where r.Country.ToLower().Contains(term.ToLower())
            //              select new { r.Country }).Distinct();
            var result = uow.MemberRepository.GetNames(p => p.FName.StartsWith(term)).Select(m => new { label = m.FName, id = m.ID});
            //  var test = Json(result, JsonRequestBehavior.AllowGet);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}