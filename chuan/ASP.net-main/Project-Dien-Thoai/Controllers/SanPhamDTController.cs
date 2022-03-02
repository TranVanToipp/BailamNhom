using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Dien_Thoai.Models;

namespace Project_Dien_Thoai.Controllers
{
    
    public class SanPhamDTController : Controller
    {
        private Project_Dien_ThoaiDBContexxt db = new Project_Dien_ThoaiDBContexxt();
        // GET: SanPhamDT
        public ActionResult Index(string searchString = "")
        {
            if (searchString != "")
            {
                var links = from l in db.SanPhams select l;
                if (!string.IsNullOrEmpty(searchString))
                {
                    var lsanpham = (from g in db.LoaiSanPhams select g).ToList();
                    ViewBag.loaisp = lsanpham;
                    links = links.Where(s => s.TenSP.ToLower().Contains(searchString.ToLower()));
                    return View(links.ToList());
                }
            }
            ViewBag.SoMauTin = db.SanPhams.Count();
            var loai = (from g in db.LoaiSanPhams select g).ToList();
            ViewBag.loaisp = loai;
            return View(db.SanPhams.ToList());
        }
        public ActionResult Phanhang(string loai)
        {
            var l = (from g in db.LoaiSanPhams select g).ToList();
            ViewBag.loaisp = l;
            var hang = (from hg in db.SanPhams where hg.LoaiSP == loai select hg).ToList();
            return View(hang);
        }
    }
}