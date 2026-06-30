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
                    cmd.CommandText = "SELECT student_id, full_name, email, phone, department, address, advisor_id FROM students WHERE student_id = :id";
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
                    cmd.Parameters.Add("p_department", OracleDbType.Varchar2).Value = (object)student.Department ?? DBNull.Value;
                    cmd.Parameters.Add("p_address", OracleDbType.Varchar2).Value = (object)student.Address ?? DBNull.Value;
                    cmd.Parameters.Add("p_advisor_id", OracleDbType.Varchar2).Value = (object)student.AdvisorId ?? DBNull.Value;

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
                    cmd.Parameters.Add("p_department", OracleDbType.Varchar2).Value = (object)student.Department ?? DBNull.Value;
                    cmd.Parameters.Add("p_address", OracleDbType.Varchar2).Value = (object)student.Address ?? DBNull.Value;
                    cmd.Parameters.Add("p_advisor_id", OracleDbType.Varchar2).Value = (object)student.AdvisorId ?? DBNull.Value;

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
    }
}
