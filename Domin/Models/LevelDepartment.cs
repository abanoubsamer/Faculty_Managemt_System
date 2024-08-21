using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class LevelDepartment
    {

        [Key]
        public int Id { get; set; }

        public int DepartmentsId { get; set; }

        [ForeignKey(nameof(DepartmentsId))]
        public Department Department { get; set; }

        public int LevelId { get; set; }

        [ForeignKey(nameof(LevelId))]
        public Level Level { get; set; }
    }
}
