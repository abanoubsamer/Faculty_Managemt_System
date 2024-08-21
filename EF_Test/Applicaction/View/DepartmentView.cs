using Domin.Helper;
using Program.Applicaction.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Applicaction.View
{
    public class DepartmentView
    {
        private readonly DepartmentController _controller;
        private readonly LevelController _levelController;
        private readonly StudentView _student;
        string[] options = { "Add Department", "View Department Details", "FindById", "DepartmentWithProfessores", "DepartmentWithCourse", "DepartmentWithStudent", "Update", "Exit" };
        public DepartmentView(DepartmentController controller, LevelController levelController)
        {
            _controller = controller;
            _levelController = levelController;
        }


        #region Main Function 
        public async Task Main() // Change void to Task
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                var option = Layout.DisplayMenu(Helper.DepartmentLayOut.MANAGEMNT_DEPARTMENT_SYSTEM.ToString().Replace('_', ' '), options);

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        //await AddEnrollmentView();
                        break;
                    case "2":
                        Console.Clear();
                        await ShowDep();
                        break;
                    case "3": //Find BY id
                        Console.Clear();
                        //await FindEnrollmentView();
                        break;
                    case "4": //EnrollmentsWithLevel
                        Console.Clear();
                        await ShowProfessorWithDep();

                        break;
                    case "5": //EnrollmentsWithCourses
                        Console.Clear();
                        await ShowCouuresWithDep();
                        break;
                    case "6": //EnrollmentsWithStudent
                        Console.Clear();
                        await ShowStudentWithDep();
                        break;
                    case "7": //Find BY id
                        //await UpdateEnrollmentView();
                        break;
                    case "8":
                        exit = true;
                        break;



                }
            }
        }
        #endregion

        public async Task ShowDep()
        {
            Layout.DisplayTital(Helper.GenericLayOut.ENTER_ID_DETALIS.ToString().Replace('_', ' '));

            var selectlevel = Layout.SelectItem(await _levelController.GetLevels(), "Level");
            if (selectlevel!=-2)
            {
                var dep = await _controller.GetDep(selectlevel);
                if (dep != null)
                {
                    Layout.Dispaly(dep, "Department");
                    Console.ReadKey();
                }
                else
                {
                    Layout.ShowMessageLine("Not Avaliable Department", ConsoleColor.Red);
                }
            }
           

        }

        private async Task<int> SelectDep()
        {
           return  Layout.SelectItem(await _controller.GetDep(), "Level");
        }

        public async Task ShowStudentWithDep()
        {

            Layout.DisplayTital(Helper.GenericLayOut.ENTER_ID_DETALIS.ToString().Replace('_', ' '));

            var selectlevel = Layout.SelectItem(await _levelController.GetLevels(), "Level");
            if (selectlevel != -2)
            {

                var SELECTDEP = Layout.SelectItem(await _controller.GetDep(selectlevel), "Department");
                if (SELECTDEP != -2)
                {
                    var students = await _controller.GetStudentWithDep(SELECTDEP);
                    if (students.Count > 0)
                    {
                        Layout.DispalyStudent(students, "STUDENT");
                        Console.ReadKey();
                    }
                    else
                    {
                        Layout.ShowMessageLine("Not Avaliable STUDENT", ConsoleColor.Red);
                        Console.ReadKey();
                    }
                }
                else
                {
                    Layout.ShowMessageLine("Not Avaliable Department", ConsoleColor.Red);
                    Console.ReadKey();
                }
            }
            else
            {
                Layout.ShowMessageLine("Not Avaliable Level", ConsoleColor.Red);
                Console.ReadKey();

            }
        }

        public async Task ShowCouuresWithDep()
        {

            var SELECTDEP = await SelectDep();
            if (SELECTDEP != -2)
            {
                var Coures = await _controller.GetCoursesWithDep(SELECTDEP);
                if (Coures.Count > 0)
                {
                    Layout.Dispaly(Coures, "Coures");
                    Console.ReadKey();
                }
                else
                {
                    Layout.ShowMessageLine("Not Avaliable Coures", ConsoleColor.Red);
                }
            }
            else
            {
                Layout.ShowMessageLine("Not Avaliable Department", ConsoleColor.Red);
            }

        }

        public async Task ShowProfessorWithDep()
        {

            var SELECTDEP = await SelectDep();
            if (SELECTDEP != -2)
            {
                var Professores = await _controller.GetProfessorsWithDep(SELECTDEP);
                if (Professores.Count > 0)
                {
                    Layout.DispalyStudent(Professores, "Professores");
                    Console.ReadKey();
                }
                else
                {
                    Layout.ShowMessageLine("Not Avaliable Professores", ConsoleColor.Red);
                    Console.ReadKey();
                }
            }
            else
            {
                Layout.ShowMessageLine("Not Avaliable Department", ConsoleColor.Red);
                Console.ReadKey();
            }

        }

       

    }

}
