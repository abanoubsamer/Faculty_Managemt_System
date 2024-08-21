using Domin.Models;
using Infrastructure.UintOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.ProfessorServices
{
    public class ProfessorServices : IProfessorServices
    {
        private readonly IUnitOfWork _UnitOfWork;
        public ProfessorServices(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<ResultServices> AddProfessor(Professor model)
        {
            if (model == null) return new ResultServices
            {
                Error = true,
                Succed = false,
                MsgError = " Model is Null",

            }; 


            try
            {
     
                await _UnitOfWork.Repository<Professor>().Add(model);
                return new ResultServices
                {
                    Error = false,
                    Succed = true,
                 

                };
            }
            catch(Exception ex)
            {
                return new ResultServices
                {
                    Error = true,
                    Succed = false,
                    MsgError = $" Error Is  {ex.Message}",

                };
            }
        }

        public async Task<ResultServices> DeleteProfessor(Professor model)
        {
            if (model == null) return new ResultServices
            {
                Error = true,
                Succed = false,
                MsgError = " Model is Null",

            };


            try
            {

                await _UnitOfWork.Repository<Professor>().Delete(model);
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
                    MsgError = $" Error Is  {ex.Message}",

                };
            }
        }

        public Task<Professor> FindById(int id )
        {
            return _UnitOfWork.Repository<Professor>().Find(x => x.Id == id);
        }

        public async Task<List<Professor>> GetAllProfessor()
        {
            return await _UnitOfWork.Repository<Professor>().GetAll();
        }

        public async Task<List<Course>> GetCoursesWithProfessor(int idProfessor)
        {
            return await _UnitOfWork.Repository<TeachTo>()
                .GetQueryable().Where(x=>x.ProfessorsId==idProfessor).Select(x => x.Course).ToListAsync();
        }


        public async Task<List<Course>> GetCoursesWithLevel(int idLevel)
        {
            return await _UnitOfWork.Repository<Course>()
                .GetQueryable().Where(x => x.LevelId == idLevel).ToListAsync();
        }


        public async Task<List<Department>> GetDepartmentsWithProfessor(int idProfessor)
        {//////////////////////////////////////////////////////
            return await _UnitOfWork.Repository<TeachIn>()
                .GetQueryable().Where(x => x.ProfessorId == idProfessor)
                .Select(x=>x.Department).ToListAsync();
        }

        public async Task<List<Student>> GetStudentsWithProfessor(int idProfessor)
        {
            return await _UnitOfWork.Repository<TeachBy>().GetQueryable()
                      .Include(x => x.Section)
                      .ThenInclude(section => section.Students)
                      .Where(x => x.ProfessorId == idProfessor)
                      .SelectMany(x => x.Section.Students)
                      .ToListAsync();
        }

        public async Task<ResultServices> UpdateProfessor(Professor model)
        {
            if (model == null) return new ResultServices
            {
                Error = true,
                Succed = false,
                MsgError = " Model is Null",

            };


            try
            {

                await _UnitOfWork.Repository<Professor>().Update(model);
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
                    MsgError = $" Error Is  {ex.Message}",

                };
            }
        }

        public async Task<ResultServices> AddCoursesProfessor(TeachTo model)
        {
            if (model == null) return new ResultServices
            {
                Error = true,
                Succed = false,
                MsgError = " Model is Null",

            };


            try
            {

                await _UnitOfWork.Repository<TeachTo>().Add(model);
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
                    MsgError = $" Error Is  {ex.Message}",

                };
            }
        }

        public async Task<ResultServices> AddProfessorInDep(TeachIn model)
        {
            if (model == null) return new ResultServices
            {
                Error = true,
                Succed = false,
                MsgError = " Model is Null",

            };


            try
            {

                await _UnitOfWork.Repository<TeachIn>().Add(model);
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
                    MsgError = $" Error Is  {ex.Message}",

                };
            }
        }




    }
}
