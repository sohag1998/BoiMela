using BoiMela.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoiMela.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            Session["User"] = "";
            ViewBag.Message = "";
            ViewBag.status = "";
            return View();
        }

        public ActionResult Logout()
        {
            Session["User"] = "";
            ViewBag.Message = "";
            ViewBag.status = "";

            return RedirectToAction("Login", "Auth");
        }
        [HttpPost]
        public ActionResult Login(LoginDtos Model)
        {
           if(!ModelState.IsValid) 
           { 
                return View(Model);
           }
           if(ModelState.IsValid)
           {
                if(Model.Username == "sohag" && Model.Password == "123456")
                {
                    Session["User"] = Model.Username;
                    ViewBag.Message = "Success";
                    ViewBag.status = "True";
                }
                else
                {
                    ViewBag.Message = "Wrong username or password!";
                }
           }


           return View();
        }
        public ActionResult Registration()
        {
            return View();
        }

    }
}