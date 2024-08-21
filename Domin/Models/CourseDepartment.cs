using Domin.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class CourseDepartment
    {
        [Key]
        public int Id { get; set; }

        public int DepartmentsId { get; set; }

        [ForeignKey(nameof(DepartmentsId))]
        public Department Department { get; set; }

        public int CoursesId { get; set; }

        [ForeignKey(nameof(CoursesId))]
        public Course Course { get; set; }

       


    }
}
