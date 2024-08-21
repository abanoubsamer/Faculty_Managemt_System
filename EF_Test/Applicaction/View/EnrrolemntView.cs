using Domin.Helper;
using Domin.Models;
using Program.Applicaction.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Applicaction.View
{
    public class EnrrolemntView
    {
        private readonly EnrrolemntController _controller;
        private readonly StudentView _student;
        string[] options = { "Add Enrollemnt", "View Enrollemnt Details", "FindById","EnrollemntWithLevel", "EnrollemntWithCourse", "EnrollemntWithStudent", "Update", "Exit" };
        public EnrrolemntView(EnrrolemntController controller, StudentView student)
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
                var option = Layout.DisplayMenu(Helper.EnrrolemntLayOut.MANAGEMNT_ENRROLEMNT_SYSTEM.ToString().Replace('_', ' '), options);

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        await AddEnrollmentView();
                        break;
                    case "2":
                        Console.Clear();
                        await GetEnrollmentsView();
                        break;
                    case "3": //Find BY id
                        Console.Clear();
                        await FindEnrollmentView();
                        break;
                    case "4": //EnrollmentsWithLevel
                        Console.Clear();
                        await GetEnrollmentsWithLevelView();
                      
                        break;
                    case "5": //EnrollmentsWithCourses
                        Console.Clear();
                        await GetEnrollmentsWithCoursesView();
                        break;
                    case "6": //EnrollmentsWithStudent
                        Console.Clear();
                        await GetEnrollmentsWithStudentView();
                        break;
                    case "7": //Find BY id
                        await UpdateEnrollmentView();
                        break;
                    case "8":
                        exit = true;
                        break;



                }
            }
        }
        #endregion


        public async Task<int> GetCoursWithDepAndLevel(int stdid,int Lid,int Did)
        {
            var Courses = await _controller.GetCourseLeveAndDep(stdid, Lid,Did);

            Layout.Dispaly(Courses, "Courses");

            Console.Write("Enter Course ID: ");
            int selectedLevelIndex=Layout.ValidationSelectInput(Courses, "Courses");

            //Select Esc
            if (selectedLevelIndex != -2)
            {
                return Courses[selectedLevelIndex].Id;

            }

            return -1;
        }

        public async Task AddEnrollmentView()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Add New Enrollment");
            Console.ResetColor();

            // Collect enrollment details from the user
            try
            {
           
               var student= await _student.FindStudentById();
                if (student != null)
                {
                    int courseId = await GetCoursWithDepAndLevel(student.Id, student.LevelId,student.DepId);
                    if (courseId != -1)
                    {
                        Console.Write("Enter Grade: ");
                        decimal grade = decimal.Parse(Console.ReadLine());

                        // Create an Enrollment model
                        Enrollment newEnrollment = new Enrollment
                        {
                            StudentsId = student.Id,
                            CoursesId = courseId,
                            Grade = grade
                        };

                        // Add the enrollment using the controller
                        bool success = await _controller.AddEnrollments(newEnrollment);

                        // Display result
                        if (success)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Enrollment added successfully!");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Failed to add enrollment. Please check the details and try again.");
                        }
                    }

                   
                }

              
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input format. Please enter valid data.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                Console.ResetColor();
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();
            }
        }

        public async Task FindEnrollmentView()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Find Enrollment by ID");
            Console.ResetColor();

            Console.Write("Enter Enrollment ID: ");
            int id = int.Parse(Console.ReadLine());

            var enrollment = await _controller.FindById(id);
            if (enrollment!=null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Student: {enrollment.Student.FirstName}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Course: {enrollment.Course.Name}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Grade: {enrollment.Grade}");
                Console.ResetColor();
                Console.ReadLine();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enrollment not found.");
                Console.ResetColor();
                Console.ReadLine();
            }
        }
        public async Task GetEnrollmentsView()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("List of All Enrollments");
            Console.ResetColor();

            var enrollments = await _controller.GetEnrollments();

            if (enrollments.Count == 0)
            {
                Layout.ShowMessage("No Enrollments Available", ConsoleColor.Red);
                Console.ReadKey();
                return;
            }

            // Define column widths
            int idWidth = 5;
            int studentWidth = 20;
            int courseWidth = 20;
            int gradeWidth = 10;

            // Print table header
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{"ID".PadRight(idWidth)}{"Student".PadRight(studentWidth)}{"Course".PadRight(courseWidth)}{"Grade".PadRight(gradeWidth)}");
            Console.WriteLine(new string('-', idWidth + studentWidth + courseWidth + gradeWidth));

            // Print table rows
            foreach (var enrollment in enrollments)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{enrollment.Id.ToString().PadRight(idWidth)}"); // Convert Id to string
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{enrollment.Student.FirstName.PadRight(studentWidth)}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{enrollment.Course.Name.PadRight(courseWidth)}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"{enrollment.Grade.ToString("F2").PadRight(gradeWidth)}"); // Convert Grade to string and format
            }

            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }


        public async Task GetEnrollmentsWithLevelView()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("List of Enrollments by Level");
            Console.ResetColor();

            Console.Write("Enter Level ID: ");
            int levelId;
            if (!int.TryParse(Console.ReadLine(), out levelId))
            {
                Layout.ShowMessage("Invalid Level ID", ConsoleColor.Red);
                return;
            }

            var enrollments = await _controller.GetEnrollmentswithLevel(levelId);
            if (enrollments.Count == 0)
            {
                Layout.ShowMessage("No Enrollments Available for the Provided Level", ConsoleColor.Red);
                Console.ReadKey();
                return;
            }

            // Define column widths
            int idWidth = 5;
            int studentWidth = 20;
            int courseWidth = 20;
            int gradeWidth = 10;

            // Print table header
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{"ID".PadRight(idWidth)}{"Student".PadRight(studentWidth)}{"Course".PadRight(courseWidth)}{"Grade".PadRight(gradeWidth)}");
            Console.WriteLine(new string('-', idWidth + studentWidth + courseWidth + gradeWidth));

            // Print table rows
            foreach (var enrollment in enrollments)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{enrollment.Id.ToString().PadRight(idWidth)}"); // Convert Id to string
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{enrollment.Student.FirstName.PadRight(studentWidth)}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{enrollment.Course.Name.PadRight(courseWidth)}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"{enrollment.Grade.ToString("F2").PadRight(gradeWidth)}"); // Convert Grade to string and format
            }

            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        public void ShowEnrollment(List<Enrollment> enrollments)
        {
            if (enrollments.Count == 0)
            {
                Layout.ShowMessage("No Enrollments Available for the Provided Course", ConsoleColor.Red);
                Console.ReadKey();
                return ;
            }

            // Define column widths
            int idWidth = 5;
            int studentWidth = 20;
            int gradeWidth = 10;

            // Print table header
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{"ID".PadRight(idWidth)}{"Student".PadRight(studentWidth)}{"Grade".PadRight(gradeWidth)}");
            Console.WriteLine(new string('-', idWidth + studentWidth + gradeWidth));

            // Print table rows
            foreach (var enrollment in enrollments)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{enrollment.Id.ToString().PadRight(idWidth)}"); // Convert Id to string
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{enrollment.Student.FirstName.PadRight(studentWidth)}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{enrollment.Grade.ToString("F2").PadRight(gradeWidth)}"); // Convert Grade to string and format
            }

        }

        public async Task GetEnrollmentsWithCoursesView()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("List of Enrollments by Course");
            Console.ResetColor();

            Console.Write("Enter Course ID: ");
            int courseId;
            if (!int.TryParse(Console.ReadLine(), out courseId))
            {
                Layout.ShowMessage("Invalid Course ID", ConsoleColor.Red);
                return;
            }

            var enrollments = await _controller.GetEnrollmentswithCourses(courseId);

            ShowEnrollment(enrollments);


            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }
 
        public async Task GetEnrollmentsWithStudentView()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("List of Enrollments by Student");
            Console.ResetColor();

            Console.Write("Enter Student ID: ");
            int studentId;
            if (!int.TryParse(Console.ReadLine(), out studentId))
            {
                Layout.ShowMessage("Invalid Student ID", ConsoleColor.Red);
                return;
            }

            var enrollments = await _controller.GetEnrollmentswithStudent(studentId);
            if (enrollments.Count == 0)
            {
                Layout.ShowMessage("No Enrollments Available for the Provided Student", ConsoleColor.Red);
                Console.ReadKey();
                return;
            }

            // Define column widths
            int idWidth = 5;
            int courseWidth = 20;
            int gradeWidth = 10;

            // Print table header
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{ "ID".PadRight(idWidth) }{ "Course".PadRight(courseWidth) }{ "Grade".PadRight(gradeWidth) }");
            Console.WriteLine(new string('-', idWidth + courseWidth + gradeWidth));

            // Print table rows
            foreach (var enrollment in enrollments)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{ enrollment.Id.ToString().PadRight(idWidth) }"); // Convert Id to string
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{ enrollment.Course.Name.PadRight(courseWidth) }");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{ enrollment.Grade.ToString("F2").PadRight(gradeWidth) }"); // Convert Grade to string and format
            }

            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        public async Task UpdateEnrollmentView()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Updata Enrollment");
            Console.ResetColor();

            // Collect enrollment details from the user
            try
            {

                var student = await _student.FindStudentById();
                if (student != null)
                {
                    var enrollments = await _controller.GetEnrollmentswithStudent(student.Id);

                    ShowEnrollment(enrollments);
                    int selectedLevelIndex = Layout.ValidationSelectInput(enrollments, "Enrollemnt");

                    if (selectedLevelIndex != -2)
                    {
                        Layout.ShowMessage("Pleas Enter Grade: ", ConsoleColor.Yellow);
                        enrollments[selectedLevelIndex].Grade = Layout.PromptForIntInput(Console.ReadLine(), "Grade");
                        await _controller.UpdateEnrollments(enrollments[selectedLevelIndex]);
                        Layout.ShowMessageLine($"Succed Updata Enrollemnt Is {enrollments[selectedLevelIndex].Course.Name}", ConsoleColor.Green);
                    }

                }


            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input format. Please enter valid data.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                Console.ResetColor();
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();
            }
        }

    }
}
