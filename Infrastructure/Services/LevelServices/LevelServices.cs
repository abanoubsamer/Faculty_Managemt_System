using Domin.Models;
using Infrastructure.UintOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.LevelServices
{
    public class LevelServices : ILevelServices
    {
        private readonly IUnitOfWork    _UnitOfWork;

        public LevelServices(IUnitOfWork unitOfWork )
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<ResultServices> AddLevel(Level model)
        {
            try
            {

                await _UnitOfWork.Repository<Level>().Add(model);
                //Transaction.Commit();
                return new ResultServices
                {
                    Succed = true,
                    Error = false,

                };
            }
            catch (Exception ex)
            {
                //await Transaction.RollbackAsync();
                return new ResultServices
                {
                    Succed = false,
                    Error = true,
                    MsgError = $"Error Is {ex}",
                };
            }
        }
        public async Task<Level> FindByID(int id)
        {
            if (id <= 0) return null;

            try
            {
                return await _UnitOfWork.Repository<Level>().Find(x => x.Id == id);
            }
            catch (Exception ex)
            {
                return null; 
            }
        }
        public async Task<List<Level>>  GetLevels()
        {

            return await _UnitOfWork.Repository<Level>().GetAll();

        }
        public async Task<List<Student>> GetStudentsWithLevel( int levelid)
        {

            return await _UnitOfWork.Repository<Level>().GetQueryable()
                    .Include(x => x.students)
                    .ThenInclude(X=>X.Department)
                      .ThenInclude(X => X.sections)
                    .Where(x=>x.Id==levelid)
                    .SelectMany(x => x.students) 
                    .ToListAsync();

        }
        public async Task<List<Course>> GetCoursesWithLevel(int levelid)
        {

            return await _UnitOfWork.Repository<Level>().GetQueryable()
                    .Include(x => x.courses)
                    .SelectMany(x => x.courses) // Flatten the list of students
                    .ToListAsync();

        }
        public async Task<List<Department>> GetDepartmentsWithLevel(int levelid)
        {

                return await _UnitOfWork.Repository<Level>().GetQueryable()
               .Include(x => x.Departments) // Include the LevelDepartment join table
               .ThenInclude(ld => ld.Department) // Include the actual Department entity
               .SelectMany(x => x.Departments) // Flatten the list of LevelDepartment entries
               .Select(ld => ld.Department) // Project to the actual Department entity
               .ToListAsync();



        }

    }
}
