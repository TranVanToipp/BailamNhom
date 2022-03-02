using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Dien_Thoai.Models;

namespace Project_Dien_Thoai.Controllers
{
    public class ChiTietSPController : Controller
    {
        private Project_Dien_ThoaiDBContexxt db = new Project_Dien_ThoaiDBContexxt();
        // GET: ChiTietSP
        public ActionResult Index(string masp)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(s=>s.MaSP == masp);
            ViewBag.tensp = sp.TenSP;
            ViewBag.hinhsp = sp.HinhSP;
            ViewBag.dongia = sp.GiaDauRa;
            ViewBag.tt = sp.ThongTinSP;
            ViewBag.soluong = sp.SoLuongSP;
            ViewBag.masp = masp;
            return View(sp);
        }
    }
}