using System;

namespace StudentManagementSystem.Models
{
    public class ResearchRequest
    {
        public int RequestId { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string TeacherId { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
        public string ResearchInterest { get; set; } = string.Empty;
        public string ResearchDescription { get; set; } = string.Empty;
        public string PreviousExperience { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
