using Domin.Helper;
using Domin.Models;
using Infrastructure.ValidateModel;
using Program.Applicaction.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Program.Applicaction.View
{
    public class LevelView
    {
        private readonly LevelController _controller;
        private readonly StudentView _student;
      
        string[] options = { "Add Level", "View Level Details", "FindById", "Update" ,"Show Department With Level", "Show Student With Level" ,"Show Courses With Level", "Exit" };
        public LevelView(LevelController controller, StudentView student)
        {
            _controller = controller;
            _student = student;
        }

        #region Main Function 
        public async Task Main() // Change void to Task
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                var option = Layout.DisplayMenu(Helper.LevelLayOut.MANAGEMNT_LEVEL_SYSTEM.ToString().Replace('_',' '), options);

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        await GetLevelInputForm();
                        break;
                    case "2":
                        Console.Clear();
                          await  GetLevel();
                    
                        break;
                    case "3": //Find BY id
                        await showLevelById();
                        break;
                    case "4": //Updata
                        //await UpdateStudent();
                        break;
                    case "5"://Show Department With Level

                        break;
                    case "6"://Show Student With Level
                        await ShowStudetWithLevel();
                        break;
                    case "7"://Show Courses With Level

                        break;
                    case "8":
                        exit = true;
                        break;



                }
            }
        }
        #endregion

        #region GetInput
        public async Task GetLevelInputForm()
        {
            // Create an array to hold the student data
            string levelname = string.Empty;
            var level = new Level();
            while (true)
            {
                DisplayLevelInputForm(levelname); // Show current input form

                var key = Console.ReadKey(true);
                if (key.Key >= ConsoleKey.NumPad1 && key.Key <= ConsoleKey.NumPad9)
                {
                    int index = key.Key - ConsoleKey.NumPad1; // Get the index of the input field
                    Console.Clear();
                    Layout.ShowMessage($"Current value for Name: {Layout.FormatField(levelname)}", ConsoleColor.Cyan);

                    Console.WriteLine($"Enter new value for Name: ");

                    levelname = Layout.PromptFoStringInput(Console.ReadLine(), "Name");
                    if (levelname == string.Empty) break;

                    level.Name = levelname;

                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {


                    var validationResults = ModelStat.IsValid(level);
                    if (validationResults.Count == 0)
                    {
                        // Call the controller method to add the student
                        if (await _controller.AddLevelController(level))
                        {
                            Layout.ShowMessage("Level added successfully!", ConsoleColor.Green);
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
                        Layout.ShowMessage("Please correct the input errors and try again.", ConsoleColor.Red);
                        Console.ReadKey();
                    }
                }
            }
        }
        #endregion

        #region ShowInput
        private void DisplayLevelInputForm(string name)
        {
            Layout.DisplayTital(Helper.GenericLayOut.ENTER_DATA_DETAILS.ToString().Replace('_',' '));

            Console.WriteLine($"1. Name: {Layout.FormatField(name)}");
            Console.WriteLine("=======================================");
            Console.WriteLine("Press the number of the field to edit it, or press Enter to save.");
        }
        #endregion


        #region FindById
        private async Task showLevelById()
        {
            List<Level> levels = new List<Level>();
            var level = await GetById();
            if (level != null)
            {
                levels.Add(level);
                Layout.Dispaly(levels,"LEVEL");
            }

        }

        public async Task<Level> GetById()
        {
            bool exit = false;
            Level level = null;

            while (!exit)
            {
                Layout.DisplayTital(Helper.GenericLayOut.ENTER_ID_DETALIS.ToString().Replace('_', ' '));

                Layout.ShowMessageLine("Please Enter Level ID:", ConsoleColor.Cyan);

                //validation Id
                int id = Layout.PromptForIntInput(Console.ReadLine(), "ID");

                // Press Esc
                if (id == -1)
                {
                    break;
                }

                level = await _controller.FindByIdController(id);


                if (level == null)
                {
                    Layout.ShowMessageLine("Level ID not found.", ConsoleColor.Red);

                    Layout.ShowMessageLine("Press [X] to try again or [Enter] to exit.", ConsoleColor.Yellow);

                    var key = Console.ReadKey(intercept: true);

                    if (key.Key == ConsoleKey.Enter)
                    {
                        exit = true;
                    }
                    else if (key.Key == ConsoleKey.X)
                    {
                        Console.Clear();
                    }
                }
                else
                {
                    exit = true; // Student found, exit the loop
                }
            }

            return level;

        }
        #endregion


        #region ShowLevel
       

      
        #endregion

        #region GetLevel

        public async Task GetLevel()
        {
            var level = await _controller.GetLevels();
            if(level==null|| level.Count == 0)
            {
                Layout.ShowMessage("Not Level available Now ", ConsoleColor.Yellow);
                Console.ReadKey();
            }
            else
            {
                Layout.DisplayTital(Helper.StudentLayOut.DATA_STUDENT_DETALIS.ToString().Replace('_', ' '));
                Layout.Dispaly(level, "Level");
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();

            }
        }

        #endregion


        public async Task ShowStudetWithLevel()
        {
            var level = await _controller.GetLevels();

            Layout.Dispaly(level, "Level");

            Console.Write("Enter Level ID: ");
            int selectedLevelIndex = Layout.ValidationSelectInput(level, "Level");

            //Select Esc
            if (selectedLevelIndex != -2)
            {
                var student=await _controller.GetStudentsWithLevel(level[selectedLevelIndex].Id) ;
                if (student.Count == 0)
                {
                    Layout.ShowMessageLine($"Not Student Avalable In Level [{level[selectedLevelIndex].Name}]", ConsoleColor.Red);
                
                }
                else
                {
                    _student.DisplayStudentDetails(student);
                }
            }

        
            Console.ReadKey();

        }

        





    }
}
