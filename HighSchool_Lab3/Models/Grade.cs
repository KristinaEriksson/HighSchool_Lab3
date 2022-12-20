using System;
using System.Collections.Generic;

namespace HighSchool_Lab3.Models
{
    public partial class Grade
    {
        public int GradeId { get; set; }
        public int FkStudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public int Grade1 { get; set; }
        public int FkEmploymentNumber { get; set; }
        public string EmployeeName { get; set; } = null!;
        public DateTime SetDate { get; set; }

        public virtual Employee FkEmploymentNumberNavigation { get; set; } = null!;
        public virtual Student FkStudent { get; set; } = null!;
    }
}
