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
            try
            {
                ViewBag.TotalStudents = _dbHelper.GetTotalStudents();
                ViewBag.TotalTeachers = _dbHelper.GetTotalTeachers();
            }
            catch (Exception ex)
            {
                ViewBag.TotalStudents = 0;
                ViewBag.TotalTeachers = 0;
                ViewBag.ErrorMessage = "Could not load dashboard statistics: " + ex.Message;
            }
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
                TempData["SuccessMessage"] = "Student added successfully!";
                return RedirectToAction("AllStudents");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error adding student: " + ex.Message;
                return View(student);
            }
        }

       
        [HttpGet]
        public IActionResult AddTeacher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTeacher(Teacher teacher)
        {
            try
            {
                _dbHelper.AddTeacher(teacher);
                TempData["SuccessMessage"] = "Teacher added successfully using PL/SQL!";
                return RedirectToAction("Dashboard");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-00001"))
                {
                    ViewBag.ErrorMessage = "A teacher with this ID already exists. Please use a different ID.";
                }
                else
                {
                    ViewBag.ErrorMessage = "Error adding teacher: " + ex.Message;
                }
                return View(teacher);
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

        [HttpGet]
        public IActionResult UpdateTeacher(string id)
        {
            try
            {
                Teacher teacher = _dbHelper.GetTeacher(id);
                if (teacher == null)
                {
                    return NotFound();
                }
                return View(teacher);
            }
            catch (Exception ex)
            {
                return Content("Error loading teacher: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult UpdateTeacher(Teacher teacher)
        {
            try
            {
                _dbHelper.UpdateTeacher(teacher);
                TempData["SuccessMessage"] = "Teacher updated successfully!";
                return RedirectToAction("AllTeachers");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error updating teacher: " + ex.Message;
                return View(teacher);
            }
        }

        [HttpGet]
        public IActionResult DeleteTeacher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteTeacher(string id)
        {
            try
            {
                _dbHelper.DeleteTeacher(id);
                TempData["SuccessMessage"] = "Teacher deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting teacher: " + ex.Message;
            }
            return RedirectToAction("AllTeachers");
        }

        [HttpGet]
        public IActionResult AllTeachers()
        {
            try
            {
                var teachers = _dbHelper.GetAllTeachers();
                return View(teachers);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Database not connected or error fetching teachers: " + ex.Message;
                return View(new System.Collections.Generic.List<Teacher>());
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
                ViewBag.ErrorMessage = "Error fetching books: " + ex.Message;
                return View(new System.Collections.Generic.List<Book>());
            }
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            try
            {
                _dbHelper.AddBook(book);
                TempData["SuccessMessage"] = "Book added successfully!";
                return RedirectToAction("Library");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error adding book: " + ex.Message;
                return View(book);
            }
        }

        public IActionResult PendingRequests()
        {
            try
            {
                var requests = _dbHelper.GetAllPendingBookRequests();
                return View(requests);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error fetching requests: " + ex.Message;
                return View(new System.Collections.Generic.List<BookRequest>());
            }
        }

        [HttpPost]
        public IActionResult ApproveRequest(int requestId)
        {
            try
            {
                _dbHelper.ApproveBookRequest(requestId);
                TempData["SuccessMessage"] = "Request approved successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error approving request: " + ex.Message;
            }
            return RedirectToAction("PendingRequests");
        }

        [HttpPost]
        public IActionResult RejectRequest(int requestId)
        {
            try
            {
                _dbHelper.RejectBookRequest(requestId);
                TempData["SuccessMessage"] = "Request rejected successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error rejecting request: " + ex.Message;
            }
            return RedirectToAction("PendingRequests");
        }

        [HttpPost]
        public IActionResult ReturnBook(int requestId)
        {
            try
            {
                _dbHelper.ReturnBook(requestId);
                TempData["SuccessMessage"] = "Book returned successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error returning book: " + ex.Message;
            }
            return RedirectToAction("Library");
        }

        [HttpGet]
        public IActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            try
            {
                _dbHelper.AddDepartment(department);
                TempData["SuccessMessage"] = "Department added successfully!";
                return RedirectToAction("Dashboard");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-00001"))
                {
                    ViewBag.ErrorMessage = "A department with this ID already exists. Please use a different ID.";
                }
                else
                {
                    ViewBag.ErrorMessage = "Error adding department: " + ex.Message;
                }
                return View(department);
            }
        }

        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            try
            {
                _dbHelper.AddCourse(course);
                TempData["SuccessMessage"] = "Course added successfully!";
                return RedirectToAction("Dashboard");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-20003") || ex.Message.Contains("ORA-00001"))
                {
                    ViewBag.ErrorMessage = "A course with this NO already exists.";
                }
                else
                {
                    ViewBag.ErrorMessage = "Error adding course: " + ex.Message;
                }
                return View(course);
            }
        }

        [HttpGet]
        public IActionResult AllDepartments()
        {
            try
            {
                var departments = _dbHelper.GetAllDepartments();
                return View(departments);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error fetching departments: " + ex.Message;
                return View(new System.Collections.Generic.List<Department>());
            }
        }

        [HttpGet]
        public IActionResult AllCourses()
        {
            try
            {
                var courses = _dbHelper.GetAllCourses();
                return View(courses);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error fetching courses: " + ex.Message;
                return View(new System.Collections.Generic.List<Course>());
            }
        }
    }
}

