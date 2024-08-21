using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models.ViewModel
{
    public class StudentDetailsDTO
    {
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string level { get; set; }
        public List<CourseDetailsDTO> Courses { get; set; }
    }
    public class CourseDetailsDTO
    {
        public string CourseName { get; set; }
        public string ProfessorFirstName { get; set; }
        public string ProfessorLastName { get; set; }
        public decimal Grad { get; set; }
    }

}
