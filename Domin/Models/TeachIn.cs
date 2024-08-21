using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class TeachIn
    {
        [Key]
        public int Id { get; set; }

        public int ProfessorId { get; set; }
        [ForeignKey(nameof(ProfessorId))]
        public Professor Professor { get; set; }


        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }


    }
}
