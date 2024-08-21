using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.DepartmentServices
{
    public interface IDepartmentServices
    {

        public Task<List<Department>>  GetDepartments();
        public  Task<ResultServices> AddDepartment(Department model);
        public  Task<ResultServices> UpdataDepartment(Department model);
        public  Task<ResultServices> DeleteDepartment(Department model);
        public Task<List<Department>> GetDepartments(int levelid);
        public Task<List<Course>> GetCoursesWithDep(int departmentsId);
        public Task<List<Professor>> GetProfessorsWithDep(int departmentsId);
        public Task<List<Student>> GetStudentsWithDep(int departmentsId);



    }
}
