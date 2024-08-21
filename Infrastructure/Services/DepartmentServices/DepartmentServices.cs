using Domin.Models;
using Infrastructure.UintOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.DepartmentServices
{
    public class DepartmentServices : IDepartmentServices
    {
        readonly private IUnitOfWork _UnitOfWork;
        public DepartmentServices(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        public  async Task<List<Department>>  GetDepartments()
        {
          return await _UnitOfWork.Repository<Department>().GetAll();
        }
        public async Task<List<Department>> GetDepartments(int levelid)
        {
            return await _UnitOfWork.Repository<LevelDepartment>()
                .GetQueryable().Where(x=>x.LevelId==levelid)
                .Select(x=>x.Department).ToListAsync();
        }
        public async Task<ResultServices> AddDepartment( Department model)
        {
            if (model == null) return new ResultServices
            {
                Error = true,
                Succed = false,
                MsgError = " Enrrolemnt Is Null",
            };

            try
            {
                await _UnitOfWork.Repository<Department>().Add(model);
                return new ResultServices
                {
                    Error = false,
                    Succed = true,

                };
            }
            catch (Exception ex)
            {
                return new ResultServices
                {
                    Error = true,
                    Succed = false,
                    MsgError = $"Error {ex.Message}"

                };
            }

        }

        public async Task<ResultServices> UpdataDepartment(Department model)
        {
            if (model == null) return new ResultServices
            {
                Error = true,
                Succed = false,
                MsgError = " Enrrolemnt Is Null",
            };

            try
            {
                await _UnitOfWork.Repository<Department>().Update(model);
                return new ResultServices
                {
                    Error = false,
                    Succed = true,

                };
            }
            catch (Exception ex)
            {
                return new ResultServices
                {
                    Error = true,
                    Succed = false,
                    MsgError = $"Error {ex.Message}"

                };
            }
        }

        public async Task<ResultServices> DeleteDepartment(Department model)
        {
            if (model == null) return new ResultServices
            {
                Error = true,
                Succed = false,
                MsgError = " Enrrolemnt Is Null",
            };

            try
            {
                await _UnitOfWork.Repository<Department>().Delete(model);
                return new ResultServices
                {
                    Error = false,
                    Succed = true,

                };
            }
            catch (Exception ex)
            {
                return new ResultServices
                {
                    Error = true,
                    Succed = false,
                    MsgError = $"Error {ex.Message}"

                };
            }
        }

        public async Task<List<Course>> GetCoursesWithDep(int departmentsId)
        {
            return await _UnitOfWork.Repository<CourseDepartment>().GetQueryable()
                .Where(x=>x.DepartmentsId==departmentsId)
                .Select(x=>x.Course).ToListAsync();
        }

        public async Task<List<Professor>> GetProfessorsWithDep(int departmentsId)
        {
            return await _UnitOfWork.Repository<TeachIn>().GetQueryable()
                .Where(x => x.DepartmentId == departmentsId)
                .Select(x => x.Professor).ToListAsync();
        }

        public async Task<List<Student>> GetStudentsWithDep(int departmentsId)
        {
            return await _UnitOfWork.Repository<Student>().GetQueryable()
                 .Where(x => x.DepId == departmentsId)
                 .ToListAsync();
        }
    }
}
