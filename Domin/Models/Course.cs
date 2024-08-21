using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        //// hna ana b8er asm ale fiald fe al db
        //[Column("CoureName", TypeName = "nvarchar(20)")]
        public string Name { get; set; }

        public int LevelId { get; set; }

        [ForeignKey(nameof(LevelId))]
        public Level Level { get; set; }

        [Range(1, 6)]
     
        public int Hours { get; set; }

        public ICollection<TeachTo> professors { get; set; }
        public ICollection<CourseDepartment> departments { get; set; }
        public ICollection<Enrollment> Students { get; set; }



    }
}
