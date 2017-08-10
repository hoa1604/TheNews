using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheNewsWebsite.Models.TheNewsWebsite;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace TheNewsWebsite.Controllers
{
    public class LoginAdminController : Controller
    {
        private readonly TheNewsContext _context;
        public LoginAdminController(TheNewsContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public int IdAdmin;
        public string NameAdmin;
        [HttpPost]
        public async Task<IActionResult> Login(string Username, string Password)
        {
            try
            {
                Admin admin = await _context.Admins.AsNoTracking().FirstOrDefaultAsync(a => a.Username.Equals(Username));
                if (admin.Password.Equals(Password))
                {
                    HttpContext.Session.SetInt32("Admin", admin.Id);
                    HttpContext.Session.SetString("AdminName", admin.Name);
                    HttpContext.Session.SetInt32("Authority", admin.Authority);
                    string fullName = admin.Name.ToString();
                    string[] name = fullName.Split(' ');
                    HttpContext.Session.SetString("LastName", name[name.Length - 1]);
                    return RedirectToAction("Index", "PostsAdmin");
                }
                else
                {
                    return View("Login");
                    ViewBag.Login = "Mật khẩu không chính xác!";
                }
            }
            catch (Exception)
            {
                ViewBag.Login = "Tên đăng nhập không tồn tại!";
            }
            return View();

        }
        public IActionResult LogOut()
        {
            // HttpContext.GetOwinContext().Authentication.SignOut();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "LoginAdmin");
        }

    }
}