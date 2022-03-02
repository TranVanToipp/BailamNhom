using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Dien_Thoai.Models;
namespace Project_Dien_Thoai.Controllers
{
    public class TrangchuController : Controller
    {
        private Project_Dien_ThoaiDBContexxt db = new Project_Dien_ThoaiDBContexxt();
        // GET: Trangchu
        public ActionResult Index(string searchString = "")
        {
            if (searchString != "")
            {
                var links = from l in db.SanPhams select l;
                if (!string.IsNullOrEmpty(searchString))
                {
                    links = links.Where(s => s.TenSP.ToLower().Contains(searchString.ToLower()));
                    return View(links.ToList());
                }
            }
            else 
                ViewBag.SoMauTin = db.SanPhams.Count();
                return View(db.SanPhams.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}