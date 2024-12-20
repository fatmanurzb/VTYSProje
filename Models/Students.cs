using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Students
    {
        [Key]
        public int StudentId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Role { get; set; }
        public DateTime EntrollmentDate { get; set; } = DateTime.Now;

        public int InstructorId { get; set; }
        public bool isCourseSelectionConfirmed { get; set; } = false;
    }
}
