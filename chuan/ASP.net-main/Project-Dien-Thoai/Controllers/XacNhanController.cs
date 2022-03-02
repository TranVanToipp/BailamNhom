using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Dien_Thoai.Models;

namespace Project_Dien_Thoai.Controllers
{
    public class XacNhanController : Controller
    {
        private Project_Dien_ThoaiDBContexxt db = new Project_Dien_ThoaiDBContexxt();
        // GET: XacNhan
        public ActionResult Index()
        {
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        public ActionResult Index(string TenDN,FormCollection field)
        {
            string strError = "";
            string xacNhan = field["xacnhan"];
            MaXacNhan maXN = db.MaXacNhans.Where(m => m.TenDN == TenDN).FirstOrDefault();
            if(maXN.Maxacnhan == xacNhan)
            {
                string maKH= Session["MaKH"].ToString();
                string fullname = Session["fullname"].ToString();
                string email = Session["email"].ToString();
                string matkhau = Session["matkhau"].ToString();
                string LTaiKhoan = "Khách Hàng";
                TaiKhoan Insert_tk = new TaiKhoan();
                Insert_tk.TenDN = TenDN;
                Insert_tk.Email = email;
                Insert_tk.MatKhau = matkhau;
                Insert_tk.LoaiTaiKhoan = LTaiKhoan;
                db.TaiKhoans.Add(Insert_tk);
                db.SaveChanges();
                string tendn = Insert_tk.TenDN;
                KhachHang Insert_kh = new KhachHang();
                Insert_kh.MaKH = maKH;
                Insert_kh.TenDN = tendn;
                Insert_kh.TenKH = fullname;
                Insert_kh.DiaChiKH = "";
                Insert_kh.GioiTinh = "";
                Insert_kh.EmailKH = email;
                Insert_kh.NgaySinh = DateTime.Now;
                Insert_kh.SODTKH = 0;
                db.KhachHangs.Add(Insert_kh);
                db.SaveChanges();
                Session["User"] = "";
                Session["MaKH"] = "";
                Session["fullname"] = "";
                Session["email"] = "";
                Session["matkhau"] = "";
                db.MaXacNhans.Remove(maXN);
                db.SaveChanges();
                return RedirectToAction("Index", "Authen");
            }
            else
                strError = "Mã xác nhận không đúng";
            ViewBag.Error = strError;
            return View();
        }
    }
}