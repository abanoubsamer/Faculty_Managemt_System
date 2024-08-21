using Domin.Models;
using Infrastructure.UintOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.ValidateModel;
using Microsoft.EntityFrameworkCore;
using Domin.Models.ViewModel;

namespace Infrastructure.Services.StudentServies
{
    public class StudentServices : IStudentServices
    {
        private readonly IUnitOfWork _UnitOfWork;

        public StudentServices(IUnitOfWork unitOfWork)
        {
            _UnitOfWork=unitOfWork;
        }
        public async Task<ResultServices> AddStudent(Student model)
        {
            //var Transaction = await _UnitOfWork.BeginTransactionAsync();
            try
            {

                await _UnitOfWork.Repository<Student>().Add(model);
                //Transaction.Commit();
                return new ResultServices
                {
                    Succed = true ,
                    Error =false,
                  
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

        public async Task<ResultServices> DeleteStudent(Student model)
        {
            try
            {

                await _UnitOfWork.Repository<Student>().Delete(model);
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

        public async Task<Student> FindById(int id)
        {
            return await _UnitOfWork.Repository<Student>().GetQueryable()
                 .Include(l => l.Level)
                 .Include(d => d.Department).Include(S=>S.section)
                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<Student> FindName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<StudentDetailsDTO> GetStudentWithDetails(int studentid)
        {
            var query = from s in _UnitOfWork.Repository<Student>().GetQueryable()
                        join e in _UnitOfWork.Repository<Enrollment>().GetQueryable() on s.Id equals e.StudentsId
                        join c in _UnitOfWork.Repository<Course>().GetQueryable() on e.CoursesId equals c.Id
                        join tt in _UnitOfWork.Repository<TeachTo>().GetQueryable() on c.Id equals tt.CoursesId
                        join p in _UnitOfWork.Repository<Professor>().GetQueryable() on tt.ProfessorsId equals p.Id
                        where s.Id == studentid
                        select new
                        {
                            StudentFirstName = s.FirstName,
                            level=s.Level.Name,
                            StudentLastName = s.LastName,
                            CourseName = c.Name,
                            Grad=e.Grade,
                            ProfessorFirstName = p.FirstName,
                            ProfessorLastName = p.LastName
                        };

            var result = await query.ToListAsync();

            if (result.Any())
            {
                var studentDetails = new StudentDetailsDTO
                {
                    StudentFirstName = result.First().StudentFirstName,
                    StudentLastName = result.First().StudentLastName,
                    level =result.First().level,
                    Courses = result.Select(r => new CourseDetailsDTO
                    {
                        CourseName = r.CourseName,
                        
                        Grad= r.Grad,
                        
                        ProfessorFirstName = r.ProfessorFirstName,
                        ProfessorLastName = r.ProfessorLastName
                    }).ToList()
                };

                return studentDetails;
            }

            return null;
        }

        public async Task<Student> GetAllCouresStudent(int studentid)
        {
            return await _UnitOfWork.Repository<Student>().GetQueryable().Include(x => x.courses).FirstOrDefaultAsync(x => x.Id == studentid);
        }

        public async Task<List<CourseProfessorDto>> GetAllProfessorStudent(int studentId)
        {
            // Get the student with the associated Section and Department
            var studentQuery = await _UnitOfWork.Repository<Student>()
                .GetQueryable()
                .Include(s => s.section) // Include Section if needed
                .Include(s => s.Department) // Include Department if needed
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (studentQuery == null)
            {
                return new List<CourseProfessorDto>(); // Return an empty list if the student is not found
            }

            // Ensure that SectionId is not null
            if (studentQuery.SectionId == null)
            {
                return new List<CourseProfessorDto>(); // Return an empty list if SectionId is null
            }

            // Perform the query directly with the database
            var query = from enrollment in _UnitOfWork.Repository<Enrollment>().GetQueryable()
                        join teachBy in _UnitOfWork.Repository<TeachBy>().GetQueryable()
                            on enrollment.CoursesId equals teachBy.CourseId
                        where enrollment.StudentsId == studentId
                              && teachBy.SectionId == studentQuery.SectionId
                        select new CourseProfessorDto
                        {
                            student = studentQuery,
                            CourseName = teachBy.Course.Name,
                            ProfessorFirstName = teachBy.Professor.FirstName,
                            ProfessorLastName = teachBy.Professor.LastName,
                            Grade = enrollment.Grade
                        };

            // Execute the query and get the results
            return await query.ToListAsync();
        }


        public async Task<List<Student>>  GetAllStudent()
        {
            try
            {
                // استخدام AsNoTracking لتحسين الأداء
                return  await _UnitOfWork.Repository<Student>()
                    .GetQueryable()
                    .Include(l => l.Level)        // تضمين الكيانات المرتبطة بمستوى الطالب
                    .Include(d => d.Department)    // تضمين الكيانات المرتبطة بقسم الطالب
                    .Include(s=>s.section)
                    .AsNoTracking()                // عدم تتبع الكيانات لتحسين الأداء
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching students: {ex.Message}");
                return new List<Student>(); // إرجاع قائمة فارغة في حالة حدوث خطأ
            }
        }

        public Task<bool>  StudentExits(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultServices>  UpdateStudent(Student model)
        {
            try
            {

                await _UnitOfWork.Repository<Student>().Update(model);
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
    }
}
