using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models.ViewModel
{
    public class CourseProfessorDto
    {
        public string CourseName { get; set; }
        public string ProfessorFirstName { get; set; }
        public string ProfessorLastName { get; set; }
        public decimal Grade { get; set; }

      public Student student { get; set; }
    }
}
