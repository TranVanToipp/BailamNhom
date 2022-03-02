using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Dien_Thoai.Models;

namespace Project_Dien_Thoai.Controllers
{
    public class OrderController : Controller
    {
        private Project_Dien_ThoaiDBContexxt db = new Project_Dien_ThoaiDBContexxt();
        // GET: Order
        public ActionResult Index(string makh)
        {
            KhachHang kh = db.KhachHangs.Single(k => k.MaKH == makh);
            ViewBag.maKH = makh;
            ViewBag.tenKH = kh.TenKH;
            return View();
        }
        public string rd_MaHD()
        {
            Random rd = new Random();
            string R;
            string mahd;
            do
            {
                R = rd.Next(1, 9999999).ToString();
                mahd = "HD_" + R;
            } while (kt_MaHD(mahd) == true);
            return mahd;
        }
        public bool kt_MaHD(string makh)
        {

            var HD = from hd in db.HoaDons where hd.MaKH.Equals(makh) select hd;
            if (HD.Any())
                return true;
            else
                return false;
        }
        public List<sgiohang> lstgiohang(string maKH)
        {
            var giohang = from gh in db.GioHangs
                          join sp in db.SanPhams
                          on gh.MaSP equals sp.MaSP
                          where gh.MaKH == maKH
                          select new sgiohang
                          {
                              id = gh.Id_GioHang,
                              makh = gh.MaKH,
                              masp = gh.MaSP,
                              tensp = sp.TenSP,
                              hinhsp = sp.HinhSP,
                              soluong = gh.SoLuongHang,
                              dongia = sp.GiaDauRa,
                          };
            return giohang.ToList();
        }
        [HttpPost]
        public ActionResult Index(string makh, FormCollection field)
        {
            string ngaySinh = field["ngaysinh"];
            string gioiTinh = field["rdbsex"];
            string diaChi = field["address"];
            int sdt = int.Parse(field["SDT"]);
            KhachHang kh = db.KhachHangs.Single(k => k.MaKH == makh);
            kh.NgaySinh = DateTime.Parse(ngaySinh);
            kh.GioiTinh = gioiTinh;
            kh.DiaChiKH = diaChi;
            kh.SODTKH = sdt;
            db.SaveChanges();
            HoaDon hoadon = new HoaDon();
            var gh = lstgiohang(makh);
            foreach (var item in gh)
            {
                hoadon.MaHoaDon = rd_MaHD();
                hoadon.Id_GioHang = item.id.ToString();
                hoadon.MaSP = item.masp;
                hoadon.MaKH = item.makh;
                hoadon.SoLuongXuat = item.soluong;
                hoadon.TrangThaiXuat = "Đang Kiểm Duyệt";
                hoadon.NgayHD = DateTime.Now;
                hoadon.ThanhTien = item.thanhtien;
                db.HoaDons.Add(hoadon);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "TTDonHang", new { makh });
        }
    }
}