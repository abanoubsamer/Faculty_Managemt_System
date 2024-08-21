using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Required]
        public string Phone { get; set; }
        [Required]
        [Range(1, 10000000, ErrorMessage = "Required Department")]
        public int DepId { get; set; }

        [ForeignKey(nameof(DepId))]
        public Department Department { get; set; }

        [Range(17, 100)]
        [Required]
        public int Age { get; set; }
        [Required]
        [Range(1, 10000000, ErrorMessage = "Required Level")]
        public int LevelId { get; set; }

        [ForeignKey(nameof(LevelId))]
      
        public Level Level { get; set; }
        public int SectionId { get; set; }

        [ForeignKey(nameof(SectionId))]

        public Section section { get; set; }

        [Range(0.0, 4.0)]
        [Required]
        public decimal Gpa { get; set; }

        [DataType(DataType.Date)]
        [Required]

        public DateTime Birthday { get; set; }


        public ICollection<Enrollment> courses { get; set; }
   

    }
}
