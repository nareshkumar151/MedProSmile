﻿namespace MedProSmile.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public int CategoryId { get; set; }
        public string? DepartmentName { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
    }
}
