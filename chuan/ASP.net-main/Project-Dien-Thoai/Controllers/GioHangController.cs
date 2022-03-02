using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Dien_Thoai.Models;

namespace Project_Dien_Thoai.Controllers
{
    public class GioHangController : Controller
    {
        private Project_Dien_ThoaiDBContexxt db = new Project_Dien_ThoaiDBContexxt();
        // GET: GioHang
        public ActionResult Index(string makh)
        {
            var gh = from g in db.GioHangs.Where(kh => kh.MaKH == makh) select g;
           
            if (gh.Any())
            {
                var gioHang = lstgiohang(makh);
                ViewBag.Makh = makh;
                return View(gioHang);
            }
            
            return RedirectToAction("Index", "Trangchu");
        }
        public List<sgiohang> lstgiohang(string maKH)
        {
            var giohang = from gh in db.GioHangs
                          join sp in db.SanPhams
                          on gh.MaSP equals sp.MaSP
                          where gh.MaKH == maKH
                          select new sgiohang
                          {
                              makh = gh.MaKH,
                              masp = gh.MaSP,
                              tensp = sp.TenSP,
                              hinhsp = sp.HinhSP,
                              soluong = gh.SoLuongHang,
                              dongia = sp.GiaDauRa,
                          };
            return giohang.ToList();
        }
        public ActionResult add_Cart(string makh, string masp, string strURL)
        {
            //kiem tra xem nguoi dung get dung masp hay chua
            SanPham sp = db.SanPhams.SingleOrDefault(s => s.MaSP == masp);
            if (sp == null)
            {
                //Response.StatusCode = 404;
                return null;
            }
            //kiem tra trong gio hang cua khach hang da ton tai sp nay chua
            GioHang gh = db.GioHangs.SingleOrDefault(g => g.MaKH == makh && g.MaSP == masp);
            if (gh == null)
            {
                GioHang gioHang = new GioHang();
                gioHang.MaKH = makh;
                gioHang.MaSP = masp;
                gioHang.SoLuongHang = 1;
                db.GioHangs.Add(gioHang);
                db.SaveChanges();
                sp.SoLuongSP -= 1;
                db.SaveChanges();
                return Redirect(strURL);
            }
            else
            {
                gh.SoLuongHang++;
                sp.SoLuongSP--;
                db.SaveChanges();
                return Redirect(strURL);
            }
        }
        [HttpPost]
        public ActionResult update_Cart(string makh, string masp, FormCollection f)
        {
            //kiem tra xem nguoi dung get dung masp hay chua
            SanPham sp = db.SanPhams.SingleOrDefault(s => s.MaSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            GioHang gh = db.GioHangs.SingleOrDefault(g => g.MaKH == makh && g.MaSP == masp);
            if (gh != null)
            {
                sp.SoLuongSP += gh.SoLuongHang;
                gh.SoLuongHang = int.Parse(f["txtsoluong"].ToString());
                sp.SoLuongSP -= gh.SoLuongHang;
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { makh, masp });
        }

        public ActionResult delete_Cart(string makh, string masp)
        {
            //kiem tra xem nguoi dung get dung masp hay chua
            SanPham sp = db.SanPhams.SingleOrDefault(s => s.MaSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //kiem tra trong gio hang cua khach hang da ton tai sp nay chua
            GioHang gh = db.GioHangs.SingleOrDefault(g => g.MaKH == makh && g.MaSP == masp);
            if (gh != null)
            {
                sp.SoLuongSP += gh.SoLuongHang;
                db.GioHangs.Remove(gh);
                db.SaveChanges();
            }
            if (db.GioHangs.Count(s => s.MaKH == makh) == 0)
            {
                return RedirectToAction("Index", "Trangchu");
            }
            return RedirectToAction("Index", new { makh, masp });
        }
        private int sumSL(string makh)
        {
            int sum = 0;
            var gh = from g in db.GioHangs.Where(kh => kh.MaKH == makh) select g;
            if (gh.Any())
            {
                sum = gh.Sum(s => s.SoLuongHang);
            }
            return sum;
        }

        public ActionResult GioHangPartial(string makh)
        {
            if (sumSL(makh) == 0)
            {
                return PartialView();
            }
            ViewBag.SumSL = sumSL(makh);
            return PartialView();
        }

        public ActionResult DatHang(string makh)
        {
            return RedirectToAction("Index", "Order", new { makh });
        }
    }
}