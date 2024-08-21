using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class Level
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
      
        public ICollection<Student> students { get; set; } = new List<Student>();
        public ICollection<Section> Sections { get; set; } = new List<Section>();
        public ICollection<LevelDepartment> Departments { get; set; } = new List<LevelDepartment>();
        public ICollection<Course> courses { get; set; } = new List<Course>();
      
    }
}
