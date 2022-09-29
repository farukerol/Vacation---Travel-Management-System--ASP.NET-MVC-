using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCVacationManagement.Models.Entity;
namespace MVCVacationManagement.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage
        DbMvcVacationManagementEntities3 db = new DbMvcVacationManagementEntities3();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(TBLMUSTERI customer)
        {
            customer.rol = "musteri";
            db.TBLMUSTERI.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index","HomePage");
        }

        /*
        public JsonResult IsAldreadySignedUpCustomer(string userId)
        {


            //var customerDetails = db.TBLMUSTERI.Where(a => a.kullanici_adi.ToLower() == userId.ToLower()).FirstOrDefault();

            var customerDetails = db.TBLMUSTERI.Where(x => x.kullanici_adi.ToLower() == userId.ToLower()).FirstOrDefault();
            bool status;
            if (customerDetails != null)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return Json(status, JsonRequestBehavior.AllowGet);

        }
        */
    }
}