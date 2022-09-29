using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCVacationManagement.Models.Entity;

namespace MVCVacationManagement.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        DbMvcVacationManagementEntities3 db = new DbMvcVacationManagementEntities3();
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult ListTourTickets()
        {
            var customerid = Session["customer_id"].ToString();
            var tourTickets = db.TBLTURREZERVASYON.Where(x => x.musteri.ToString() == customerid).ToList();
            return View(tourTickets);
           
        }

        public ActionResult ListHotelReservations()
        {
            var customerid = Session["customer_id"].ToString();
            var hotelReservations = db.TBLHOTELREZERVASYON.Where(x => x.musteri.ToString() == customerid).ToList();
            return View(hotelReservations);
        }
    }
}