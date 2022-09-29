using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCVacationManagement.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace MVCVacationManagement.Controllers
{
    public class TurController : Controller
    {
        // GET: Tur
        DbMvcVacationManagementEntities3 db = new DbMvcVacationManagementEntities3();

        /*
        public ActionResult Index(int page = 1)
        {
            var tourlist = db.TBLTUR.Where(x => x.durum == true).ToList().ToPagedList(page, 4);
            return View(tourlist);
        }
        */

        public ActionResult Index(string p,int page = 1)
        {
            var tourlist = from k in db.TBLTUR select k;
            if (!string.IsNullOrEmpty(p))
            {
                tourlist = tourlist.Where(m => m.tur_basligi.Contains(p) & m.durum==true);
            }
            return View(tourlist.Where(x => x.durum == true).ToList().ToPagedList(page, 4));
        }

        [Authorize(Roles = "musteri")]
        public ActionResult TourTicket(int id)
        {
            var tour = db.TBLTUR.Find(id);
            ViewBag.tourid = tour.id;
            ViewBag.tourname = tour.tur_basligi.ToString();
            ViewBag.kalkisYeri = tour.kalkis_yeri.ToString();
            ViewBag.tarih = tour.tarih.ToString();
            ViewBag.detaylar = tour.detay.ToString();
            ViewBag.turYetkilisi = db.TBLTUR.Find(id).TBLTURYETKILISI.ad + " " + db.TBLTUR.Find(id).TBLTURYETKILISI.soyad;
            return View("TourTicket",tour);
        }

        public ActionResult BuyTourTicket()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BuyTourTicket(int? tourid,int? customerid)
      {
            TBLTURREZERVASYON reservation = new TBLTURREZERVASYON();
            reservation.tur = tourid;
            reservation.musteri = customerid;
            db.TBLTUR.Find(tourid).kontenjan -= 1;
            db.TBLTURREZERVASYON.Add(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /*
        [HttpGet]
        public ActionResult newTour()
        {
            return View();
        }

        [HttpPost]
        public ActionResult newTour(TBLTUR tur)
        {
            db.TBLTUR.Add(tur);
            db.SaveChanges();
            return View();
        }*/
    }
}