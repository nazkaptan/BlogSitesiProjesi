﻿using BlogSitesiProjesi.Models.Data;
using BlogSitesiProjesi.ViewModels.Auth.Login;
using BlogSitesiProjesi.ViewModels.Auth.Register;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BlogSitesiProjesi.Models.Entity;

namespace BlogSitesiProjesi.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login(string yonlen)
        {
            ViewBag.yonlen = yonlen;
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model, string yonlen)
        {
            if (ModelState.IsValid)
            {
                User user = _context.Users.FirstOrDefault(x => x.Email.Equals(model.Email));

                if (user is not null)
                {
                    HttpContext.Session.SetString("userId", user.Id.ToString());
                    HttpContext.Session.SetString("email", user.Email);

                    if (string.IsNullOrEmpty(yonlen)) return RedirectToAction("Index", "Home");
                    else return Redirect(yonlen);
                }
                else ModelState.AddModelError("", "email bulunamadi");
            }
            else ModelState.AddModelError("", "email bulunamadi");
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("email");
            return RedirectToAction("Login");
        }

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                if (!_context.Users.Any(x => x.Email.ToLower().Equals(user.Email.ToLower())))
                {
                    User newUser = new User(user.Email);
                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    TempData["message"] = "Kayit edilmistir";
                    return RedirectToAction("Login");
                }
                else ModelState.AddModelError("", "Bu mail sistemde mevcut");
            }
            return View();

        }
    }
}

