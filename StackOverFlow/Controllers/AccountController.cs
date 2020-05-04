using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StackOverFlow.Data;

namespace StackOverFlow.Controllers
{
    public class AccountController : Controller
    {

        private string _connectionString;
        public AccountController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(User u, string Password)
        {
            QuestionRepository qr = new QuestionRepository(_connectionString);
            qr.AddUser(u, Password);
            return Redirect("/Account/Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            QuestionRepository qr = new QuestionRepository(_connectionString);
            User user = qr.Login(Email, Password);
            if (user == null)
            {
                return Redirect("Account/Login");
            }
            var claims = new List<Claim>
            {
                new Claim("user", user.Email)
            };
            HttpContext.SignInAsync(new ClaimsPrincipal(
            new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();
            return Redirect("/Home/Index");
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/Home/Index");
        }
    }
}