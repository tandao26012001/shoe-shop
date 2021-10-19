using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiShop.Areas.admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: admin/Home
        public ActionResult Index()
        {
            if(Session["user"] == null)
            {
                return RedirectToAction("Login", "Default");
            }
            return View();
        }
    }
}