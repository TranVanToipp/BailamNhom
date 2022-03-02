using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Dien_Thoai.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        public BaseController()
        {
            if (System.Web.HttpContext.Current.Session["User"].Equals(""))
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Authen");
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["LTaiKhoan"].Equals("Admin")) {;}
                else
                    System.Web.HttpContext.Current.Response.Redirect("~/Trangchu");
            }
        }
    }
}