using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PurchaseOrderSystem.Data;
using PurchaseOrderSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly Context _context;

        public LoginController(ILogger<LoginController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public string Login(string username, string pwd)
        {
            string dcount = string.Empty;

            var _checkcredentials = _context.Login.Where(x => x.UserName == username && x.Password == pwd).ToList();
            if (_checkcredentials.Count > 0)
            {
                //return RedirectToAction("Index", "Home");
                this.HttpContext.Session.SetString("uname", (username));
                this.HttpContext.Session.SetString("pwd", pwd);
                TempData["a"] = username;
                dcount = "1";
            }
            else
            {
                dcount = "-1";
            }

            string JSONresult = dcount;
            return JSONresult;
        }
        [HttpPost]
        public string Register(string username, string pwd)
        {
            Login oid = new Login();
            //List<Login> _oid = new List<Login>();
            oid.UserName = username;
            oid.Password = pwd;
            oid.LoggedOn = DateTime.Now; ;
            _context.Login.Add(oid);
            _context.SaveChanges();
            string JSONresult = JsonConvert.SerializeObject("3");
            return JSONresult;
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }
    }

}
