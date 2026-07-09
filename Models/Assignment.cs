using System;

namespace StudentManagementSystem.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public string CourseNo { get; set; } = string.Empty;
        public string TeacherId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AssignmentType { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
    }
}
