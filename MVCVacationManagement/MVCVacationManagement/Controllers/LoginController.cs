using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCVacationManagement.Models.Entity;
using System.Web.Security;
namespace MVCVacationManagement.Controllers
{
    public class LoginController : Controller
    {
        DbMvcVacationManagementEntities3 db = new DbMvcVacationManagementEntities3();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(TBLADMIN admin,TBLMUSTERI customer, TBLHOTELYETKILISI hotelManager, TBLTURYETKILISI tourManager)
        {
            var adminInfo = db.TBLADMIN.FirstOrDefault(x => x.kullanici_adi == admin.kullanici_adi && x.sifre == admin.sifre);
            var customerInfo = db.TBLMUSTERI.FirstOrDefault(x => x.kullanici_adi == customer.kullanici_adi && x.sifre == customer.sifre);
            var hotelManagerInfo = db.TBLHOTELYETKILISI.FirstOrDefault(x => x.kullanici_adi == hotelManager.kullanici_adi && x.sifre == hotelManager.sifre);
            var tourManagerInfo = db.TBLTURYETKILISI.FirstOrDefault(x => x.kullanici_adi == tourManager.kullanici_adi && x.sifre == tourManager.sifre);

            if (adminInfo != null)
            {
                FormsAuthentication.SetAuthCookie(adminInfo.kullanici_adi, false);
                Session["admin_username"] = adminInfo.kullanici_adi.ToString();
                Session["admin_id"] = adminInfo.id.ToString();
                
                return RedirectToAction("Index", "HomePage");
            }
            else if(customerInfo != null){
                FormsAuthentication.SetAuthCookie(customerInfo.kullanici_adi, false);
                Session["customer_username"] = customerInfo.kullanici_adi.ToString();
                Session["customer_id"] = customerInfo.id.ToString();
                Session["customer_name"] = customerInfo.ad.ToString();
                Session["customer_surname"] = customerInfo.soyad.ToString();
                
                return RedirectToAction("Index", "HomePage");
            }
            else if(hotelManagerInfo != null)
            {
                FormsAuthentication.SetAuthCookie(hotelManagerInfo.kullanici_adi, false);
                Session["hotelmanager_username"] = hotelManagerInfo.kullanici_adi.ToString();
                Session["hotelmanager_id"] = hotelManager.id.ToString();
                return RedirectToAction("Index", "HomePage");
            }
            else if (tourManagerInfo != null)
            {
                FormsAuthentication.SetAuthCookie(tourManagerInfo.kullanici_adi, false);
                Session["tourmanager_username"] = tourManagerInfo.kullanici_adi.ToString();
                Session["tourmanager_id"] = tourManagerInfo.id.ToString();
                Session["tourmanager_name"] = tourManagerInfo.ad.ToString();
                return RedirectToAction("Index", "HomePage");
            }
            else
            {
                return View();
            }
           
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Homepage");
        }
    }
}

/*
[HttpPost]
public ActionResult Login(TBLADMIN admin)
{
    var info = db.TBLADMIN.FirstOrDefault(x => x.kullanici_adi == admin.kullanici_adi && x.sifre == admin.sifre);
    if (info != null)
    {
        FormsAuthentication.SetAuthCookie(info.kullanici_adi, false);
        return RedirectToAction("Index", "Admin");
    }
    else
    {
        return View();
    }

}*/