using MultiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiShop.Areas.admin.Controllers
{
    public class DefaultController : Controller
    {
        private AccountModel _db = new AccountModel();
        // GET: admin/Default
        [HttpGet]
        public ActionResult Login()
        {
            if(Session["admin"]!= null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username,string password)
        {
            var user = username;
            var pass = password;
            var acc = _db.AspNetUsers.SingleOrDefault(x => x.UserName == user && x.PasswordHash == pass);
            if(acc != null)
            {
                //dang nhap thanh cong
                Session["user"] = acc;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //dang nhap that bai
                return View();

            }
        }
    }
}