using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCVacationManagement.Models.Entity;
namespace MVCVacationManagement.Controllers
{
    public class TourManagerController : Controller
    {
        // GET: TourManager
        DbMvcVacationManagementEntities3 db = new DbMvcVacationManagementEntities3();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListCustomers(string tourName)
        {
            var tourmanagerid = Session["tourmanager_id"].ToString();
            var tourcustomers = db.TBLTURREZERVASYON.Where(x => x.TBLTUR.TBLTURYETKILISI.id.ToString() == tourmanagerid);
            if(!string.IsNullOrEmpty(tourName))
            {
                tourcustomers = tourcustomers.Where(m => m.TBLTUR.tur_basligi.Contains(tourName));
            }
            return View(tourcustomers.ToList());
        }

        public ActionResult ResponsibleTourList(string tourManagerId)
        {
            tourManagerId = Session["tourmanager_id"].ToString();
            var tourList = db.TBLTUR.Where(x => x.TBLTURYETKILISI.id.ToString() == tourManagerId);
            return View(tourList.ToList());
        }

        public ActionResult ListTourCustomers(int id)
        {
            var tourCustomers = db.TBLTURREZERVASYON.Where(x => x.TBLTUR.id == id);
            return View(tourCustomers.ToList());
        }

    }
}

/*
   public ActionResult ListCustomers()
        {
            var tourmanagerid = Session["tourmanager_id"].ToString();
            var tourcustomers = db.TBLTURREZERVASYON.Where(x => x.TBLTUR.TBLTURYETKILISI.id.ToString() == tourmanagerid).ToList();
            return View(tourcustomers);
        }
*/