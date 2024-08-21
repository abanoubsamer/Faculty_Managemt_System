using Azure;
using Domin.Helper;
using Domin.Models;
using Domin.Models.ViewModel;
using Infrastructure.ValidateModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Program.Applicaction.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Program.Applicaction.View
{
    public class StudentView
    {


        private readonly StudentController _controller;
        private readonly DepartmentController departmentController;
        private readonly SectionController sectionController;
        string[] options = { "Add Student", "View Student Details", "FindById", "Update","Result","Delete", "Exit" };
        public StudentView(StudentController controller, DepartmentController departmentController, SectionController sectionController)
        {
            _controller = controller;
            this.departmentController = departmentController;
            this.sectionController = sectionController;
        }


        #region Main Function 
        public async Task Main() // Change void to Task
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                var option = Layout.DisplayMenu(Helper.StudentLayOut.MANAGEMNT_STUDENT_SYSTEM.ToString().Replace('_',' '), options);

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        await GetStudentInputForm();
                        break;
                    case "2":
                        Console.Clear();
                        DisplayStudentDetails(await _controller.GetStudent());
                        break;
                    case "3": //Find BY id
                        Console.Clear();
                        await  showstudentById();
                        break;
                    case "4":
                        Console.Clear();
                       await  UpdateStudent();
                        break;
                    case "5":
                        Console.Clear();
                        await GetDetails();
                        break;
                    case "6":
                        Console.Clear();
                        await DeleteStudent();
                        break;
                    case "7":
                        exit = true;
                        break;


                    default:
                        Layout.ShowMessage("Invalid Option", ConsoleColor.Red);
                        break;
                }
            }
        }

        #endregion

        #region FindById

        private async Task showstudentById()
        {
            List<Student> students = new List<Student>();
            var std = await FindStudentById();
            if (std!=null)
            {
                students.Add(std);
                DisplayStudentDetails(students);
            }
           
        }


        public async Task<Student>  FindStudentById()
        {
            bool exit = false;
            Student student = null;
            
            while (!exit)
            {
                Layout.DisplayTital(Helper.GenericLayOut.ENTER_ID_DETALIS.ToString().Replace('_', ' '));

                Layout.ShowMessageLine("Please Enter Student ID:", ConsoleColor.Cyan);
            
                //validation Id
                int id = Layout.PromptForIntInput(Console.ReadLine(), "ID");

                // Press Esc
                if (id == -1) {
                    break;
                }

                student = await _controller.GetStudnetByID(id);


                if (student == null)
                {
                    Layout.ShowMessageLine("Student ID not found.", ConsoleColor.Red);

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

            return student;
        }

        #endregion

        #region Details
        private async Task<List<CourseProfessorDto>> FindStudentByIdwithDetails()
        {
            bool exit = false;
            List<CourseProfessorDto> student = null;

            while (!exit)
            {
                Layout.DisplayTital(Helper.GenericLayOut.ENTER_ID_DETALIS.ToString().Replace('_', ' '));

                Layout.ShowMessageLine("Please Enter Student ID:", ConsoleColor.Cyan);

                //validation Id
                int id = Layout.PromptForIntInput(Console.ReadLine(), "ID");

                // Press Esc
                if (id == -1)
                {
                    break;
                }

                student = await _controller.GetStudnetProfessors(id);


                if (student == null)
                {
                    Layout.ShowMessageLine("Student ID not found.", ConsoleColor.Red);

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

            return student;
        }




        public async Task GetDetails()
        {
            var student = await FindStudentByIdwithDetails();
            if (student != null)
            {
                    DisplayStudentDetailsWithDetails(student);
                
            }
        }

        public void DisplayStudentDetailsWithDetails(List<CourseProfessorDto>  studentDetails)
        {
            Layout.DisplayTital(Helper.StudentLayOut.DATA_STUDENT_DETALIS.ToString().Replace('_', ' '));


            
            Layout.ShowMessage("Student: ", ConsoleColor.Cyan);
            Layout.ShowMessageLine($"[{studentDetails[0].student.FirstName} {studentDetails[0].student.LastName}]", ConsoleColor.Yellow);

            Layout.ShowMessage("Department: ", ConsoleColor.Cyan);
            Layout.ShowMessageLine($"[{studentDetails[0].student.Department.Name}]", ConsoleColor.Yellow);


            Layout.ShowMessage("Section: ", ConsoleColor.Cyan);
            Layout.ShowMessageLine($"[{studentDetails[0].student.section.Name}]", ConsoleColor.Yellow);
           

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{"Course",-30} {"Grade",-10} {"Professor",-30}");
            Console.WriteLine(new string('-', 70));
            foreach (var item in studentDetails)
            {
              


                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{item.CourseName,-30} ");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{item.Grade,-10} ");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"{item.ProfessorFirstName} {item.ProfessorLastName,-30}");
            }

           
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('=', 70));
            Console.WriteLine("Press any key to return to the menu...");
            Console.ResetColor();
            Console.ReadKey();
        }



        #endregion

        #region ShowStudent
        public void DisplayStudentDetails(List<Student> students)
        {
            Layout.DisplayTital(Helper.StudentLayOut.DATA_STUDENT_DETALIS.ToString().Replace('_', ' '));

            if (students.Count == 0)
            {
                Layout.ShowMessageLine("No students available.", ConsoleColor.Yellow);
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            // Display table header with corrected formatting
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═════╦════════════════════════╦════════════════════════════╦═════════════════╦═════════════════╦══════════╦═════╦══════╦═══════════════╦════════════╗");
            Console.WriteLine("║ ID  ║ Name                   ║ Email                      ║ Phone           ║ Department      ║ Section  ║ Age ║ GPA  ║ Level         ║ Birthday   ║");
            Console.WriteLine("╠═════╬════════════════════════╬════════════════════════════╬═════════════════╬═════════════════╬══════════╬═════╬══════╬═══════════════╬════════════╣");

            // Display each student's information
            foreach (var student in students)
            {
                DisplayStudentInfo(student);
            }

            // Display table footer
            Console.WriteLine("╚═════╩════════════════════════╩════════════════════════════╩═════════════════╩═════════════════╩══════════╩═════╩══════╩═══════════════╩════════════╝");

            // Prompt user to return to the menu
            Console.WriteLine();
            Layout.ShowMessageLine("Press any key to return to the menu...", ConsoleColor.Yellow);
            Console.ReadKey();
        }

        private void DisplayStudentInfo(Student student)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"║ {student.Id,-3} ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"║ {student.FirstName + " " + student.LastName,-22} ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"║ {student.Email,-26} ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"║ {student.Phone,-15} ");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"║ {student.Department.Name,-15} ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"║ {student.section.Name,-8} ");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"║ {student.Age,-3} ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"║ {student.Gpa,-3} ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"║ {student.Level.Name,-13} ");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"║ {student.Birthday.ToShortDateString(),-10} ║");

            Console.ResetColor();
        }

        #endregion

        #region GetInputStudent
        public async Task GetStudentInputForm()
        {
            // Create an array to hold the student data
            string[] studentData = new string[10];
            var newStudent = new Student();

            while (true)
            {
                DisplayStudentInputForm(studentData); // Show current input form

                var key = Console.ReadKey(true);
                if (key.Key >= ConsoleKey.NumPad0 && key.Key <= ConsoleKey.NumPad9)
                {
                    int index = key.Key - ConsoleKey.NumPad0; // Get the index of the input field to Zero Index
                    Console.Clear();
                    var fieldName = GetFieldName(index);

                       await  HandleField(newStudent, fieldName, index, studentData);

                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    // Create the student object with the filled data
                    var student = new Student
                    {
                        FirstName = studentData[0],
                        LastName = studentData[1],
                        Email = studentData[2],
                        Phone = studentData[3],
                        SectionId = newStudent.SectionId,
                        LevelId = newStudent.LevelId,
                        DepId = newStudent.DepId,
                        Age = newStudent.Age,
                        Gpa = newStudent.Gpa,
                        Birthday = newStudent.Birthday
                    };

                    var validationResults = ModelStat.IsValid(student);
                    if (validationResults.Count == 0)
                    {
                        // Call the controller method to add the student
                        if (await _controller.AddStudnetController(student))
                        {
                            Layout.ShowMessageLine("Student added successfully!", ConsoleColor.Green);
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

        #endregion

        #region ShowInput
        private void DisplayStudentInputForm(string[] studentData)
        {
            Layout.DisplayTital(Helper.GenericLayOut.ENTER_DATA_DETAILS.ToString().Replace('_', ' '));

            Console.WriteLine($"0. First Name: {Layout.FormatField(studentData[0])}");
            Console.WriteLine($"1. Last Name: {Layout.FormatField(studentData[1])}");
            Console.WriteLine($"2. Email: {Layout.FormatField(studentData[2])}");
            Console.WriteLine($"3. Phone: {Layout.FormatField(studentData[3])}");
            Console.WriteLine($"4. Level ID: {Layout.FormatField(studentData[4])}");
            Console.WriteLine($"5. Department ID: {Layout.FormatField(studentData[5])}");
            Console.WriteLine($"6. Age: {Layout.FormatField(studentData[6])}");
            Console.WriteLine($"7. GPA: {Layout.FormatField(studentData[7])}");
            Console.WriteLine($"8. Birthday (yyyy-mm-dd): {Layout.FormatField(studentData[8])}");
            Console.WriteLine($"9. Section Id: {Layout.FormatField(studentData[9])}");
            Console.WriteLine("=======================================");
            Console.WriteLine("Press the number of the field to edit it, or press Enter to save.");
        }
        #endregion

        #region Validation
     
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

        private static DateTime PromptForDateInput(string intput, string message)
        {
            Console.Write(message);
            DateTime result;
            while (!DateTime.TryParse(intput, out result))
            {
                Console.WriteLine("Invalid input. Please enter a valid date (yyyy-mm-dd).");
                Console.Write(message);
            }
            return result;
        }
        #endregion

        #region Methode
        


        private static string GetFieldName(int index)
        {
            return index switch
            {
                0 => "First Name",
                1 => "Last Name",
                2 => "Email",
                3 => "Phone",
                4 => "Level ID",
                5 => "Department ID",
                6 => "Age",
                7 => "GPA",
                8 => "Birthday",
                9 => "Section",
                _ => ""
            };
        }
        #endregion

        #region GetLevel
        public async Task<Student> SelectLevel(Student student)
        {
            // Retrieve the levels from the controller
            var levels = await _controller.GetLevels();

            if (levels == null || levels.Count == 0)
            {
                Layout.ShowMessageLine("No levels available.", ConsoleColor.Red);
                Console.ReadKey();
                return student;
                
            }

            //DisplayLevel
            Layout.Dispaly(levels,"LEVEL");

            //validiation Select level
            int selectedLevelIndex = Layout.ValidationSelectInput(levels,"LEVEL");

            //Select Esc
            if (selectedLevelIndex!=-2)
            {
                // Set the selected level with confirmation message in color
                var selectedLevel = levels[selectedLevelIndex];
                Layout.ShowMessageLine($"\nYou selected: {selectedLevel.Name}\n", ConsoleColor.Green);
                student.LevelId = selectedLevel.Id;
                student.Level = new Level { Id = selectedLevel.Id, Name = selectedLevel.Name };
            }

            return student;
        }
        #endregion


        #region GetSection
        public async Task<Student> SelectSection(Student student)
        {
            if (student.DepId > 0)
            {

                var section = await sectionController.GetSection(student.DepId, student.LevelId);

                if (section == null || section.Count == 0)
                {
                    Layout.ShowMessageLine("No section available.", ConsoleColor.Red);
                    return student;
                }

                // Display the levels with color
                Layout.Dispaly(section, "Section");

                // Get user input with enhanced instructions

                var selectedSectionlIndex = Layout.ValidationSelectInput(section, "Section");

                if (selectedSectionlIndex != -2)
                {
                    // Set the selected level with confirmation message in color
                    var selectedLevel = section[selectedSectionlIndex];
                    Layout.ShowMessageLine($"\nYou selected: {selectedLevel.Name}\n", ConsoleColor.Green);
                    student.SectionId = selectedLevel.Id;
                    student.section = new Section { Id = selectedLevel.Id, Name = selectedLevel.Name };
                }

            }
            else
            {
                Layout.ShowMessageLine("=======================", ConsoleColor.Gray);
                Layout.ShowMessageLine("Shoud By Chosse Department First", ConsoleColor.Red);
                Layout.ShowMessageLine("=======================", ConsoleColor.Gray);
                Console.ReadKey();
            }
            return student;

        }
        #endregion


        #region GetDep
        public async Task<Student> SelectDep(Student student)
        {
            if (student.LevelId > 0)
            {

                var dep = await departmentController.GetDep(student.LevelId);

                if (dep == null || dep.Count == 0)
                {
                    Layout.ShowMessageLine("No Department available.", ConsoleColor.Red);
                    return student;
                }

                // Display the levels with color
                Layout.Dispaly(dep, "Department");

                // Get user input with enhanced instructions

                var selectedSectionlIndex = Layout.ValidationSelectInput(dep, "Department");

                if (selectedSectionlIndex != -2)
                {
                    // Set the selected level with confirmation message in color
                    var selectedDep = dep[selectedSectionlIndex];
                    Layout.ShowMessageLine($"\nYou selected: {selectedDep.Name}\n", ConsoleColor.Green);
                    student.DepId = selectedDep.Id;
                    student.Department = new Department { Id = selectedDep.Id, Name = selectedDep.Name };
                }

            }
            else
            {
                Layout.ShowMessageLine("=======================", ConsoleColor.Gray);
                Layout.ShowMessageLine("Shoud By Chosse Level First", ConsoleColor.Red);
                Layout.ShowMessageLine("=======================", ConsoleColor.Gray);
                Console.ReadKey();
            }
            return student;

        }
        #endregion

        #region UpdateStudent
        public async Task UpdateStudent()
        {
            var student = await FindStudentById();

            if (student!=null)
            {
                string[] studentData = InitializeStudentData(student);

                while (true)
                {
                    DisplayStudentInputForm(studentData);

                    var key = Console.ReadKey(true);
                    if (key.Key >= ConsoleKey.NumPad1 && key.Key <= ConsoleKey.NumPad9)
                    {
                        int index = key.Key - ConsoleKey.NumPad1;
                        Console.Clear();
                        var fieldName = GetFieldName(index);

                       await HandleField(student, fieldName, index, studentData);
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        if (await ValidateAndSaveStudent(student, studentData))
                        {
                            break;
                        }
                    }
                }
            }
        }

        private string[] InitializeStudentData(Student student)
        {
            return new string[]
            {
                 student.FirstName,
                 student.LastName,
                 student.Email,
                 student.Phone,
                 student.Level.Name,
                 student.Department.Name,
               
                 student.Age.ToString(),
                 student.Gpa.ToString(),
                 student.Birthday.ToString("yyyy-MM-dd"),
                 student.SectionId.ToString(),
            };
        }

        private async Task HandleField(Student student, string fieldName, int index, string[] studentData)
        {
            Layout.ShowMessageLine($"Current value for {fieldName}: {Layout.FormatField(studentData[index])}", ConsoleColor.Cyan);
            Layout.ShowMessageLine($"Enter new value for {fieldName}: ", ConsoleColor.Cyan);
            Console.WriteLine();

            if (fieldName == "Level ID")
            {
                student = await SelectLevel(student);
                studentData[index] = Layout.FormatField(student?.Level?.Name);
            }
            else if (fieldName == "Department ID")
            {
                student =  await SelectDep(student);
                studentData[index] = Layout.FormatField(student?.Department?.Name);
            }
            else if(fieldName == "Section")
            {
                student = await SelectSection(student);
                studentData[index] = Layout.FormatField(student?.section?.Name);
            }
            else
            {
                var input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    UpdateStudentField(student, fieldName, index, input, studentData);
                }
            }
        }

        private void UpdateStudentField(Student student, string fieldName, int index, string input, string[] studentData)
        {
            switch (fieldName)
            {
                case "Age":
                    student.Age = Layout.PromptForIntInput(input, fieldName);
                    studentData[index] = student.Age.ToString();
                    break;
                case "GPA":
                    student.Gpa = PromptForDecimalInput(input, fieldName);
                    studentData[index] = student.Gpa.ToString();
                    break;
                case "Birthday":
                    student.Birthday = PromptForDateInput(input, fieldName);
                    studentData[index] = student.Birthday.ToString("yyyy-MM-dd");
                    break;
                default:
                    studentData[index] = input;
                    break;
            }
        }

        private async Task<bool>  ValidateAndSaveStudent(Student student, string[] studentData)
        {
            var validationResults = ModelStat.IsValid(student);
            if (validationResults.Count == 0)
            {
                if (await _controller.UpdateStudent(student))
                {
                    Layout.ShowMessageLine("Student updated successfully!", ConsoleColor.Green);
                    Console.ReadKey();
                    return true;
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
            return false;
        }
        #endregion

        #region DeleteStudent
         public async Task DeleteStudent()
         {

          
           
                var student = await FindStudentById();
                if (student != null)
                {
                    Layout.ShowMessageLine($"Are Your Sure To Delete Student With Name [{student.FirstName} {student.LastName}] \n" +
                                       $"Ok Press [Enter] Cancel Press [Esc]", ConsoleColor.Yellow);
                    while (true)
                    {
                        var key = Console.ReadKey();
                        if (key.Key == ConsoleKey.Enter)
                        {
                            if (await _controller.DeleteStudent(student))
                            {
                                Layout.ShowMessageLine($"Succesed Delete Student With Name [{student.FirstName}]", ConsoleColor.Green);
                                Console.ReadKey();
                              
                                break;
                            }
                            else
                            {
                                Layout.ShowMessageLine($"Error Delete Student With Name [{student.FirstName}]", ConsoleColor.Red);
                                Console.ReadKey();
                               
                                break;
                            }
                        }
                        else if (key.Key == ConsoleKey.Escape)
                        {
                           
                            break;
                        }

                    }
                }
               
            
               

              
                
            }
            
        #endregion


    }
}

