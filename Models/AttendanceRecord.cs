using System;

namespace StudentManagementSystem.Models
{
    public class AttendanceRecord
    {
        public int AttendanceId { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string CourseNo { get; set; } = string.Empty;
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; } = string.Empty; // "Present" or "Absent"
    }

    public class StudentAttendanceSummary
    {
        public string CourseNo { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public double AttendancePercentage { get; set; }
    }
}
