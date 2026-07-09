using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System;
using System.Linq;

namespace StudentManagementSystem.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        private readonly DatabaseHelper _dbHelper;

        public TeacherController(IConfiguration configuration)
        {
            string connString = configuration.GetConnectionString("OracleDbConnection");
            _dbHelper = new DatabaseHelper(connString);
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GiveAssignment()
        {
            try
            {
                string teacherId = User.FindFirstValue("UserId");
                var teacher = _dbHelper.GetTeacher(teacherId);
                if (teacher != null && !string.IsNullOrEmpty(teacher.DeptId))
                {
                    var courses = _dbHelper.GetCoursesByDepartment(teacher.DeptId);
                    ViewBag.Courses = courses;
                }
                else
                {
                    ViewBag.Courses = new System.Collections.Generic.List<Course>();
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading courses: " + ex.Message;
                return RedirectToAction("Dashboard");
            }
        }

        [HttpPost]
        public IActionResult GiveAssignment(Assignment model)
        {
            try
            {
                model.TeacherId = User.FindFirstValue("UserId");
                
                _dbHelper.AddAssignment(model);
                TempData["SuccessMessage"] = "Assignment added successfully!";
                return RedirectToAction("Dashboard");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-20004"))
                {
                    TempData["ErrorMessage"] = "Theory classes can only have Assignments, not Projects.";
                }
                else if (ex.Message.Contains("ORA-20005"))
                {
                    TempData["ErrorMessage"] = "You can only give assignments for courses in your department.";
                }
                else
                {
                    TempData["ErrorMessage"] = "An error occurred while adding the assignment: " + ex.Message;
                }
                return RedirectToAction("GiveAssignment");
            }
        }

        public IActionResult ViewAssignments()
        {
            try
            {
                string teacherId = User.FindFirstValue("UserId");
                var assignments = _dbHelper.GetAssignmentsForTeacher(teacherId);
                return View(assignments);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading assignments: " + ex.Message;
                return RedirectToAction("Dashboard");
            }
        }
    }
}
