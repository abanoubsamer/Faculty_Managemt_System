using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class TeachTo
    {
        // Many To Many {Professor , Course}

        [Key]
        public int Id { get; set; }

        public int ProfessorsId { get; set; } // Foreign key for Professor

        [ForeignKey(nameof(ProfessorsId))]
        public Professor Professor { get; set; }

        public int CoursesId { get; set; } // Foreign key for Course

        [ForeignKey(nameof(CoursesId))]
        public Course Course { get; set; }
    }
}
