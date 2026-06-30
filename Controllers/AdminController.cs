using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly DatabaseHelper _dbHelper;

        public AdminController(IConfiguration configuration)
        {
            
            string connString = configuration.GetConnectionString("OracleDbConnection");
            _dbHelper = new DatabaseHelper(connString);
        }

   
        public IActionResult Dashboard()
        {
            return View();
        }

      
        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            try
            {
                _dbHelper.AddStudent(student);
                TempData["SuccessMessage"] = "Student added successfully using PL/SQL!";
                return RedirectToAction("AllStudents");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error adding student: " + ex.Message;
                return View(student);
            }
        }

       
        [HttpGet]
        public IActionResult UpdateStudent(string id)
        {
            try
            {
                Student student = _dbHelper.GetStudent(id);
                if (student == null)
                {
                    return NotFound();
                }
                return View(student);
            }
            catch (Exception ex)
            {
                return Content("Error loading student: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult UpdateStudent(Student student)
        {
            try
            {
               
                _dbHelper.UpdateStudent(student);
                TempData["SuccessMessage"] = "Student updated successfully!";
                return RedirectToAction("AllStudents");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error updating student: " + ex.Message;
                return View(student);
            }
        }

        
        [HttpGet]
        public IActionResult DeleteStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteStudent(string id)
        {
            try
            {
                _dbHelper.DeleteStudent(id);
                TempData["SuccessMessage"] = "Student deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting student: " + ex.Message;
            }
            return RedirectToAction("AllStudents");
        }

       
        [HttpGet]
        public IActionResult AllStudents()
        {
            try
            {
                var students = _dbHelper.GetAllStudents();
                return View(students);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Database not connected. Please run Setup.sql and configure appsettings.json. Here is dummy data: " + ex.Message;
                return View(new System.Collections.Generic.List<Student>());
            }
        }

        public IActionResult Library()
        {
            return Content("Library Management (To be built next week)");
        }
    }
}

