using Domin.Models;
using Infrastructure.Services.DepartmentServices;
using Infrastructure.Services.LevelServices;
using Infrastructure.Services.SectionServices;
using Infrastructure.Services.StudentServies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Applicaction.Controller
{
    public class DepartmentController
    {
        private readonly IDepartmentServices _DepartmentServices;
        public DepartmentController(IDepartmentServices departmentServices)
        {
            _DepartmentServices = departmentServices;
        }
        public async Task<List<Department>> GetDep()
        {
            return await _DepartmentServices.GetDepartments();
        }
        public async Task<List<Department>> GetDep(int levelid)
        {
            return await _DepartmentServices.GetDepartments(levelid);
        }
        public async Task<bool> AddDep(Department model)
        {
            if (model == null) return false;

            try
            {
                var result = await _DepartmentServices.AddDepartment(model);
                return result.Succed;
            }
            catch
            {
                return false;

            }

        }

        public async Task<bool> UpdateDep(Department model)
        {
            if (model == null) return false;

            try
            {
                var result = await _DepartmentServices.UpdataDepartment(model);
                return result.Succed;
            }
            catch
            {
                return false;

            }

        }

        public async Task<List<Course>> GetCoursesWithDep(int depid)
        {
            return await _DepartmentServices.GetCoursesWithDep(depid);
        }
        public async Task<List<Student>> GetStudentWithDep(int depid)
        {
            return await _DepartmentServices.GetStudentsWithDep(depid);
        }
        public async Task<List<Professor>> GetProfessorsWithDep(int depid)
        {
            return await _DepartmentServices.GetProfessorsWithDep(depid);
        }





    }
}
