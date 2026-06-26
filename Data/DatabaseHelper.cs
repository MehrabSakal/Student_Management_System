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

        // Method to get a single student by ID
        public Student GetStudent(string studentId)
        {
            Student student = null;

            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    // Simple SQL query to fetch a student
                    cmd.CommandText = "SELECT student_id, full_name, email, phone, department, address, advisor_id FROM students WHERE student_id = :id";
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
                                Department = reader["department"].ToString(),
                                Address = reader["address"].ToString(),
                                AdvisorId = reader["advisor_id"].ToString()
                            };
                        }
                    }
                }
            }

            return student;
        }

        // Method to get all students
        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT student_id, full_name, email, phone, department, address, advisor_id FROM students";

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
                                Department = reader["department"].ToString(),
                                Address = reader["address"].ToString(),
                                AdvisorId = reader["advisor_id"].ToString()
                            });
                        }
                    }
                }
            }

            return students;
        }

        // Method to call PL/SQL procedure to ADD a student
        public void AddStudent(Student student)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    // We call the PL/SQL stored procedure here
                    cmd.CommandText = "add_student";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_student_id", OracleDbType.Varchar2).Value = student.StudentId;
                    cmd.Parameters.Add("p_full_name", OracleDbType.Varchar2).Value = student.FullName;
                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = student.Email;
                    cmd.Parameters.Add("p_phone", OracleDbType.Varchar2).Value = student.Phone;
                    cmd.Parameters.Add("p_department", OracleDbType.Varchar2).Value = student.Department;
                    cmd.Parameters.Add("p_address", OracleDbType.Varchar2).Value = student.Address;
                    cmd.Parameters.Add("p_advisor_id", OracleDbType.Varchar2).Value = student.AdvisorId;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Method to call PL/SQL procedure to UPDATE a student
        public void UpdateStudent(Student student)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "update_student";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_student_id", OracleDbType.Varchar2).Value = student.StudentId;
                    cmd.Parameters.Add("p_full_name", OracleDbType.Varchar2).Value = student.FullName;
                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = student.Email;
                    cmd.Parameters.Add("p_phone", OracleDbType.Varchar2).Value = student.Phone;
                    cmd.Parameters.Add("p_department", OracleDbType.Varchar2).Value = student.Department;
                    cmd.Parameters.Add("p_address", OracleDbType.Varchar2).Value = student.Address;
                    cmd.Parameters.Add("p_advisor_id", OracleDbType.Varchar2).Value = student.AdvisorId;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Method to call PL/SQL procedure to DELETE a student
        public void DeleteStudent(string studentId)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "delete_student";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_student_id", OracleDbType.Varchar2).Value = studentId;

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
