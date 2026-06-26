using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public IActionResult Login(string role)
        {
            // Store the role in ViewBag so the view can use it
            ViewBag.Role = role ?? "Student"; // Default to student if none provided
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string userId, string password, string role)
        {
            // WEEK 1: Simple hardcoded login just to test the pages
            // In WEEK 2, we will replace this with Oracle PL/SQL checks using Oracle.ManagedDataAccess
            
            if (role == "Admin" && userId == "admin" && password == "admin123")
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else if (role == "Student" && userId == "student" && password == "student123")
            {
                // Placeholder for student dashboard
                return Content("Welcome Student Dashboard! (To be built)");
            }
            else if (role == "Teacher" && userId == "teacher" && password == "teacher123")
            {
                // Placeholder for teacher dashboard
                return Content("Welcome Teacher Dashboard! (To be built)");
            }

            ViewBag.Role = role;
            ViewBag.ErrorMessage = "Invalid User ID or Password.";
            return View();
        }
    }
}
