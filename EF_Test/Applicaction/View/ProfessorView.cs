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
    public class ProfessorView
    {
        private readonly ProfessorController _controller;
        private readonly DepartmentController _Depcontroller;
        private readonly LevelController _Levelcontroller;
       
      
        string[] options = { "Add Professor", "View Professor Details", "FindById", "Get Student With Professores", "Get Department With Professores", "Get Courses With Professores","Add Professor In Department", "Add Coures To Professor ", "Exit" };
        public ProfessorView(ProfessorController controller, DepartmentController depcontroller,LevelController levelController)
        {
            _controller = controller;
            _Levelcontroller = levelController;
            _Depcontroller = depcontroller;
        }


        #region Main Function 
        public async Task Main() // Change void to Task
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                var option = Layout.DisplayMenu(Helper.ProfessoresLayOut.MANAGEMNT_PROFESSORS_SYSTEM.ToString().Replace('_', ' '), options);

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        await GetStudentInputForm();
                        break;
                    case "2":
                        Console.Clear();
                        DisplayStudentDetails(await _controller.GetProfessorsAsync());
                        break;
                    case "3": //Find BY id
                        Console.Clear();
                      
                        break;
                    case "4": //EnrollmentsWithLevel
                        Console.Clear();
                        await GetStudentsWithProfessor();

                        break;
                    case "5": //EnrollmentsWithCourses
                        Console.Clear();
                        await GetDepWithProfessor();
                        break;
                    case "6": //EnrollmentsWithStudent
                        Console.Clear();
                        await GetCoursesWithProfessor();
                        break;
                    case "7": //Update
                        Console.Clear();
                        await AddProfessorInDep();
                        break;
                    case "8":
                        Console.Clear();
                        await AddCouresProfessor();
                        break;
                    case "9":
                        exit = true;
                        break;

                       

                }
            }
        }
        #endregion
       
      

        #region GetInputStudent
        public async Task GetStudentInputForm()
        {
            // Create an array to hold the student data
            string[] studentData = new string[5];
            var newStudent = new Student();

            while (true)
            {
                DisplayStudentInputForm(studentData); // Show current input form

                var key = Console.ReadKey(true);
                if (key.Key >= ConsoleKey.NumPad0 && key.Key <= ConsoleKey.NumPad4)
                {
                    int index = key.Key - ConsoleKey.NumPad0; // Get the index of the input field to Zero Index
                    Console.Clear();
                    var fieldName = GetFieldName(index);

                    await HandleField(newStudent, fieldName, index, studentData);

                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    // Create the student object with the filled data
                    var Professor = new Professor
                    {
                        FirstName = studentData[0],
                        LastName = studentData[1],
                        Email = studentData[2],
                        Phone = studentData[3],
                        Salary= decimal.Parse(studentData[4]) 
                       
                    };

                    var validationResults = ModelStat.IsValid(Professor);
                    if (validationResults.Count == 0)
                    {
                        // Call the controller method to add the student
                        if (await _controller.AddProfessor(Professor))
                        {
                            Layout.ShowMessageLine("Professor added successfully!", ConsoleColor.Green);
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
        private async Task HandleField(Student student, string fieldName, int index, string[] studentData)
        {
            Layout.ShowMessageLine($"Current value for {fieldName}: {Layout.FormatField(studentData[index])}", ConsoleColor.Cyan);
            Layout.ShowMessageLine($"Enter new value for {fieldName}: ", ConsoleColor.Cyan);
            Console.WriteLine();


            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                studentData[index] = input;
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
                0 => "First Name",
                1 => "Last Name",
                2 => "Email",
                3 => "Phone",
                4 => "Salaray",
                _ => ""
            };
        }
        #endregion



        #region ShowInput
        private void DisplayStudentInputForm(string[] studentData)
        {
            Layout.DisplayTital(Helper.GenericLayOut.ENTER_DATA_DETAILS.ToString().Replace('_', ' '));

            Console.WriteLine($"0. First Name: {Layout.FormatField(studentData[0])}");
            Console.WriteLine($"1. Last Name: {Layout.FormatField(studentData[1])}");
            Console.WriteLine($"2. Email: {Layout.FormatField(studentData[2])}");
            Console.WriteLine($"3. Phone: {Layout.FormatField(studentData[3])}");
            Console.WriteLine($"4. Salary: {Layout.FormatField(studentData[4])}");
            Console.WriteLine("=======================================");
            Console.WriteLine("Press the number of the field to edit it, or press Enter to save.");
        }
        #endregion



        #region ShowStudent
        public void DisplayStudentDetails(List<Professor> Professors)
        {
            Layout.DisplayTital(Helper.ProfessoresLayOut.DATA_PROFESSORS_DETALIS.ToString().Replace('_', ' '));

            if (Professors.Count == 0)
            {
                Layout.ShowMessageLine("No Professors available.", ConsoleColor.Yellow);
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            // Display table header with corrected formatting
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═════╦════════════════════════╦════════════════════════════╦═════════════════╦═════════════════╗");
            Console.WriteLine("║ ID  ║ Name                   ║ Email                      ║ Phone           ║ Salary          ║");
            Console.WriteLine("╠═════╬════════════════════════╬════════════════════════════╬═════════════════╬═════════════════╣");

            // Display each student's information
            foreach (var Professor in Professors)
            {
                DisplayStudentInfo(Professor);
            }

            // Display table footer
            Console.WriteLine("╚═════╩════════════════════════╩════════════════════════════╩═════════════════╩═════════════════╝");

            // Prompt user to return to the menu
            Console.WriteLine();
            Layout.ShowMessageLine("Press any key to return to the menu...", ConsoleColor.Yellow);
            Console.ReadKey();
        }

        private void DisplayStudentInfo(Professor Professor)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"║ {Professor.Id,-3} ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"║ {Professor.FirstName + " " + Professor.LastName,-22} ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"║ {Professor.Email,-26} ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"║ {Professor.Phone,-15} ");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"║ {Professor.Salary,-15} ║");

           

            Console.ResetColor();
        }

        #endregion



        #region Get Students With Professor
        private async Task GetStudentsWithProfessor()
        {
            Layout.DisplayTital("ENTER PROFESSOR ID");
            Layout.ShowMessage("Enter Id For Professor : ", ConsoleColor.Cyan);
            var id = Layout.PromptForIntInput(Console.ReadLine(), "ID");
            if (id != -2)
            {
                var students = await _controller.GetStudentsWithProfessor(id);
                if (students.Count > 0)
                {
                    Layout.DispalyStudent(students,"STUDENT");
                    Console.ReadKey();
                }
                else
                {
                    Layout.ShowMessageLine("Not Student Avalably is Professor",ConsoleColor.Red);
                    Console.ReadKey();
                }
            }
            
        }


        #endregion

        #region Get Courses With Professor
        private async Task GetCoursesWithProfessor()
        {
            Layout.DisplayTital("ENTER PROFESSOR ID");
            Layout.ShowMessage("Enter Id For Professor : ", ConsoleColor.Cyan);
            var id = Layout.PromptForIntInput(Console.ReadLine(), "ID");
            if (id != -2)
            {
                var Courses = await _controller.GetCoursesWithProfessor(id);
                if (Courses.Count > 0)
                {
                    Layout.Dispaly(Courses, "Courses");
                    Console.ReadKey();
                }
                else
                {
                    Layout.ShowMessageLine("Not Courses Avalably is Professor", ConsoleColor.Red);
                    Console.ReadKey();
                }
            }

        }


        #endregion


        #region Get Dep With Professor
        private async Task GetDepWithProfessor()
        {
            Layout.DisplayTital("ENTER PROFESSOR ID");
            Layout.ShowMessage("Enter Id For Professor : ", ConsoleColor.Cyan);
            var id = Layout.PromptForIntInput(Console.ReadLine(), "ID");
            if (id != -2)
            {
                var Department = await _controller.GetDepartmentsWithProfessor(id);
                if (Department.Count > 0)
                {
                    Layout.Dispaly(Department, "Department");
                    Console.ReadKey();
                }
                else
                {
                    Layout.ShowMessageLine("Not Department Avalably is Professor", ConsoleColor.Red);
                    Console.ReadKey();
                }
            }

        }


        #endregion

        #region AddProfessorInDep

        public async Task AddProfessorInDep()
        {
            Layout.DisplayTital("ENTER DATA OF PROFESSOR");
            Layout.ShowMessage("Enter Professor Id : ", ConsoleColor.Cyan);
            var professorid = Layout.PromptForIntInput(Console.ReadLine(), "ID");
            if (professorid != -2)
            {
                var levels = await _Levelcontroller.GetLevels();
                var selectlevel= Layout.SelectItem(levels, "Level");
                if (selectlevel != -1)
                {
                    var Dep =  await _Depcontroller.GetDep(selectlevel);
                    var selectdep = Layout.SelectItem(Dep, "Department");
                    if (selectdep != -1)
                    {
                        
                        if(await _controller.AddProfesserInDep(new TeachIn{ DepartmentId = selectdep,ProfessorId = professorid, }))
                        {
                            Layout.ShowMessageLine("Succed Add Professor In Department", ConsoleColor.Green);
                            Console.ReadKey();
                        }else
                        {
                            Layout.ShowMessageLine("Error Add Professor In Department", ConsoleColor.Red);
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
        }


        #endregion
        #region AddCouresProfessor
        public async Task AddCouresProfessor()
        {
            Layout.DisplayTital("ENTER DATA OF PROFESSOR");
            Layout.ShowMessage("Enter Professor Id : ", ConsoleColor.Cyan);
            var professorid = Layout.PromptForIntInput(Console.ReadLine(), "ID");
            if (professorid != -2)
            {
                var levels = await _Levelcontroller.GetLevels();
                var selectlevel = Layout.SelectItem(levels, "Level");
                if (selectlevel != -1)
                {
                    var Coures = await _controller.GetCoursesWithLevel(selectlevel);
                    var selectdCoures = Layout.SelectItem(Coures, "Coures");
                    if (selectdCoures != -1)
                    {

                        if (await _controller.AddCouresProfesser(new TeachTo {  CoursesId=selectdCoures, ProfessorsId= professorid }))
                        {
                            Layout.ShowMessageLine("Succed Add Coures Professer", ConsoleColor.Green);
                            Console.ReadKey();
                        }
                        else
                        {
                            Layout.ShowMessageLine("Error Add Coures Professer", ConsoleColor.Red);
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Layout.ShowMessageLine("Not Avaliable Coures", ConsoleColor.Red);
                        Console.ReadKey();
                    }
                }
                else
                {
                    Layout.ShowMessageLine("Not Avaliable Level", ConsoleColor.Red);
                    Console.ReadKey();
                }

            }
        }
        #endregion


    }
}
