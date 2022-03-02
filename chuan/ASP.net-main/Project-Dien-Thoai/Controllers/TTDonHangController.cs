using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Dien_Thoai.Models;

namespace Project_Dien_Thoai.Controllers
{

    public class TTDonHangController : Controller
    {
        private Project_Dien_ThoaiDBContexxt db = new Project_Dien_ThoaiDBContexxt();
        // GET: TTDonHang
        public ActionResult Index(string makh)
        {
            var ttdh = from dh in db.HoaDons where dh.MaKH == makh select dh;
            if (ttdh.Any())
            {
                var tthoadon = lstdonhang(makh);
                KhachHang kh = db.KhachHangs.Single(k => k.MaKH == makh);
                ViewBag.sdt = kh.SODTKH;
                ViewBag.diachi = kh.DiaChiKH;
                return View(tthoadon);
            }
            return RedirectToAction("Index", "Trangchu");
        }

        public List<TTDonHang> lstdonhang(string makh)
        {
            var lstdon = from d in db.HoaDons
                         join g in db.SanPhams
                         on d.MaSP equals g.MaSP
                         join k in db.KhachHangs
                         on d.MaKH equals k.MaKH
                         where d.MaKH == makh
                         select new TTDonHang
                         {
                             makh = d.MaKH,
                             masp = g.MaSP,
                             TenSP = g.TenSP,
                             soluong = d.SoLuongXuat,
                             SDT = k.SODTKH,
                             diachi = k.DiaChiKH,
                             trangthai = d.TrangThaiXuat,
                             thanhtien = d.ThanhTien,
                         };
            return lstdon.ToList();
        }
    }
}