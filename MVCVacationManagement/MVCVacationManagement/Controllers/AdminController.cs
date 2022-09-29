using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCVacationManagement.Models.Entity;
using PagedList;
using PagedList.Mvc;
using System.IO;
namespace MVCVacationManagement.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        
        DbMvcVacationManagementEntities3 db = new DbMvcVacationManagementEntities3();
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewTour()
        {
            List<SelectListItem> tourmanager = ( from x in db.TBLTURYETKILISI.Where(x => x.durum == true).ToList()
                                                 select new SelectListItem
                                        {                               
                                            Text = x.ad + " " +x.soyad,
                                            Value = x.id.ToString()
                                        }).ToList();

            ViewBag.drop = tourmanager;
            return View();
        }

        [HttpGet]
        public ActionResult NewHotel()
        {
            List<SelectListItem> hotelManagers = (from x in db.TBLHOTELYETKILISI.Where(x => x.durum == true).ToList()
                                                  select new SelectListItem
                                                {
                                                    Text = x.ad + " " + x.soyad,
                                                    Value = x.id.ToString()
                                                }).ToList();

            ViewBag.dropHotelManagers = hotelManagers;
            return View();
        }

        [HttpGet]
        public ActionResult NewTourManager()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NewHotelManager()
        {
            return View();
        }


        public ActionResult ListTours()
        {
            var tours = db.TBLTUR.Where(x => x.durum == true).ToList();
            return View(tours);
        }

        public ActionResult ListHotels()
        {
            var hotels = db.TBLHOTEL.Where(x => x.durum == true).ToList();
            return View(hotels);
        }

        public ActionResult ListTourManagers()
        {
            var tourmanagers = db.TBLTURYETKILISI.Where(x => x.durum == true).ToList();
            return View(tourmanagers);
        }

        public ActionResult ListHotelManagers()
        {
            var hotelmanagers = db.TBLHOTELYETKILISI.Where(x => x.durum == true).ToList();
            return View(hotelmanagers);
        }

        [HttpPost]
        public ActionResult NewTour(TBLTUR tour)
        {
          
            var tourmanager = db.TBLTURYETKILISI.Where(x => x.id == tour.TBLTURYETKILISI.id).FirstOrDefault();
            tour.TBLTURYETKILISI = tourmanager;
        
                if (Request.Files.Count > 0)
                {
               
                    string photoFileName = Path.GetFileName(Request.Files[0].FileName);
                    string fileExtension = Path.GetExtension(Request.Files[0].FileName);
                    string fileUrl = "~/Image" + photoFileName + fileExtension;
                    Request.Files[0].SaveAs(Server.MapPath(fileUrl));
                    tour.fotograf = "/Image" + photoFileName + fileExtension;
                }
         
            tour.durum = true;
            db.TBLTUR.Add(tour);
            db.SaveChanges();
            return RedirectToAction("ListTours");
        }

        [HttpPost]
        public ActionResult NewHotel(TBLHOTEL hotel)
        {

            var hotelManager = db.TBLHOTELYETKILISI.Where(x => x.id == hotel.TBLHOTELYETKILISI.id).FirstOrDefault();
            hotel.TBLHOTELYETKILISI = hotelManager;
            if (Request.Files.Count > 0)
            {
                for (int i=0; i<3; i++)
                {
                    string photoFileName = Path.GetFileName(Request.Files[i].FileName);
                    string fileExtension = Path.GetExtension(Request.Files[i].FileName);
                    string fileUrl = "~/Image" + photoFileName + fileExtension;
                    Request.Files[i].SaveAs(Server.MapPath(fileUrl));
                    if (i == 0)
                    {
                        hotel.fotograf = "/Image" + photoFileName + fileExtension;
                    }
                    if (i == 1)
                    {
                        hotel.fotograf2 = "/Image" + photoFileName + fileExtension;
                    }
                    if (i == 2)
                    {
                        hotel.fotograf3 = "/Image" + photoFileName + fileExtension;
                    }
                }
        
            }
            hotel.durum = true;
            db.TBLHOTEL.Add(hotel);
            db.SaveChanges();
            return RedirectToAction("ListHotels");
        }

        [HttpPost]
        public ActionResult NewTourManager(TBLTURYETKILISI tourmanager)
        {
            db.TBLTURYETKILISI.Add(tourmanager);
            tourmanager.durum = true;
            tourmanager.rol ="tur_yetkilisi";
            db.SaveChanges();
            return RedirectToAction("ListTourManagers");
        }

        [HttpPost]
        public ActionResult NewHotelManager(TBLHOTELYETKILISI hotelmanager)
        {
            db.TBLHOTELYETKILISI.Add(hotelmanager);
            hotelmanager.durum = true;
            hotelmanager.rol ="hotel_yetkilisi";
            db.SaveChanges();
            return RedirectToAction("ListHotelManagers");
        }



        public ActionResult TourInformations(int id)
        {
            List<SelectListItem> tourmanagers = (from x in db.TBLTURYETKILISI.Where(x => x.durum == true).ToList()
                                                 select new SelectListItem
                                        {
                                            Text = x.ad + " " + x.soyad,
                                            Value = x.id.ToString()
                                        }).ToList();
            var tour = db.TBLTUR.Find(id);
            ViewBag.tourmanagername = tourmanagers;
            return View("TourInformations", tour);
        }

        public ActionResult HotelInformations(int id)
        {
            List<SelectListItem> hotelManagers = (from x in db.TBLHOTELYETKILISI.Where(x => x.durum == true).ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.ad + " " + x.soyad,
                                                     Value = x.id.ToString()
                                                 }).ToList();
            var hotel = db.TBLHOTEL.Find(id);
            ViewBag.hotelmanagername = hotelManagers;
            return View("HotelInformations", hotel);
        }

        public ActionResult TourManagerInformations(int id)
        {
            var tourManager = db.TBLTURYETKILISI.Find(id);
            return View("TourManagerInformations", tourManager);
        }

        public ActionResult HotelManagerInformations(int id)
        {
            var hotelManager = db.TBLHOTELYETKILISI.Find(id);
            return View("HotelManagerInformations", hotelManager);
        }

        
        public ActionResult UpdateTour(TBLTUR oldTour)
        {
            var updatedTour = db.TBLTUR.Find(oldTour.id);
            updatedTour.tur_basligi = oldTour.tur_basligi;
            updatedTour.kalkis_yeri = oldTour.kalkis_yeri;
            updatedTour.kontenjan = oldTour.kontenjan;
            updatedTour.sure = oldTour.sure;
            updatedTour.fotograf = oldTour.fotograf;
            updatedTour.ucret = oldTour.ucret;
            updatedTour.tarih = oldTour.tarih;
            updatedTour.detay = oldTour.detay;

            var tourmanager = db.TBLTURYETKILISI.Where(x => x.id == oldTour.TBLTURYETKILISI.id).FirstOrDefault();
            updatedTour.tur_yetkilisi = tourmanager.id;
            db.SaveChanges();
            return RedirectToAction("ListTours");
        }

        
        public ActionResult UpdateHotel(TBLHOTEL oldHotel)
        {
            var updatedHotel = db.TBLHOTEL.Find(oldHotel.id);
            updatedHotel.isim = oldHotel.isim;
            updatedHotel.konum = oldHotel.konum;
            updatedHotel.fotograf = oldHotel.fotograf;
            updatedHotel.bir_kisilik_oda_sayisi = oldHotel.bir_kisilik_oda_sayisi;
            updatedHotel.bir_kisilik_oda_ucret = oldHotel.bir_kisilik_oda_ucret;
            updatedHotel.iki_kisilik_oda_sayisi = oldHotel.iki_kisilik_oda_sayisi;
            updatedHotel.iki_kisilik_oda_ucret = oldHotel.iki_kisilik_oda_ucret;
            updatedHotel.uc_kisilik_oda_sayisi = oldHotel.uc_kisilik_oda_sayisi;
            updatedHotel.uc_kisilik_oda_ucret = oldHotel.uc_kisilik_oda_ucret;
            updatedHotel.dort_kisilik_oda_sayisi = oldHotel.dort_kisilik_oda_sayisi;
            updatedHotel.dort_kisilik_oda_ucret = oldHotel.dort_kisilik_oda_ucret;
            updatedHotel.detaylar = oldHotel.detaylar;

            var hotelmanager = db.TBLHOTELYETKILISI.Where(x => x.id == oldHotel.TBLHOTELYETKILISI.id).FirstOrDefault();
            updatedHotel.hotel_yetkilisi = hotelmanager.id;

            db.SaveChanges();
            return RedirectToAction("ListHotels");
        }


       
        public ActionResult UpdateTourManager(TBLTURYETKILISI oldTourManager)
        {
            var updatedTourManager = db.TBLTURYETKILISI.Find(oldTourManager.id);
            updatedTourManager.ad = oldTourManager.ad;
            updatedTourManager.soyad = oldTourManager.soyad;
            updatedTourManager.kullanici_adi = oldTourManager.kullanici_adi;
            updatedTourManager.sifre = oldTourManager.sifre;
            db.SaveChanges();
            return RedirectToAction("ListTourManagers");
        }

        
        public ActionResult UpdateHotelManager(TBLHOTELYETKILISI oldHotelManager)
        {
            var updatedHotelManager = db.TBLHOTELYETKILISI.Find(oldHotelManager.id);
            updatedHotelManager.ad = oldHotelManager.ad;
            updatedHotelManager.soyad = oldHotelManager.soyad;
            updatedHotelManager.kullanici_adi = oldHotelManager.kullanici_adi;
            updatedHotelManager.sifre = oldHotelManager.sifre;
            db.SaveChanges();
            return RedirectToAction("ListHotelManagers");
        }

        
        public ActionResult DeleteTour(TBLTUR tour)
        {
            var deletedTour = db.TBLTUR.Find(tour.id);
            deletedTour.durum = false;
            db.SaveChanges();
            return RedirectToAction("ListTours");
        }

        
        public ActionResult DeleteHotel(TBLHOTEL hotel)
        {
            var deletedHotel = db.TBLTUR.Find(hotel.id);
            deletedHotel.durum = false;
            db.SaveChanges();
            return RedirectToAction("ListHotels");
        }

        
        public ActionResult DeleteTourManager(TBLTURYETKILISI tourManager)
        {
            var findTourManager = db.TBLTURYETKILISI.Find(tourManager.id);
            findTourManager.durum = false;
            db.SaveChanges();
            return RedirectToAction("ListTourManagers");
        }

        
        public ActionResult DeleteHotelManager(TBLHOTELYETKILISI hotelManager)
        {
            var findHotelManager = db.TBLHOTELYETKILISI.Find(hotelManager.id);
            findHotelManager.durum = false;
            db.SaveChanges();
            return RedirectToAction("ListHotelManagers");
        }


    }
}