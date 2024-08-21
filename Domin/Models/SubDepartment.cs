using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class SubDepartment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }

        //public ICollection<Student> Students { get; set; }
        //public ICollection<CourseDepartment> courses { get; set; }
        //public ICollection<Section> sections { get; set; }
        //public ICollection<TeachIn> Professors { get; set; }

    }
}
