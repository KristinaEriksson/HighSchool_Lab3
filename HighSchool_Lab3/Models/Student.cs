using System;
using System.Collections.Generic;

namespace HighSchool_Lab3.Models
{
    public partial class Student
    {
        public Student()
        {
            Grades = new HashSet<Grade>();
        }

        public int StudentId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string SocialSecurityNumber { get; set; } = null!;
        public string Class { get; set; } = null!;

        public virtual ICollection<Grade> Grades { get; set; }
    }
}
