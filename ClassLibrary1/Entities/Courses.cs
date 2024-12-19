using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Courses
    {
        [Key]
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public int CourseCredit { get; set; }
        public bool CourseType { get; set; }

    }
}
