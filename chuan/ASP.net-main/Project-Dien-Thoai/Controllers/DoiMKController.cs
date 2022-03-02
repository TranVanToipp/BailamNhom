using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Dien_Thoai.Models;

namespace Project_Dien_Thoai.Controllers
{
    public class DoiMKController : Controller
    {
        private Project_Dien_ThoaiDBContexxt db = new Project_Dien_ThoaiDBContexxt();
        // GET: DoiMK
        public ActionResult Index()
        {
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        public ActionResult Index(string tenDN,FormCollection field)
        {
            string strError = "";
            string pws = field["pws"];
            string newpws = field["newpws"];
            string rnewpws = field["rnewpws"];
            TaiKhoan tk = db.TaiKhoans.SingleOrDefault(t => t.TenDN == tenDN);
            if(tk.MatKhau == pws)
            {
                if (newpws == rnewpws)
                {
                    tk.MatKhau = newpws;
                    db.SaveChanges();
                    strError = "Đổi Mật Khẩu Thành Công";
                }
                else
                    strError = "Mật Khẩu Không Trùng Khớp";
            }
            else
                strError = "Mật Khẩu Cũ Không Chính Xác";
            ViewBag.Error = strError;
            return View();
        }
    }
}