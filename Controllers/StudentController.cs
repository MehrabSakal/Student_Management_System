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

        public IActionResult MyProfile()
        {
            try
            {
                string studentId = User.FindFirstValue("UserId");
                var student = _dbHelper.GetStudent(studentId);
                if (student == null)
                {
                    TempData["ErrorMessage"] = "Could not load profile details.";
                    return RedirectToAction("Dashboard");
                }
                return View(student);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading your profile: " + ex.Message;
                return RedirectToAction("Dashboard");
            }
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            try
            {
                string studentId = User.FindFirstValue("UserId");
                var student = _dbHelper.GetStudent(studentId);
                if (student == null)
                {
                    TempData["ErrorMessage"] = "Could not load profile for editing.";
                    return RedirectToAction("MyProfile");
                }
                return View(student);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading profile: " + ex.Message;
                return RedirectToAction("MyProfile");
            }
        }

        [HttpPost]
        public IActionResult EditProfile(StudentManagementSystem.Models.Student updatedStudent)
        {
            try
            {
                string studentId = User.FindFirstValue("UserId");
                
                // Fetch the existing student to retain uneditable fields
                var existingStudent = _dbHelper.GetStudent(studentId);
                if (existingStudent == null)
                {
                    TempData["ErrorMessage"] = "Student not found.";
                    return RedirectToAction("MyProfile");
                }

                // Update only the allowed fields
                existingStudent.Email = updatedStudent.Email;
                existingStudent.Phone = updatedStudent.Phone;
                existingStudent.Address = updatedStudent.Address;
                
                // If they provided a new password, update it. Otherwise keep old one.
                if (!string.IsNullOrEmpty(updatedStudent.Password))
                {
                    existingStudent.Password = updatedStudent.Password;
                }

                _dbHelper.UpdateStudent(existingStudent);
                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("MyProfile");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error updating profile: " + ex.Message;
                return View(updatedStudent);
            }
        }

        public IActionResult MyResults()
        {
            try
            {
                string studentId = User.FindFirstValue("UserId");
                var results = _dbHelper.GetStudentResults(studentId);
                return View(results);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading your results: " + ex.Message;
                return RedirectToAction("Dashboard");
            }
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

        [HttpGet]
        public IActionResult RequestResearch()
        {
            try
            {
                var teachers = _dbHelper.GetAllTeachers();
                return View(teachers);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading teachers: " + ex.Message;
                return RedirectToAction("Dashboard");
            }
        }

        [HttpPost]
        public IActionResult RequestResearch(StudentManagementSystem.Models.ResearchRequest model)
        {
            try
            {
                string studentId = User.FindFirstValue("UserId");
                _dbHelper.RequestResearch(studentId, model.TeacherId, model.ResearchInterest, model.ResearchDescription, model.PreviousExperience);
                TempData["SuccessMessage"] = "Research request submitted successfully! Awaiting teacher approval.";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-20011"))
                {
                    TempData["ErrorMessage"] = "You already have a pending research request.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error submitting request: " + ex.Message;
                }
            }
            return RedirectToAction("Dashboard");
        }
    }
}
