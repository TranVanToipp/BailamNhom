using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Dien_Thoai.Models;

namespace Project_Dien_Thoai.Controllers
{
    public class AuthenController : Controller
    {
        private Project_Dien_ThoaiDBContexxt db = new Project_Dien_ThoaiDBContexxt();
        // GET: Authen
        public ActionResult Index()
        {    
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection field)
        {
            string strError = "";
            string userName = field["TenDN"];
            string password = field["MatKhau"];
            TaiKhoan taiKhoan = db.TaiKhoans.Where(m => m.TenDN == userName || m.Email == userName).FirstOrDefault();
            if (taiKhoan == null)
            {
                strError = "Tên Đăng Nhập Không Tồn Tại";
            }
            else
                if (taiKhoan.LoaiTaiKhoan.Equals("Admin"))
                {
                    if (taiKhoan.MatKhau == password)
                    {
                        Session["User"] = taiKhoan.TenDN;
                        Session["userName"] = taiKhoan.TenDN;
                        Session["LTaiKhoan"] = taiKhoan.LoaiTaiKhoan;
                        return RedirectToAction("Index", "Trangchu");
                    }
                    else
                        strError = "Mật khẩu không đúng";
                }
            else
                {
                    if (taiKhoan.MatKhau == password)
                    {
                    string tdn = taiKhoan.TenDN;
                    KhachHang khachHang = db.KhachHangs.Where(k => k.TenDN == tdn).FirstOrDefault();
                    Session["userName"] = khachHang.TenKH;
                    Session["User"] = tdn;
                    Session["Ma_User"] = khachHang.MaKH;
                    Session["LTaiKhoan"] = taiKhoan.LoaiTaiKhoan;
                        return RedirectToAction("Index", "Trangchu");
                    }
                    else
                        strError = "Mật Khẩu Không Chính sác";
                }
            ViewBag.Error = strError;
            return View();
        }
        public ActionResult thoat()
        {
            Session["userName"] = "";
            Session["User"] = "";
            Session["LTaiKhoan"] = "";
            Session["Ma_User"] = "";
            return RedirectToAction("Index", "Trangchu");
        }
    }
}