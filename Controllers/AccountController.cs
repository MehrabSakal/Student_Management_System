using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using System.Collections.Generic;

namespace StudentManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseHelper _dbHelper;

        public AccountController(IConfiguration configuration)
        {
            string connString = configuration.GetConnectionString("OracleDbConnection");
            _dbHelper = new DatabaseHelper(connString);
        }

        public IActionResult Login(string role)
        {
            ViewBag.Role = role ?? "Student";
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> Login(string userId, string password, string role)
        {
            if (role == "Admin" && userId == "admin" && password == "admin123")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userId),
                    new Claim(ClaimTypes.Role, "Admin")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Dashboard", "Admin");
            }
            else if (role == "Student")
            {
                Student student = _dbHelper.GetStudent(userId);
                if (student != null && student.Password == password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, student.FullName),
                        new Claim("UserId", student.StudentId),
                        new Claim(ClaimTypes.Role, "Student")
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Dashboard", "Student");
                }
            }
            else if (role == "Teacher")
            {
                Teacher teacher = _dbHelper.GetTeacher(userId);
                if (teacher != null && teacher.Password == password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, teacher.FirstName + " " + teacher.LastName),
                        new Claim("UserId", teacher.TeacherId),
                        new Claim(ClaimTypes.Role, "Teacher")
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Dashboard", "Teacher");
                }
            }

            ViewBag.Role = role;
            ViewBag.ErrorMessage = "Invalid User ID or Password.";
            return View();
        }
        
        public async System.Threading.Tasks.Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
