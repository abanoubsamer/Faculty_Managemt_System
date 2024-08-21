using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Program.Applicaction;
using Infrastructure.UintOfWork;
using Infrastructure.Services.StudentServies;
using Program.Applicaction.View;
using Program.Applicaction.Controller;
using Domin.Data;
using Domin;
using Microsoft.EntityFrameworkCore;
using Domin.Models;
using Infrastructure.Services.LevelServices;
using Infrastructure.Services.DepartmentServices;
using Infrastructure.Services.EnrrolemntServices;
using Infrastructure.Services.SectionServices;
using Infrastructure.Services.ProfessorServices;
using Infrastructure.Services.CouresServices;

namespace Program.ConfigureServices
{
    public static class ServiceCollectionExtensions
    {
        // Extension method for IServiceCollection
        public static void AddServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                       options.UseSqlServer(ConnectionDbContext.ConnectionString));

            // Register other services here
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IStudentServices, StudentServices>();
            services.AddTransient<IDepartmentServices, DepartmentServices>();
            services.AddTransient<IEnrrolemntServices, EnrrolemntServices>();
            services.AddTransient<ILevelServices, LevelServices>();
            services.AddTransient<IProfessorServices, ProfessorServices>();
            services.AddTransient<ISectionServices, SectionServices>();
            services.AddTransient<ICouresServices, CouresServices>();

            //Controller

            services.AddTransient<StudentController>();
            services.AddTransient<DepartmentController>();
            services.AddTransient<EnrrolemntController>();
            services.AddTransient<LevelController>();
            services.AddTransient<ProfessorController>();
            services.AddTransient<SectionController>();
            services.AddTransient<CouresController>();



            //View

            services.AddTransient<StudentView>();
            services.AddTransient<EnrrolemntView>();
            services.AddTransient<LevelView>();
            services.AddTransient<SectionView>();
            services.AddTransient<DepartmentView>();
            services.AddTransient<ProfessorView>();
            services.AddTransient<CouresView>();


            //APP
            services.AddTransient<App>();
        }
    }

}
