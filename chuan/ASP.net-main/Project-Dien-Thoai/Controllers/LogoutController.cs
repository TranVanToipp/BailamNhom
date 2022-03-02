using Project_Dien_Thoai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace Project_Dien_Thoai.Controllers
{
    public class LogoutController : Controller
    {
        private Project_Dien_ThoaiDBContexxt db = new Project_Dien_ThoaiDBContexxt();
        // GET: Logout
        public ActionResult Index()
        {
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection field)
        {
            string strError = "";
            string maRd = randomMa();
            string MaKH = rd_MaKH();
            string userName = field["TenDN"];
            string fullname = field["fullname"];
            string email = field["email"];
            string matkhau = field["MatKhau"];
            string con_matkhau = field["confirmation_pwd"];
            if (kt_TenDN(userName,email)==false)
            {
                if (matkhau == con_matkhau)
                {
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/client/template/neworder.html"));

                    content = content.Replace("{{Name}}", fullname);
                    content = content.Replace("{{Email}}", email);
                    content = content.Replace("{{MaXN}}", maRd);
                    new MailHelper().SendMail(email, "Mã xác nhận từ nhóm MobileOnline", content);
                    Session["MaKH"] = MaKH;
                    Session["User"] = userName;
                    Session["fullname"] = fullname;
                    Session["email"] = email;
                    Session["matkhau"] = matkhau;
                    MaXacNhan maxn = new MaXacNhan();
                    maxn.TenDN = userName;
                    maxn.Maxacnhan = maRd;
                    db.MaXacNhans.Add(maxn);
                    db.SaveChanges();
                    return RedirectToAction("Index", "XacNhan");
                }
                else
                    strError = "Mật Khẩu Không Khớp, Vui Lòng Nhập lại Mật Khẩu";
            }
            else
                strError = "Tên Đăng Nhập hoặc Email đã tồn tại, vui lòng kiểm tra lại";
            ViewBag.Error = strError;
            return View();
        }
        public string rd_MaKH()
        {
            Random rd = new Random();
            string R;
            string makh;
            do
            {
                R = rd.Next(1, 9999999).ToString();
                makh = "KH_" + R;
            } while (kt_MaKH(makh) == true);
            return makh;
        }
        public bool kt_MaKH(string makh)
        {
            var kh = from KH in db.KhachHangs where KH.MaKH.Equals(makh) select KH;
            if (kh.Any())
                return true;
            else
                return false; 
        }
        public bool kt_TenDN(string tendn, string email)
        {
            var tk = from TK in db.TaiKhoans where TK.TenDN.Equals(tendn) || TK.Email.Equals(email) select TK;
            if (tk.Any())
                return true;
            else
                return false;
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