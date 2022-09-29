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
    public class HotelController : Controller
    {
        // GET: Hotel
        DbMvcVacationManagementEntities3 db = new DbMvcVacationManagementEntities3();
        public ActionResult Index(string p, int page = 1)
        {
            var hotellist = from k in db.TBLHOTEL select k;
            if (!string.IsNullOrEmpty(p))
            {
                hotellist = hotellist.Where(m => m.konum.Contains(p) || m.isim.Contains(p) & m.durum == true);
            }
            return View(hotellist.Where(x => x.durum == true).ToList().ToPagedList(page, 4));
        }

        [Authorize(Roles ="musteri")]
        public ActionResult HotelReservation(int id)
        {
            var hotel = db.TBLHOTEL.Find(id);
            TempData["hotel_id"] = hotel.id;
            TempData["hotel_fotograf"] = hotel.fotograf;
            TempData["hotel_fotograf2"] = hotel.fotograf2;
            TempData["hotel_fotograf3"] = hotel.fotograf3;
            ViewBag.hotelid = hotel.id;
            ViewBag.hoteladi = hotel.isim;
            ViewBag.konum = hotel.konum;
            ViewBag.detaylar = hotel.detaylar;
            ViewBag.fotograf = hotel.fotograf;
            ViewBag.bir_kisilik_ucret = hotel.bir_kisilik_oda_ucret;
            ViewBag.iki_kisilik_ucret = hotel.iki_kisilik_oda_ucret;
            ViewBag.uc_kisilik_ucret = hotel.uc_kisilik_oda_ucret;
            ViewBag.dort_kisilik_ucret = hotel.dort_kisilik_oda_ucret;
            ViewBag.hotelYetkilisi = db.TBLHOTEL.Find(id).TBLHOTELYETKILISI.ad + " " + db.TBLHOTEL.Find(id).TBLHOTELYETKILISI.soyad;
            return View("HotelReservation", hotel);
        }

        [HttpGet]
        public ActionResult ConfirmHotelReservation()
        {

                List<SelectListItem> numberOfPerson = new List<SelectListItem>();

                numberOfPerson.Add(new SelectListItem { Text = "1", Value = "1", Selected = true });

                numberOfPerson.Add(new SelectListItem { Text = "2", Value = "2" });

                numberOfPerson.Add(new SelectListItem { Text = "3", Value = "3" });

                numberOfPerson.Add(new SelectListItem { Text = "4", Value = "4" });

                ViewBag.person = numberOfPerson;          
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmHotelReservation(int? hotelid, int? customerid, TBLHOTELREZERVASYON reservation)
        {

            //TBLHOTELREZERVASYON reservation = new TBLHOTELREZERVASYON();
            reservation.hotel = hotelid;
            reservation.musteri = customerid;
            db.TBLHOTELREZERVASYON.Add(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }

}

/*
      public ActionResult Index(int page = 1)
      {
          var hotelList = db.TBLHOTEL.Where(x => x.durum == true).ToList().ToPagedList(page, 4);
          return View(hotelList);
      }*/