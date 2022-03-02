using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_Dien_Thoai.Models;

namespace Project_Dien_Thoai.Areas.Admin.Controllers
{
    public class HoaDonController : BaseController
    {
        private Project_Dien_ThoaiDBContexxt db = new Project_Dien_ThoaiDBContexxt();

        // GET: Admin/HoaDon
        public ActionResult Index(string hoadon)
        {
            var hd = from HD in db.HoaDons.Where(a => a.TrangThaiXuat == hoadon) select HD;
            if (hoadon == "")
            {
                return View(db.HoaDons.ToList());
            }
            else
            {
                return View(hd);
            }
        }
        public ActionResult Duyet_Don(string mahd)
        {
            string tc = "Thành Công";
            HoaDon hoaDon = db.HoaDons.Where(hd => hd.MaHoaDon == mahd).FirstOrDefault();
            if(hoaDon.TrangThaiXuat.Equals("Đang Kiểm Duyệt"))
            {
                hoaDon.TrangThaiXuat = tc;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Huy_Don(string mahd,string masp)
        {
            string tc = "Đơn Đã Hủy";
            HoaDon hoaDon = db.HoaDons.Where(hd => hd.MaHoaDon == mahd).FirstOrDefault();
            SanPham sp = db.SanPhams.Where(s => s.MaSP == masp).FirstOrDefault();
            if (hoaDon.TrangThaiXuat.Equals("Đang Kiểm Duyệt"))
            {
                hoaDon.TrangThaiXuat = tc;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        // GET: Admin/HoaDon/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
