using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.LevelServices
{
    public interface ILevelServices
    {
        public Task<List<Level>>  GetLevels();
        public Task<ResultServices> AddLevel(Level model);
        public Task<Level> FindByID(int id);
        public Task<List<Student>> GetStudentsWithLevel(int levelid);
        public Task<List<Course>> GetCoursesWithLevel(int levelid);
        public Task<List<Department>> GetDepartmentsWithLevel(int levelid);
    }
}
