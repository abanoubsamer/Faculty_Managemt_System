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
    public class Section
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }

        public int LevelId { get; set; }

        [ForeignKey(nameof(LevelId))]
        public Level Level { get; set; }

        // Each section includes multiple students
        public ICollection<Student> Students { get; set; } = new List<Student>();

        // Each section is taught by multiple professors
        public ICollection<TeachBy> Professors { get; set; } = new List<TeachBy>();
    }

}
