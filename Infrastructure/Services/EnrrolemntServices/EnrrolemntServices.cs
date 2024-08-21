using Domin.Models;
using Infrastructure.UintOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.EnrrolemntServices
{   
    public class EnrrolemntServices : IEnrrolemntServices
    {

        private readonly IUnitOfWork _UnitOfWork;
        public EnrrolemntServices( IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<ResultServices> AddEnrrolment(Enrollment model)
        {
            if (model == null) return new ResultServices
            {
                Error = true,
                Succed = false,
                MsgError=" Enrrolemnt Is Null",
            };

            try
            {
                await _UnitOfWork.Repository<Enrollment>().Add(model);
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
                    MsgError=$"Error {ex.Message}"

                };
            }

            



        }

        public async Task<ResultServices> DeleteEnrrolemnt(Enrollment model)
        {
            if (model == null) return new ResultServices
            {
                Error = true,
                Succed = false,
                MsgError = " Enrrolemnt Is Null",
            };

            try
            {
                await _UnitOfWork.Repository<Enrollment>().Delete(model);
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

        public async Task<List<Course>> GetCourseLeveAndDep(int stdid,int studentDep, int studentLevel)
        {
            var enr = await _UnitOfWork.Repository<Enrollment>()
            .GetQueryable()
            .Where(x => x.StudentsId == stdid)
            .ToListAsync();

            // Get the list of courses based on level, department, and excluding already enrolled courses
            return await _UnitOfWork.Repository<CourseDepartment>()
                .GetQueryable()
                .Where(x => x.Course.LevelId == studentLevel
                            && x.DepartmentsId == studentDep
                            && !enr.Select(e => e.CoursesId).Contains(x.CoursesId)) // Exclude already enrolled courses
                .Select(x => x.Course) // Select only the Course entities
                .ToListAsync();
        }
        public async Task<Enrollment> FindById(int id)
        {
            return await _UnitOfWork.Repository<Enrollment>().Find(x => x.Id == id); 
        }

        public Task<List<Enrollment>> GetEnrollments()
        {
            return _UnitOfWork.Repository<Enrollment>().GetQueryable().Include(x=>x.Student).Include(x=>x.Course).ToListAsync();
        }

        public async Task<List<Enrollment>> GetEnrollmentsWithCousrs(int courseid)
        {
    
            return await _UnitOfWork.Repository<Enrollment>().GetQueryable().Include(x => x.Student).Include(x => x.Course).Where(x=>x.CoursesId== courseid).ToListAsync();

        }

        public async Task<List<Enrollment>> GetEnrollmentsWithLevel(int Levelid)
        {
            return await _UnitOfWork.Repository<Enrollment>().GetQueryable().Include(x => x.Student).Include(x => x.Course).Where(x => x.Course.LevelId == Levelid).ToListAsync();
        }

        public async Task<List<Enrollment>> GetEnrollmentsWithStudent(int studentid)
        {
            return await _UnitOfWork.Repository<Enrollment>().GetQueryable().Include(x => x.Student).Include(x => x.Course).Where(x => x.StudentsId == studentid).ToListAsync();
        }

        public async Task<ResultServices> UpdateEnrrolemnt(Enrollment model)
        {
            if (model == null) return new ResultServices
            {
                Error = true,
                Succed = false,
                MsgError = " Enrrolemnt Is Null",
            };

            try
            {
                await _UnitOfWork.Repository<Enrollment>().Update(model);
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
    }
}
