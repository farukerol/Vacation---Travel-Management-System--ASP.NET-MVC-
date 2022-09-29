using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCVacationManagement.Models.Entity;
namespace MVCVacationManagement.Controllers
{
    public class HotelManagerController : Controller
    {
        // GET: HotelManager
        DbMvcVacationManagementEntities3 db = new DbMvcVacationManagementEntities3();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListCustomers(string search)
        {
            var hotelManagerId = Session["hotelmanager_id"].ToString();
            var customers = db.TBLHOTELREZERVASYON.Where(x => x.TBLHOTEL.TBLHOTELYETKILISI.id.ToString() == hotelManagerId);
            if (!string.IsNullOrEmpty(search))
            {
                customers = customers.Where(m => m.TBLHOTEL.isim.Contains(search) || m.TBLMUSTERI.ad.Contains(search) );
            }
            return View(customers.ToList());
        }
    }
}