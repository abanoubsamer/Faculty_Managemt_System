using Domin.Helper;
using Domin.Models;
using Domin.Models.ViewModel;
using Program.Applicaction.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Program.Applicaction.View
{
    public class SectionView
    {
        private readonly SectionController _controller;
        private readonly DepartmentController _depcontroller;
        private readonly LevelController _Levelcontroller;


        
        string[] options = { "Add Section", "View Section Details", "Get Student With Section", "Get Prfessores With Section", "Add Professor In Section", "Update", "Delete", "Exit" };
        public SectionView(SectionController controller, DepartmentController dep, LevelController levelcontroller)
        {
            _controller = controller;
            _depcontroller = dep;
            _Levelcontroller = levelcontroller;
        }

        #region Main Function 
        public async Task Main() // Change void to Task
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                var option = Layout.DisplayMenu(Helper.SectionLayOut.MANAGEMNT_SECTION_SYSTEM.ToString().Replace('_', ' '), options);

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        await AddSectionAsync();
                        break;
                    case "2":
                        Console.Clear();
                        await GetSectionAsync();
                        break;
                    case "3": //Find BY id
                        Console.Clear();
                        await ShowStudentWithSectionAsync();
                        break;
                    case "4":
                        Console.Clear();
                     await ShowProfessorsWithSectionAsync();
                        break;
                    case "5":
                        Console.Clear();
                        await AddProfessorInSection();
                        break;
                    case "6":
                      
                        break;
                    case "7":
                    
                        break;
                    case "8":
                        exit = true;
                        break;



                    default:
                        Layout.ShowMessage("Invalid Option", ConsoleColor.Red);
                        break;
                }
            }
        }

        #endregion





        private async Task GetSectionAsync()
        {
            var selectLevel = await SelectLevelAsync();
            if (ValidateSelection(selectLevel, "Level"))
            {
                var selectDep = await SelectDepAsync(selectLevel);
                if (ValidateSelection(selectDep, "Department"))
                {
                    var section = await _controller.GetSection(selectDep, selectLevel);
                    ValidateAndDisplaySection(section);
                }
            }
        }
        private async Task<int> SelectLevelAsync()
        {
            DisplayTitle("ENTER ID DETAILS");
            var levels = await _Levelcontroller.GetLevels();
            return Layout.SelectItem(levels, "Level");
        }
        private async Task<int> SelectDepAsync(int levelId)
        {
            DisplayTitle("ENTER ID DETAILS");
            var departments = await _depcontroller.GetDep(levelId);
            return Layout.SelectItem(departments, "Department");
        }
        private async Task<int> SelectSectionAsync(int depId, int levelId)
        {
            DisplayTitle("ENTER ID DETAILS");
            var sections = await _controller.GetSection(depId, levelId);
            return Layout.SelectItem(sections, "Section");
        }
        private async Task<int> SelectProfessorAsync(int depId)
        {

            DisplayTitle("ENTER ID DETAILS");
            var professors = await _controller.GetProfessorsWithDep(depId);
            if (professors != null)
            {
                Layout.DispalyStudent(professors, "Professor");
                var selecteddeplIndex = Layout.ValidationSelectInput(professors, "Professor");


                if (selecteddeplIndex != -2)
                {
                    Console.Clear();
                    Layout.DisplayTital(Helper.GenericLayOut.ENTER_ID_DETALIS.ToString().Replace('_', ' '));
                    Layout.ShowMessageLine("================================", ConsoleColor.Magenta);
                    Layout.ShowMessage($"Department", ConsoleColor.Cyan);
                    Layout.ShowMessage($"[", ConsoleColor.Green);
                    Layout.ShowMessage($"{professors[selecteddeplIndex].FirstName}", ConsoleColor.Yellow);
                    Layout.ShowMessageLine($"]", ConsoleColor.Green);
                    Layout.ShowMessageLine("================================", ConsoleColor.Magenta);
                    return professors[selecteddeplIndex].Id;

                }
            }


            return -1;


         

        

           


        }
        private async Task<int> SelectCouresWithProfessorAsync(int proid,int depId,int levelid)
        {
            DisplayTitle("ENTER ID DETAILS");
            var coures = await _controller.GetCoursesProfessorWithDep(proid,depId,levelid);
            return Layout.SelectItem(coures, "Courses");

        }
        private async Task AddSectionAsync()
        {
            var selectLevel = await SelectLevelAsync();
            if (ValidateSelection(selectLevel, "Level"))
            {
                var selectDep = await SelectDepAsync(selectLevel);
                if (ValidateSelection(selectDep, "Department"))
                { 
                    var exit = false;
                    while (!exit)
                    {
                        Console.Write("Enter New Name Section: ");
                        var name = Layout.PromptFoStringInput(Console.ReadLine(), "Name");
                        if (name == "EXITE")
                        {     
                            exit=true;
                        }else if (name!=string.Empty)
                        {
                            var section = new Domin.Models.Section { LevelId = selectLevel, DepartmentId = selectDep, Name = name };
                            await DisplayResultAsync(_controller.AddSection(section), "Adding New Section");
                            exit = true;
                        }

                    }

                  
                }
            }
        }
        private async Task ShowStudentWithSectionAsync()
        {
            var selectLevel = await SelectLevelAsync();
            if (ValidateSelection(selectLevel, "Level"))
            {
                var selectDep = await SelectDepAsync(selectLevel);
                if (ValidateSelection(selectDep, "Department"))
                {
                    var selectSection = await SelectSectionAsync(selectDep, selectLevel);
                    if (ValidateSelection(selectSection, "Section"))
                    {
                        var students = await _controller.GetStudentWithSection(selectSection);
                        ValidateAndDisplayStudents(students);
                    }
                }
            }
        }
        private async Task ShowProfessorsWithSectionAsync()
        {
            var selectLevel = await SelectLevelAsync();
            if (ValidateSelection(selectLevel, "Level"))
            {
                var selectDep = await SelectDepAsync(selectLevel);
                if (ValidateSelection(selectDep, "Department"))
                {
                    var selectSection = await SelectSectionAsync(selectDep, selectLevel);
                    if (ValidateSelection(selectSection, "Section"))
                    {
                        var professors = await _controller.GetProfessorsWithSection(selectSection);
                        ValidateAndDisplayProfessors(professors);
                    }
                }
            }
        }
        private bool ValidateSelection(int selection, string itemName)
        {
            if (selection == -1)
            {
                Layout.ShowMessage($"{itemName} Not Available", ConsoleColor.Red);
                Console.ReadKey();
                return false;
            }
            return true;
        }
        private void ValidateAndDisplaySection<T>(List<T>  section) where T: class
        {
            if (section != null)
            {
                Layout.Dispaly(section, "SECTION");
                Console.ReadKey();
            }
            else
            {
                Layout.ShowMessage("No SECTION Available", ConsoleColor.Red);
                Console.ReadKey();
            }
        }
        private void ValidateAndDisplayStudents(List<Student> students)
        {
            if (students != null && students.Any())
            {
                Layout.DispalyStudent(students, "Students");
                Console.ReadKey();
            }
            else
            {
                Layout.ShowMessage("No Students Available", ConsoleColor.Red);
                Console.ReadKey();
            }
        }
        private void ValidateAndDisplayProfessors(List<ProfessorShowViewModel> professors)
        {
            if (professors != null && professors.Any())
            {
                Layout.DispalyProfessor(professors, "Professors");
                Console.ReadKey();
            }
            else
            {
                Layout.ShowMessage("No Professors Available", ConsoleColor.Red);
                Console.ReadKey();
            }
        }
        private async Task DisplayResultAsync(Task<bool> task, string operationName)
        {
            if (await task)
            {
                Layout.ShowMessageLine($"{operationName} Succeeded", ConsoleColor.Green);
            }
            else
            {
                Layout.ShowMessageLine($"{operationName} Failed", ConsoleColor.Red);
            }
            Console.ReadKey();
        }
        private void DisplayTitle(string title)
        {
            Layout.DisplayTital(title.Replace('_', ' '));
        }
        private async Task AddProfessorInSection()
        {

          var selectLevel = await SelectLevelAsync();
            if (ValidateSelection(selectLevel, "Level"))
            {
                var selectDep = await SelectDepAsync(selectLevel);
                if (ValidateSelection(selectDep, "Department"))
                {
                    var selectsection = await SelectSectionAsync(selectDep, selectLevel);
                    if (ValidateSelection(selectsection, "SECTION"))
                    {
                        var selectPro = await SelectProfessorAsync(selectDep);
                        if (ValidateSelection(selectPro, "Professor"))
                        {
                            var selectCoureses = await SelectCouresWithProfessorAsync(selectPro, selectDep, selectLevel);
                            if (ValidateSelection(selectCoureses, "Coures"))
                            {

                                var Techby = new TeachBy
                                {
                                    CourseId = selectCoureses,
                                    ProfessorId = selectPro,
                                    SectionId = selectsection,
                                };
                                await DisplayResultAsync(_controller.AddProfessorInSection(Techby), "Adding New Professor In Section");


                            }

                        }

                    }

                }
            }
        }



    }




}
