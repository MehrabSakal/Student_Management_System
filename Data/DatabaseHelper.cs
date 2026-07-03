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
    }
}
