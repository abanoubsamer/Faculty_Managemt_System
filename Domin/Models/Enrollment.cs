using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class Enrollment
    {
        /// Many To Many {Student , Course}
 
        [Key]
        public int Id { get; set; }

        public int StudentsId { get; set; } // Foreign key for Student

        [ForeignKey(nameof(StudentsId))]
        public virtual Student Student { get; set; }

        public int CoursesId { get; set; } // Foreign key for Course

        [ForeignKey(nameof(CoursesId))]
        public virtual  Course Course { get; set; }

        [Range(0.0, 100.0)]
        public decimal Grade { get; set; }


      
    }
}
