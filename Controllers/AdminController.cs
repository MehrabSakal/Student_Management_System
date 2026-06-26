using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: /Admin/Dashboard
        public IActionResult Dashboard()
        {
            return View();
        }

        // GET: /Admin/AddStudent
        public IActionResult AddStudent()
        {
            return Content("Add Student Page (To be built next week)");
        }

        // GET: /Admin/UpdateStudent
        public IActionResult UpdateStudent()
        {
            return Content("Update Student Page (To be built next week)");
        }

        // GET: /Admin/DeleteStudent
        public IActionResult DeleteStudent()
        {
            return Content("Delete Student Page (To be built next week)");
        }

        // GET: /Admin/AllStudents
        public IActionResult AllStudents()
        {
            return Content("All Students List (To be built next week)");
        }

        // GET: /Admin/Library
        public IActionResult Library()
        {
            return Content("Library Management (To be built next week)");
        }
    }
}
