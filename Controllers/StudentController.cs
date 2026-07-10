using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StudentManagementSystem.Data;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System;

namespace StudentManagementSystem.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly DatabaseHelper _dbHelper;

        public StudentController(IConfiguration configuration)
        {
            string connString = configuration.GetConnectionString("OracleDbConnection");
            _dbHelper = new DatabaseHelper(connString);
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult MyCourses()
        {
            try
            {
                string studentId = User.FindFirstValue("UserId");
                var courses = _dbHelper.GetMyCourses(studentId);
                return View(courses);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading your courses: " + ex.Message;
                return RedirectToAction("Dashboard");
            }
        }

        [HttpGet]
        public IActionResult RegisterCourses()
        {
            try
            {
                var courses = _dbHelper.GetAllCourses();
                return View(courses);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading courses: " + ex.Message;
                return View(new System.Collections.Generic.List<Models.Course>());
            }
        }

        [HttpPost]
        public IActionResult RegisterCourses(string[] selectedCourses)
        {
            string studentId = User.FindFirstValue("UserId");
            
            if (selectedCourses == null || selectedCourses.Length == 0)
            {
                TempData["ErrorMessage"] = "Please select at least one course.";
                return RedirectToAction("RegisterCourses");
            }

            if (selectedCourses.Length > 5)
            {
                TempData["ErrorMessage"] = "You cannot select more than 5 courses at once.";
                return RedirectToAction("RegisterCourses");
            }

            int successCount = 0;
            string errorMsgs = "";

            foreach (var courseNo in selectedCourses)
            {
                try
                {
                    _dbHelper.RegisterCourse(studentId, courseNo);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("ORA-20001"))
                    {
                        errorMsgs += $"Max 5 courses limit reached. Cannot add {courseNo}. ";
                        break; 
                    }
                    else if (ex.Message.Contains("ORA-20002") || ex.Message.Contains("ORA-00001"))
                    {
                        errorMsgs += $"Already registered for {courseNo}. ";
                    }
                    else
                    {
                        errorMsgs += $"Error adding {courseNo}: {ex.Message} ";
                    }
                }
            }

            if (successCount > 0)
            {
                TempData["SuccessMessage"] = $"Successfully registered for {successCount} course(s).";
            }
            if (!string.IsNullOrEmpty(errorMsgs))
            {
                TempData["ErrorMessage"] = errorMsgs;
            }

            return RedirectToAction("Dashboard");
        }

        public IActionResult ViewAssignments()
        {
            try
            {
                string studentId = User.FindFirstValue("UserId");
                var assignments = _dbHelper.GetAssignmentsForStudent(studentId);
                return View(assignments);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading assignments: " + ex.Message;
                return RedirectToAction("Dashboard");
            }
        }
        public IActionResult Library()
        {
            try
            {
                var books = _dbHelper.GetAllBooks();
                return View(books);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error fetching books: " + ex.Message;
                return View(new System.Collections.Generic.List<Models.Book>());
            }
        }

        [HttpPost]
        public IActionResult RequestBook(int bookId)
        {
            try
            {
                string studentId = User.FindFirstValue("UserId");
                _dbHelper.RequestBook(studentId, bookId);
                TempData["SuccessMessage"] = "Book requested successfully! Awaiting admin approval.";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-20007"))
                {
                    TempData["ErrorMessage"] = "This book is currently out of stock.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error requesting book: " + ex.Message;
                }
            }
            return RedirectToAction("Library");
        }

        public IActionResult MyBookRequests()
        {
            try
            {
                string studentId = User.FindFirstValue("UserId");
                var requests = _dbHelper.GetStudentBookRequests(studentId);
                return View(requests);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error fetching your requests: " + ex.Message;
                return View(new System.Collections.Generic.List<Models.BookRequest>());
            }
        }
    }
}
