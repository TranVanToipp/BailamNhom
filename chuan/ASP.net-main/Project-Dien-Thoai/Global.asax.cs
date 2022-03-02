using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project_Dien_Thoai
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_Start()
        {
            Session["User"] = "";
            Session["LTaiKhoan"] = "";
            Session["userName"] = "";
            Session["Ma_User"] = "";
            Session["MaKH"] = "";
            Session["fullname"] = "";
            Session["email"] = "";
            Session["matkhau"] = "";

        }
    }
}
