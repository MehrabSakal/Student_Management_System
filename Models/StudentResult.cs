namespace StudentManagementSystem.Models
{
    public class StudentResult
    {
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string CourseNo { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public double? Marks { get; set; }
        public double? GPA { get; set; }

        public string Grade
        {
            get
            {
                if (!Marks.HasValue) return "N/A";
                if (Marks >= 80) return "A+";
                if (Marks >= 75) return "A";
                if (Marks >= 70) return "A-";
                if (Marks >= 65) return "B+";
                if (Marks >= 60) return "B";
                if (Marks >= 55) return "B-";
                if (Marks >= 50) return "C+";
                if (Marks >= 45) return "C";
                if (Marks >= 40) return "D";
                return "F";
            }
        }
    }
}
