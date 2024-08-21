using Domin.Models;
using Domin.Models.ViewModel;
using Infrastructure.UintOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.SectionServices
{
    public class SectionServices : ISectionServices
    {
        private readonly IUnitOfWork _UnitOfWork;

        public SectionServices(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<ResultServices> AddSection(Section model)
        {      
            
            if(model==null) return new ResultServices
                {
                    Succed = false,
                    Error = true,
                     MsgError = $"Error Is Model Is Null",
                 };

            try
            {

                await _UnitOfWork.Repository<Section>().Add(model);
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

        public async Task<ResultServices> DeleteSection(Section model)
        {
            if (model == null) return new ResultServices
            {
                Succed = false,
                Error = true,
                MsgError = $"Error Is Model Is Null",
            };

            try
            {

                await _UnitOfWork.Repository<Section>().Delete(model);
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

        public async Task<List<Section>> GetSections(int depid)
        {
            return await _UnitOfWork.Repository<Section>().GetQueryable().Where(x => x.DepartmentId == depid).ToListAsync();
        }

        public async Task<List<Section>> GetSections(int depid,int levelid)
        {
            return await _UnitOfWork.Repository<Section>().GetQueryable().Where(x => x.DepartmentId == depid && x.LevelId==levelid).ToListAsync();
        }


        public async Task<List<ProfessorShowViewModel>> ProfessoresWithSection(int sectionid)
        {
            return await _UnitOfWork.Repository<TeachBy>()
                 .GetQueryable()
                 .Where(x => x.SectionId == sectionid)
                 .Include(x => x.Professor) // Ensure Professor is included
                 .Include(x => x.Course) // Ensure Course is included
                 .Select(x => new ProfessorShowViewModel
                 {
                     Id = x.Professor.Id,
                     FirstName = x.Professor.FirstName,
                     LastName = x.Professor.LastName,
                     CourseName = x.Course.Name
                 })
                 .ToListAsync();
                }

        public async Task<List<Student>> StudentsWithSection(int sectionid)
        {
            return await _UnitOfWork.Repository<Student>()
                     .GetQueryable()
                     .Where(s => s.SectionId == sectionid)
                     .ToListAsync();
        }

        public async Task<ResultServices> UpdateSection(Section model)
        {
            if (model == null) return new ResultServices
            {
                Succed = false,
                Error = true,
                MsgError = $"Error Is Model Is Null",
            };

            try
            {

                await _UnitOfWork.Repository<Section>().Update(model);
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


        public async Task<ResultServices> AddProfessorInSection(TeachBy model)
        {
            if (model == null) return new ResultServices
            {
                Succed = false,
                Error = true,
                MsgError = $"Error Is Model Is Null",
            };

            try
            {

                await _UnitOfWork.Repository<TeachBy>().Add(model);
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

        public async Task<List<Professor>> GetProfessorsWithDep(int depid)
        {
            return await _UnitOfWork.Repository<TeachIn>().GetQueryable().Where(x => x.DepartmentId == depid).Select(x => x.Professor).ToListAsync();
        }

        public async Task<ResultServices> ValidationAddProfessorInSection(int Sectionid, int couresid)
        {
            var res= await _UnitOfWork.Repository<TeachBy>().GetQueryable()
                    .Where(x=> x.SectionId==Sectionid && x.CourseId==couresid).Select(x=>x.Professor).FirstOrDefaultAsync();

            if (res != null)
            {
                return new ResultServices
                {
                    Error = true,
                    Succed = false,
                    MsgError = $"Can Not Save Is Couers Bucoues Teach By {res.FirstName} {res.LastName} In This Section"
                };
            }
            else
                return  new ResultServices
                {
                    Error = false,
                    Succed = true,
                  
                };


        }
      
        public async Task<List<Course>> GetCoursesProfessorWithDep(int professorId, int departmentId, int levelid)
        {
            var teachToQuery = _UnitOfWork.Repository<TeachTo>().GetQueryable();
            var courseDepQuery = _UnitOfWork.Repository<CourseDepartment>().GetQueryable();
            var courseQuery = _UnitOfWork.Repository<Course>().GetQueryable();

            var query = from teachTo in teachToQuery
                        where teachTo.ProfessorsId == professorId//hna ana gebt ale mwad ale bydha 
                        join courseDep in courseDepQuery on teachTo.CoursesId equals courseDep.CoursesId
                        where courseDep.DepartmentsId == departmentId
                        join course in courseQuery on courseDep.CoursesId equals course.Id
                        where course.LevelId==levelid 
                        select course;


            return await query.ToListAsync();
        }


      



    }
}
