using Domin.Helper;
using Domin.Models;
using Infrastructure.ValidateModel;
using Program.Applicaction.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Applicaction.View
{
    public class CouresView
    {
        private readonly CouresController _controller;
        private readonly LevelController _Levelcontroller;
        private readonly DepartmentController _DepartmentController;
 
        string[] options = { "Add Coures", "View Coures Details", "FindById", "Add Coures In Department", "Get Coures With Level", "Get Coures With Department", "Update", "Exit" };
        public CouresView(CouresController controller,LevelController levelController, DepartmentController departmentController)
        {
            _controller = controller;
            _Levelcontroller = levelController;
            _DepartmentController = departmentController;
        }


        #region Main Function 
        public async Task Main() // Change void to Task
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                var option = Layout.DisplayMenu(Helper.CouresLayOut.MANAGEMNT_COURES_SYSTEM.ToString().Replace('_', ' '), options);

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        await GetStudentInputForm();
                        break;
                    case "2":
                        Console.Clear();
                        DisplayCourestDetails(await _controller.GetCourses());
                        break;
                    case "3": //Find BY id
                        Console.Clear();
                        //await FindEnrollmentView();
                        break;
                    case "4": //EnrollmentsWithLevel
                        Console.Clear();
                        await AddCouresInDep();
                        break;
                    case "5": //EnrollmentsWithCourses
                        Console.Clear();
                        await GetCouresWithLevel();
                        break;
                    case "6": //EnrollmentsWithStudent
                        Console.Clear();
                        await GetCouresWithDep();
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

        #region AddCoures
        public async Task GetStudentInputForm()
        {
            // Create an array to hold the student data
            string[]  CouresData = new string[3];
            var newCoures = new Course();

            while (true)
            {
                DisplayStudentInputForm(CouresData); // Show current input form

                var key = Console.ReadKey(true);
                if (key.Key >= ConsoleKey.NumPad0 && key.Key <= ConsoleKey.NumPad2)
                {
                    int index = key.Key - ConsoleKey.NumPad0; // Get the index of the input field to Zero Index
                    Console.Clear();
                    var fieldName = GetFieldName(index);

                    await HandleField(newCoures, fieldName, index, CouresData);

                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    // Create the student object with the filled data
                    var Course = new Course
                    {
                        Name = CouresData[0],
                        Hours = int.Parse(CouresData[1]) ,
                        LevelId = newCoures.LevelId,
                      

                    };

                    var validationResults = ModelStat.IsValid(Course);
                    if (validationResults.Count == 0)
                    {
                        // Call the controller method to add the student
                        if (await _controller.AddCoures(Course))
                        {
                            Layout.ShowMessageLine("Course added successfully!", ConsoleColor.Green);
                            Console.ReadKey();
                            break; // Exit after adding
                        }
                    }
                    else
                    {
                        foreach (var validationResult in validationResults)
                        {
                            Console.WriteLine(validationResult.ErrorMessage);
                        }
                        Layout.ShowMessageLine("Please correct the input errors and try again.", ConsoleColor.Red);
                        Console.ReadKey();
                    }
                }
            }
        }
        public async Task<Course> SelectLevel(Course course)
        {
            // Retrieve the levels from the controller
            var levels = await _Levelcontroller.GetLevels();

            if (levels == null || levels.Count == 0)
            {
                Layout.ShowMessageLine("No levels available.", ConsoleColor.Red);
                Console.ReadKey();
                return course;

            }

            //DisplayLevel
            Layout.Dispaly(levels, "LEVEL");

            //validiation Select level
            int selectedLevelIndex = Layout.ValidationSelectInput(levels, "LEVEL");

            //Select Esc
            if (selectedLevelIndex != -2)
            {
                // Set the selected level with confirmation message in color
                var selectedLevel = levels[selectedLevelIndex];
                Layout.ShowMessageLine($"\nYou selected: {selectedLevel.Name}\n", ConsoleColor.Green);
                course.LevelId = selectedLevel.Id;
                course.Level = new Level { Id = selectedLevel.Id, Name = selectedLevel.Name };
            }

            return course;
        }
        private async Task HandleField(Course newCoures, string fieldName, int index, string[] CouresData)
        {
            Layout.ShowMessageLine($"Current value for {fieldName}: {Layout.FormatField(CouresData[index])}", ConsoleColor.Cyan);
            Layout.ShowMessageLine($"Enter new value for {fieldName}: ", ConsoleColor.Cyan);
            Console.WriteLine();
          
            if (fieldName == "Level ID")
            {
                newCoures = await SelectLevel(newCoures);
                CouresData[index] = Layout.FormatField(newCoures?.Level?.Name);
            }
        
            else {
                var input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                CouresData[index] = input;
            }
        }
        private static decimal PromptForDecimalInput(string intput, string message)
        {
            Console.Write(message);
            decimal result;
            while (!decimal.TryParse(intput, out result))
            {
                Console.WriteLine("Invalid input. Please enter a valid decimal.");
                Console.Write(message);
            }
            return result;
        }
        private static string GetFieldName(int index)
        {
            return index switch
            {
                0 => "Coures Name",
                1 => "Houres Name",
                2 => "Level ID",
                _ => ""
            };
        }
        private void DisplayStudentInputForm(string[] CouresData)
        {
            Layout.DisplayTital(Helper.GenericLayOut.ENTER_DATA_DETAILS.ToString().Replace('_', ' '));

            Console.WriteLine($"0. Coures Name: {Layout.FormatField(CouresData[0])}");
            Console.WriteLine($"1. Houres Name: {Layout.FormatField(CouresData[1])}");
            Console.WriteLine($"2. Level ID: {Layout.FormatField(CouresData[2])}");
            Console.WriteLine("=======================================");
            Console.WriteLine("Press the number of the field to edit it, or press Enter to save.");
        }
        #endregion


        #region GetCoures

        #region ShowCoures
        public void DisplayCourestDetails(List<Course> courses)
        {
            Layout.DisplayTital(Helper.CouresLayOut.DATA_COURES_DETALIS.ToString().Replace('_', ' '));

            if (courses.Count == 0)
            {
                Layout.ShowMessageLine("No Coures available.", ConsoleColor.Yellow);
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            // Display table header with corrected formatting
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═════╦════════════════════════╦════════════════════════════╦═════════════════╗");
            Console.WriteLine("║ ID  ║ NAME                   ║ LEVEL                      ║  HOURS          ║");
            Console.WriteLine("╠═════╬════════════════════════╬════════════════════════════╬═════════════════╣");

            // Display each student's information
            foreach (var course in courses)
            {
                DisplayCouresInfo(course);
            }

            // Display table footer
            Console.WriteLine("╚═════╩════════════════════════╩════════════════════════════╩═════════════════╝");

            // Prompt user to return to the menu
            Console.WriteLine();
            Layout.ShowMessageLine("Press any key to return to the menu...", ConsoleColor.Yellow);
            Console.ReadKey();
        }

        private void DisplayCouresInfo(Course course)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"║ {course.Id,-3} ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"║ {course.Name ,-22} ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"║ {course.Level.Name,-26} ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"║ {course.Hours,-15} ║");

            Console.ResetColor();
        }

        #endregion



        #endregion


        #region Add Coures In Department
        public async Task<Course> SelectCoures(List<Course> course)
        {
            var selectedCoures = new Course();

            if (course == null || course.Count == 0)
            {
                Layout.ShowMessageLine("No levels available.", ConsoleColor.Red);
                Console.ReadKey();
                return null;

            }

            //DisplayLevel
            Layout.Dispaly(course, "COURES");

            //validiation Select level
            int selectedLevelIndex = Layout.ValidationSelectInput(course, "COURES");

            //Select Esc
            if (selectedLevelIndex != -2)
            {
                selectedCoures = course[selectedLevelIndex];
            }

            return selectedCoures;
        }
        private async Task<Course> SelectCoures()
           {
                return await SelectCoures(await _controller.GetCourses());
           }
           private async Task<int> SelectDep(int levelid)
           {
                return Layout.SelectItem(await _DepartmentController.GetDep(levelid), "Department");
           }

            private async Task AddCouresInDep()
            {
                Layout.DisplayTital("ADD COURES IN DEP");
                var selectcoures = await SelectCoures();
                    if (selectcoures.Id > 0)
                    {
                         var selectdep = await SelectDep(selectcoures.LevelId);
                            if (selectdep != -1)
                            {      
                               if (await _controller.AddCouresInDepartemnt(new CourseDepartment { DepartmentsId = selectdep, CoursesId = selectcoures.Id }))
                               {
                                    Layout.ShowMessageLine("Successd Add Coures In Department", ConsoleColor.Green);
                                        Console.ReadKey();
                               }
                               else
                               {
                        
                                    Layout.ShowMessageLine("Error Add Coures In Department", ConsoleColor.Red);
                                        Console.ReadKey();
                               }
                            }
                            else
                            {

                                Layout.ShowMessageLine("Not Avaluable Department", ConsoleColor.Red);
                                Console.ReadKey();
                            }

                    }
                    else
                    {

                        Layout.ShowMessageLine("Not Avaluable Coures", ConsoleColor.Red);
                        Console.ReadKey();
                    }

        }

        #endregion


        #region Get Coures With Level

        private async Task GetCouresWithLevel()
        {
            Layout.DisplayTital("ADD COURES IN DEP");
            var selectLevel =  Layout.SelectItem(await _Levelcontroller.GetLevels(),"Level");
            if (selectLevel != -1)
            {
                DisplayCourestDetails(await _controller.GetCourses(selectLevel));
            }
            else
            {
                Layout.ShowMessageLine("Not Avaliable Level", ConsoleColor.Red);
                Console.ReadKey();
            }        
        }
        #endregion

        #region Get Coures With Dep
        private async Task GetCouresWithDep()
        {
            Layout.DisplayTital("ADD COURES IN DEP");
            var selectLevel = Layout.SelectItem(await _Levelcontroller.GetLevels(), "Level");
            if (selectLevel != -1)
            {
                Layout.DisplayTital("ADD COURES IN DEP");
                var selectdep = Layout.SelectItem(await _DepartmentController.GetDep(selectLevel), "Department");
                if (selectdep!=-1)
                {
                    DisplayCourestDetails(await _controller.GetCourses(selectLevel,selectdep));
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
        #endregion
    }
}
