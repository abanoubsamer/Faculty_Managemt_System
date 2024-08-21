using Domin.Models;
using Infrastructure.UintOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.CouresServices
{
    public class CouresServices:ICouresServices
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CouresServices(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork=UnitOfWork;
        }

        public async Task<ResultServices> AddCoures(Course model)
        {
            if (model == null) return new ResultServices
            {
                Error = true,
                Succed = false,
                MsgError = "Model Is Null"
            };

            try
            {
                await _UnitOfWork.Repository<Course>().Add(model);
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

        public async Task<ResultServices> AddCouresInDepartemnt(CourseDepartment model)
        {
            if (model == null) return new ResultServices
            {
                Error = true,
                Succed = false,
                MsgError = "Model Is Null"
            };

            try
            {
                await _UnitOfWork.Repository<CourseDepartment>().Add(model);
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

        public async Task<ResultServices> DeleteCoures(Course model)
        {
            if (model == null) return new ResultServices
            {
                Error = true,
                Succed = false,
                MsgError = "Model Is Null"
            };

            try
            {
                await _UnitOfWork.Repository<Course>().Delete(model);
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

        public async Task<Course> FindById(int id)
        {
            return await _UnitOfWork.Repository<Course>().Find(x => x.Id == id);
        }

        public async Task<List<Course>> GetCourses()
        {
            return await _UnitOfWork.Repository<Course>().GetQueryable().Include(x=>x.Level).ToListAsync();
        }

        public async Task<List<Course>> GetCourses(int levelid)
        {
            return await _UnitOfWork.Repository<Course>().GetQueryable().Include(x => x.Level).Where(x => x.LevelId == levelid).ToListAsync();
        }

        public async Task<List<Course>> GetCourses(int levelid, int depid)
        {
            return await _UnitOfWork.Repository<CourseDepartment>().GetQueryable()
               .Include(x=>x.Course)
               .Where(x => x.DepartmentsId == depid && x.Course.LevelId==levelid)
               //.Include(x => x.Course.Level)
               .Select(x => x.Course).ToListAsync();
        }

        public async Task<ResultServices> UpdateCoures(Course model)
        {
            if (model == null) return new ResultServices
            {
                Error = true,
                Succed = false,
                MsgError = "Model Is Null"
            };

            try
            {
                await _UnitOfWork.Repository<Course>().Update(model);
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
