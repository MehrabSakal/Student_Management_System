namespace StudentManagementSystem.Models
{
    public class Department
    {
        public string DeptId { get; set; } = string.Empty;
        public string DeptName { get; set; } = string.Empty;
        public string Faculty { get; set; } = string.Empty;
        public int NoOfStudents { get; set; }
    }
}
