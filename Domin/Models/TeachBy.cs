using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class TeachBy
    {
        [Key]
        public int Id { get; set; }

        public int ProfessorId { get; set; }
        [ForeignKey(nameof(ProfessorId))]
        public Professor Professor { get; set; }


        public int SectionId { get; set; }
        [ForeignKey(nameof(SectionId))]
        public Section Section { get; set; }

        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }

    }
}
