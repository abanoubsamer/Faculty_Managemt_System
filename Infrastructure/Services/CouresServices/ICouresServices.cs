using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.CouresServices
{
    public interface ICouresServices
    {

        public Task<ResultServices> AddCoures(Course model);
        public Task<ResultServices> UpdateCoures(Course model);
        public Task<ResultServices> DeleteCoures(Course model);
        public Task<ResultServices> AddCouresInDepartemnt(CourseDepartment model);
        public Task<Course> FindById(int id);
        public Task<List<Course>> GetCourses();
        public Task<List<Course>> GetCourses(int levelid);
        public Task<List<Course>> GetCourses(int levelid,int depid);


    }
}
