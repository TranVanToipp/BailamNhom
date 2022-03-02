using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Project_Dien_Thoai.Models;

namespace Project_Dien_Thoai.Controllers
{
    public class QuenMKController : Controller
    {
        private Project_Dien_ThoaiDBContexxt db = new Project_Dien_ThoaiDBContexxt();
        // GET: QuenMK
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
            TaiKhoan taiKhoan = db.TaiKhoans.Where(m => m.TenDN == userName || m.Email == userName).FirstOrDefault();
            if (taiKhoan == null)
            {
                strError = "Tên Đăng Nhập Không Tồn Tại";
            }
            else
            {
                KhachHang kh = db.KhachHangs.Where(k => k.TenDN == userName || k.EmailKH == userName).FirstOrDefault();
                string maRd = randomMa();
                string fullname = kh.TenKH;
                string email = kh.EmailKH;
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/client/templateQMK/neworder.html"));
                content = content.Replace("{{Name}}", fullname);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{MaXN}}", maRd);
                new MailHelper().SendMail(email, "Mật Khẩu gửi từ nhóm MobileOnline", content);
                taiKhoan.MatKhau = maRd;
                db.SaveChanges();
                strError = "Mật Khẩu Đã Được Gửi Đến Email của bạn, vui lòng kiểm tra email";
            }
            ViewBag.Error = strError;
            return View();
        }
        public string randomMa()
        {
            Random rd = new Random();
            string R;
            R = rd.Next(1111111, 9999999).ToString();
            return R;
        }
    }
}