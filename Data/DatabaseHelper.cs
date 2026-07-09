using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Student GetStudent(string studentId)
        {
            Student student = null;

            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT student_id, full_name, email, phone, dept_id, address, advisor_id, password FROM students WHERE student_id = :id";
                    cmd.BindByName = true;
                    cmd.Parameters.Add("id", OracleDbType.Varchar2).Value = studentId;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            student = new Student
                            {
                                StudentId = reader["student_id"].ToString(),
                                FullName = reader["full_name"].ToString(),
                                Email = reader["email"].ToString(),
                                Phone = reader["phone"].ToString(),
                                DeptId = reader["dept_id"].ToString(),
                                Address = reader["address"].ToString(),
                                AdvisorId = reader["advisor_id"].ToString(),
                                Password = reader["password"].ToString()
                            };
                        }
                    }
                }
            }

            return student;
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT student_id, full_name, email, phone, dept_id, address, advisor_id, password FROM students";

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                StudentId = reader["student_id"].ToString(),
                                FullName = reader["full_name"].ToString(),
                                Email = reader["email"].ToString(),
                                Phone = reader["phone"].ToString(),
                                DeptId = reader["dept_id"].ToString(),
                                Address = reader["address"].ToString(),
                                AdvisorId = reader["advisor_id"].ToString(),
                                Password = reader["password"].ToString()
                            });
                        }
                    }
                }
            }

            return students;
        }

        public List<Teacher> GetAllTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();

            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT teacher_id, first_name, last_name, email, designation, dept_id, password FROM teachers";

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            teachers.Add(new Teacher
                            {
                                TeacherId = reader["teacher_id"].ToString(),
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString(),
                                Email = reader["email"].ToString(),
                                Designation = reader["designation"].ToString(),
                                DeptId = reader["dept_id"].ToString(),
                                Password = reader["password"].ToString()
                            });
                        }
                    }
                }
            }

            return teachers;
        }

        public Teacher GetTeacher(string teacherId)
        {
            Teacher teacher = null;

            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT teacher_id, first_name, last_name, email, designation, dept_id, password FROM teachers WHERE teacher_id = :id";
                    cmd.BindByName = true;
                    cmd.Parameters.Add("id", OracleDbType.Varchar2).Value = teacherId;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            teacher = new Teacher
                            {
                                TeacherId = reader["teacher_id"].ToString(),
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString(),
                                Email = reader["email"].ToString(),
                                Designation = reader["designation"].ToString(),
                                DeptId = reader["dept_id"].ToString(),
                                Password = reader["password"].ToString()
                            };
                        }
                    }
                }
            }

            return teacher;
        }

        public void AddTeacher(Teacher teacher)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "add_teacher";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("p_teacher_id", OracleDbType.Varchar2).Value = (object)teacher.TeacherId ?? DBNull.Value;
                    cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = (object)teacher.FirstName ?? DBNull.Value;
                    cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = (object)teacher.LastName ?? DBNull.Value;
                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = (object)teacher.Email ?? DBNull.Value;
                    cmd.Parameters.Add("p_designation", OracleDbType.Varchar2).Value = (object)teacher.Designation ?? DBNull.Value;
                    cmd.Parameters.Add("p_dept_id", OracleDbType.Varchar2).Value = (object)teacher.DeptId ?? DBNull.Value;
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = (object)teacher.Password ?? DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddStudent(Student student)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "add_student";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("p_student_id", OracleDbType.Varchar2).Value = (object)student.StudentId ?? DBNull.Value;
                    cmd.Parameters.Add("p_full_name", OracleDbType.Varchar2).Value = (object)student.FullName ?? DBNull.Value;
                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = (object)student.Email ?? DBNull.Value;
                    cmd.Parameters.Add("p_phone", OracleDbType.Varchar2).Value = (object)student.Phone ?? DBNull.Value;
                    cmd.Parameters.Add("p_dept_id", OracleDbType.Varchar2).Value = (object)student.DeptId ?? DBNull.Value;
                    cmd.Parameters.Add("p_address", OracleDbType.Varchar2).Value = (object)student.Address ?? DBNull.Value;
                    cmd.Parameters.Add("p_advisor_id", OracleDbType.Varchar2).Value = (object)student.AdvisorId ?? DBNull.Value;
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = (object)student.Password ?? DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStudent(Student student)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "update_student";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("p_student_id", OracleDbType.Varchar2).Value = (object)student.StudentId ?? DBNull.Value;
                    cmd.Parameters.Add("p_full_name", OracleDbType.Varchar2).Value = (object)student.FullName ?? DBNull.Value;
                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = (object)student.Email ?? DBNull.Value;
                    cmd.Parameters.Add("p_phone", OracleDbType.Varchar2).Value = (object)student.Phone ?? DBNull.Value;
                    cmd.Parameters.Add("p_dept_id", OracleDbType.Varchar2).Value = (object)student.DeptId ?? DBNull.Value;
                    cmd.Parameters.Add("p_address", OracleDbType.Varchar2).Value = (object)student.Address ?? DBNull.Value;
                    cmd.Parameters.Add("p_advisor_id", OracleDbType.Varchar2).Value = (object)student.AdvisorId ?? DBNull.Value;
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = (object)student.Password ?? DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStudent(string studentId)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "delete_student";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("p_student_id", OracleDbType.Varchar2).Value = studentId;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateTeacher(Teacher teacher)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "update_teacher";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("p_teacher_id", OracleDbType.Varchar2).Value = (object)teacher.TeacherId ?? DBNull.Value;
                    cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = (object)teacher.FirstName ?? DBNull.Value;
                    cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = (object)teacher.LastName ?? DBNull.Value;
                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = (object)teacher.Email ?? DBNull.Value;
                    cmd.Parameters.Add("p_designation", OracleDbType.Varchar2).Value = (object)teacher.Designation ?? DBNull.Value;
                    cmd.Parameters.Add("p_dept_id", OracleDbType.Varchar2).Value = (object)teacher.DeptId ?? DBNull.Value;
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = (object)teacher.Password ?? DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTeacher(string teacherId)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "delete_teacher";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("p_teacher_id", OracleDbType.Varchar2).Value = teacherId;

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public int GetTotalStudents()
        {
            int count = 0;
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM students";
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        count = System.Convert.ToInt32(result);
                    }
                }
            }
            return count;
        }

        public int GetTotalTeachers()
        {
            int count = 0;
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM teachers";
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        count = System.Convert.ToInt32(result);
                    }
                }
            }
            return count;
        }

        public void AddDepartment(Department department)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "add_department";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("p_dept_id", OracleDbType.Varchar2).Value = (object)department.DeptId ?? DBNull.Value;
                    cmd.Parameters.Add("p_dept_name", OracleDbType.Varchar2).Value = (object)department.DeptName ?? DBNull.Value;
                    cmd.Parameters.Add("p_faculty", OracleDbType.Varchar2).Value = (object)department.Faculty ?? DBNull.Value;
                    cmd.Parameters.Add("p_no_of_students", OracleDbType.Int32).Value = department.NoOfStudents;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddCourse(Course course)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "add_course";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("p_course_no", OracleDbType.Varchar2).Value = (object)course.CourseNo ?? DBNull.Value;
                    cmd.Parameters.Add("p_course_name", OracleDbType.Varchar2).Value = (object)course.CourseName ?? DBNull.Value;
                    cmd.Parameters.Add("p_dept_id", OracleDbType.Varchar2).Value = (object)course.DeptId ?? DBNull.Value;
                    cmd.Parameters.Add("p_course_type", OracleDbType.Varchar2).Value = (object)course.CourseType ?? DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Department> GetAllDepartments()
        {
            List<Department> departments = new List<Department>();
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT dept_id, dept_name, faculty, no_of_students FROM department";
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            departments.Add(new Department
                            {
                                DeptId = reader["dept_id"].ToString(),
                                DeptName = reader["dept_name"].ToString(),
                                Faculty = reader["faculty"].ToString(),
                                NoOfStudents = reader["no_of_students"] != DBNull.Value ? Convert.ToInt32(reader["no_of_students"]) : 0
                            });
                        }
                    }
                }
            }
            return departments;
        }

        public List<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT course_no, course_name, dept_id, course_type FROM courses";
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(new Course
                            {
                                CourseNo = reader["course_no"].ToString(),
                                CourseName = reader["course_name"].ToString(),
                                DeptId = reader["dept_id"].ToString(),
                                CourseType = reader["course_type"].ToString()
                            });
                        }
                    }
                }
            }
            return courses;
        }

        public void RegisterCourse(string studentId, string courseNo)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "register_course";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("p_student_id", OracleDbType.Varchar2).Value = studentId;
                    cmd.Parameters.Add("p_course_no", OracleDbType.Varchar2).Value = courseNo;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Course> GetCoursesByDepartment(string deptId)
        {
            List<Course> courses = new List<Course>();
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT course_no, course_name, dept_id, course_type FROM courses WHERE dept_id = :dept_id";
                    cmd.BindByName = true;
                    cmd.Parameters.Add("dept_id", OracleDbType.Varchar2).Value = deptId;
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(new Course
                            {
                                CourseNo = reader["course_no"].ToString(),
                                CourseName = reader["course_name"].ToString(),
                                DeptId = reader["dept_id"].ToString(),
                                CourseType = reader["course_type"].ToString()
                            });
                        }
                    }
                }
            }
            return courses;
        }

        public void AddAssignment(Assignment assignment)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "add_assignment";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("p_course_no", OracleDbType.Varchar2).Value = (object)assignment.CourseNo ?? DBNull.Value;
                    cmd.Parameters.Add("p_teacher_id", OracleDbType.Varchar2).Value = (object)assignment.TeacherId ?? DBNull.Value;
                    cmd.Parameters.Add("p_title", OracleDbType.Varchar2).Value = (object)assignment.Title ?? DBNull.Value;
                    cmd.Parameters.Add("p_description", OracleDbType.Varchar2).Value = (object)assignment.Description ?? DBNull.Value;
                    cmd.Parameters.Add("p_assignment_type", OracleDbType.Varchar2).Value = (object)assignment.AssignmentType ?? DBNull.Value;
                    cmd.Parameters.Add("p_due_date", OracleDbType.Date).Value = (object)assignment.DueDate ?? DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Assignment> GetAssignmentsForTeacher(string teacherId)
        {
            List<Assignment> assignments = new List<Assignment>();
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT assignment_id, course_no, teacher_id, title, description, assignment_type, due_date FROM assignments WHERE teacher_id = :teacher_id";
                    cmd.BindByName = true;
                    cmd.Parameters.Add("teacher_id", OracleDbType.Varchar2).Value = teacherId;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            assignments.Add(new Assignment
                            {
                                AssignmentId = Convert.ToInt32(reader["assignment_id"]),
                                CourseNo = reader["course_no"].ToString(),
                                TeacherId = reader["teacher_id"].ToString(),
                                Title = reader["title"].ToString(),
                                Description = reader["description"].ToString(),
                                AssignmentType = reader["assignment_type"].ToString(),
                                DueDate = reader["due_date"] != DBNull.Value ? Convert.ToDateTime(reader["due_date"]) : (DateTime?)null
                            });
                        }
                    }
                }
            }
            return assignments;
        }

        public List<Assignment> GetAssignmentsForStudent(string studentId)
        {
            List<Assignment> assignments = new List<Assignment>();
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT a.assignment_id, a.course_no, a.teacher_id, a.title, a.description, a.assignment_type, a.due_date
                        FROM assignments a
                        INNER JOIN student_courses sc ON a.course_no = sc.course_no
                        WHERE sc.student_id = :student_id";
                    cmd.BindByName = true;
                    cmd.Parameters.Add("student_id", OracleDbType.Varchar2).Value = studentId;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            assignments.Add(new Assignment
                            {
                                AssignmentId = Convert.ToInt32(reader["assignment_id"]),
                                CourseNo = reader["course_no"].ToString(),
                                TeacherId = reader["teacher_id"].ToString(),
                                Title = reader["title"].ToString(),
                                Description = reader["description"].ToString(),
                                AssignmentType = reader["assignment_type"].ToString(),
                                DueDate = reader["due_date"] != DBNull.Value ? Convert.ToDateTime(reader["due_date"]) : (DateTime?)null
                            });
                        }
                    }
                }
            }
            return assignments;
        }

        public void AddBook(Book book)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "add_book";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("p_title", OracleDbType.Varchar2).Value = book.Title;
                    cmd.Parameters.Add("p_author", OracleDbType.Varchar2).Value = book.Author;
                    cmd.Parameters.Add("p_copies", OracleDbType.Int32).Value = book.TotalCopies;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT book_id, title, author, total_copies, available_copies FROM books";
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(new Book
                            {
                                BookId = Convert.ToInt32(reader["book_id"]),
                                Title = reader["title"].ToString(),
                                Author = reader["author"].ToString(),
                                TotalCopies = Convert.ToInt32(reader["total_copies"]),
                                AvailableCopies = Convert.ToInt32(reader["available_copies"])
                            });
                        }
                    }
                }
            }
            return books;
        }

        public void RequestBook(string studentId, int bookId)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "request_book";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;
                    cmd.Parameters.Add("p_student_id", OracleDbType.Varchar2).Value = studentId;
                    cmd.Parameters.Add("p_book_id", OracleDbType.Int32).Value = bookId;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<BookRequest> GetAllPendingBookRequests()
        {
            List<BookRequest> requests = new List<BookRequest>();
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT br.request_id, br.student_id, s.full_name as student_name, 
                               br.book_id, b.title as book_title, br.request_date, br.status
                        FROM book_requests br
                        JOIN students s ON br.student_id = s.student_id
                        JOIN books b ON br.book_id = b.book_id
                        WHERE br.status = 'Pending'
                        ORDER BY br.request_date ASC";
                    
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            requests.Add(new BookRequest
                            {
                                RequestId = Convert.ToInt32(reader["request_id"]),
                                StudentId = reader["student_id"].ToString(),
                                StudentName = reader["student_name"].ToString(),
                                BookId = Convert.ToInt32(reader["book_id"]),
                                BookTitle = reader["book_title"].ToString(),
                                RequestDate = Convert.ToDateTime(reader["request_date"]),
                                Status = reader["status"].ToString()
                            });
                        }
                    }
                }
            }
            return requests;
        }

        public List<BookRequest> GetStudentBookRequests(string studentId)
        {
            List<BookRequest> requests = new List<BookRequest>();
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT br.request_id, br.student_id, br.book_id, b.title as book_title, br.request_date, br.status
                        FROM book_requests br
                        JOIN books b ON br.book_id = b.book_id
                        WHERE br.student_id = :student_id
                        ORDER BY br.request_date DESC";
                    cmd.BindByName = true;
                    cmd.Parameters.Add("student_id", OracleDbType.Varchar2).Value = studentId;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            requests.Add(new BookRequest
                            {
                                RequestId = Convert.ToInt32(reader["request_id"]),
                                StudentId = reader["student_id"].ToString(),
                                BookId = Convert.ToInt32(reader["book_id"]),
                                BookTitle = reader["book_title"].ToString(),
                                RequestDate = Convert.ToDateTime(reader["request_date"]),
                                Status = reader["status"].ToString()
                            });
                        }
                    }
                }
            }
            return requests;
        }

        public void ApproveBookRequest(int requestId)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "approve_book_request";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;
                    cmd.Parameters.Add("p_request_id", OracleDbType.Int32).Value = requestId;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RejectBookRequest(int requestId)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "reject_book_request";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;
                    cmd.Parameters.Add("p_request_id", OracleDbType.Int32).Value = requestId;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ReturnBook(int requestId)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "return_book";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;
                    cmd.Parameters.Add("p_request_id", OracleDbType.Int32).Value = requestId;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
