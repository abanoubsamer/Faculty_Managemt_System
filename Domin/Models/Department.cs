using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(1, 3000)]
        public int Capacity { get; set; }




        public ICollection<Student> Students { get; set; }
        //public ICollection<SubDepartment> SubDepartments { get; set; }
        public ICollection<CourseDepartment> courses { get; set; } 
        public ICollection<LevelDepartment> levels { get; set; } 
        public ICollection<Section> sections { get; set; } 
        public ICollection<TeachIn> Professors { get; set; } 
    

    }
}
