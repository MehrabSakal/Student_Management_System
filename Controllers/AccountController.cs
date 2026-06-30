using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string role)
        {
            ViewBag.Role = role ?? "Student";
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userId, string password, string role)
        {
            if (role == "Admin" && userId == "admin" && password == "admin123")
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else if (role == "Student" && userId == "student" && password == "student123")
            {
                return Content("Welcome Student Dashboard! (To be built)");
            }
            else if (role == "Teacher" && userId == "teacher" && password == "teacher123")
            {
                return Content("Welcome Teacher Dashboard! (To be built)");
            }

            ViewBag.Role = role;
            ViewBag.ErrorMessage = "Invalid User ID or Password.";
            return View();
        }
    }
}
