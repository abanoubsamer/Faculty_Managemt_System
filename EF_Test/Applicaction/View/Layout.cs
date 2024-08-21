using Domin.Models;
using Domin.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Program.Applicaction.View
{
    public static class Layout
    {


        #region Layout
        ///////////////////  LayOut Section  \\\\\\\\\\\\\\\\\\\\\ 
        public static int DispalyStartApp()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("\t\t\t\t===========================================");
            Console.WriteLine("\t\t\t      ///// WELCOME TO FACULTY MANAGEMENT SYSTEM \\\\\\\\\\");
            Console.WriteLine("\t\t\t\t===========================================");

            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\tThis application allows you to manage student, course, and professor information efficiently.");
            Console.WriteLine("\tYou can:\n");

            // Student Management System
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t\t1. STUDENT MANAGEMENT SYSTEM\t\t");

            // Level Management System
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t2. LEVEL MANAGEMENT SYSTEM\n");

            // Enrollment Management System
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t\t3. ENROLLMENT MANAGEMENT SYSTEM\t\t");

            // Section Management System
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t4. SECTION MANAGEMENT SYSTEM\n");

            // Professor Management System
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\t\t5. PROFESSOR MANAGEMENT SYSTEM\t\t");

            // Course Management System
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t6. COURSE MANAGEMENT SYSTEM\n");

            // Department Management System
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\t\t7. DEPARTMENT MANAGEMENT SYSTEM\n");

            Console.ResetColor();
            Console.WriteLine("\t\t...and much more.");
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("\t\t\t\tPlease select an option from the main menu.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\t\t\tPress any key to continue...");
            Console.ResetColor();
            var keyInfo = Console.ReadKey(true); // Read the key input without displaying it on the console
            return (int)keyInfo.Key; // Return the key as an integer
        }
        public static string DisplayMenu(string layout, string[] options)
        {
            Console.Clear();
            DisplayTital(layout);


            int selectedIndex = 0;

            while (true)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($" {i + 1}. {options[i]}");
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true);
                Console.Clear();
                DisplayTital(layout);

                if (key.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : options.Length - 1;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    selectedIndex = (selectedIndex < options.Length - 1) ? selectedIndex + 1 : 0;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    return (selectedIndex + 1).ToString();
                }
            }
        }
        public static string FormatField(string fieldValue)
        {
            return string.IsNullOrEmpty(fieldValue) ? "____________________" : fieldValue;
        }
        public static void ShowMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }
        public static void ShowMessageLine(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void DisplayTital(string layout)
        {
            Console.Clear();
            ShowMessageLine("\t\t\t\t\t=======================================", ConsoleColor.Cyan);
            ShowMessageLine($"\t\t\t\t\t          {layout}         ", ConsoleColor.Cyan);
            ShowMessageLine("\t\t\t\t\t=======================================", ConsoleColor.Cyan);
        }
        #endregion


        #region DispalyStudent
        public static void DispalyStudent<T>(List<T> Entity, string msg) where T : class
        {
            var FirstNameProperty = typeof(T).GetProperty("FirstName");
            var LastNameProperty = typeof(T).GetProperty("LastName");
            var idProperty = typeof(T).GetProperty("Id");

            if (FirstNameProperty == null || LastNameProperty == null || idProperty == null)
            {
                throw new InvalidOperationException("T must have 'LastNameProperty' and 'FirstNameProperty' and 'Id' properties.");
            }

          
            // Display header with message
            ShowMessageLine($"\nAvailable {msg}:", ConsoleColor.Cyan);



            // Display table header
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"╔═════╦═══════════════════════╗");
            Console.WriteLine($"║ ID  ║ Name                  ║");
            Console.WriteLine($"╠═════╬═══════════════════════╣");

            // Display each entity's information
            if (Entity.Count == 0)
            {
                ShowMessageLine($"No {msg} available.", ConsoleColor.Red);
                Console.WriteLine($"╚═════╩═══════════════════════╝");
            }
            else
            {
             
                foreach (var entity in Entity)
                {
                    DisplayEntityInfo(entity, FirstNameProperty, LastNameProperty, idProperty);
                }

                // Display table footer
                Console.WriteLine($"╚═════╩═══════════════════════╝");
            }

            Console.ResetColor(); // Reset the console color to default
        }

        private static void DisplayEntityInfo<T>(T entity, PropertyInfo firstNameProperty, PropertyInfo lastNameProperty, PropertyInfo idProperty) where T : class
        {
            string firstName = firstNameProperty.GetValue(entity)?.ToString() ?? "";
            string lastName = lastNameProperty.GetValue(entity)?.ToString() ?? "";

            // Calculate total width for formatting
           
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"║ {idProperty.GetValue(entity),-3} ");

            Console.ForegroundColor = ConsoleColor.Green;
           
            Console.Write($"║ {firstName} {lastName}");
           

            Console.ResetColor();
            Console.WriteLine();
        }

        #endregion

        #region DispalyProfessor
        public static void DispalyProfessor(List<ProfessorShowViewModel> Entity, string msg)
        {
          
            // Display header with message
            ShowMessageLine($"\nAvailable {msg}:", ConsoleColor.Cyan);



            // Display table header
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"╔═════╦════════════════════╦═══════════╗");
            Console.WriteLine($"║ ID  ║ Name               ║  Coures   ║");
            Console.WriteLine($"╠═════╬════════════════════╬═══════════╣");

            // Display each entity's information
            if (Entity.Count == 0)
            {
                ShowMessageLine($"No {msg} available.", ConsoleColor.Red);
               
            }
            else
            {
                foreach (var entity in Entity)
                {
                    DisplayEntityInfo(entity.FirstName, entity.LastName, entity.CourseName,entity.Id);
                }

                // Display table footer
                Console.WriteLine($"╚═════╩════════════════════╩═══════════╝");
            }

            Console.ResetColor(); // Reset the console color to default
        }

        private static void DisplayEntityInfo( string FirstNameProperty, string LastNameProperty, string CoueseNameProperty, int idProperty) 
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"║ {idProperty,-3} ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"║ {FirstNameProperty} {LastNameProperty,-11}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"║ {CoueseNameProperty,-10}║ ");



            Console.ResetColor(); // Reset color after each row
            Console.WriteLine();
        }
        #endregion



        public static int SelectItem<T>(List<T> items, string itemName) where T : class
        {
            if (items != null && items.Any())
            {
                Layout.Dispaly(items, itemName);

                int selectedIndex = Layout.ValidationSelectInput(items, itemName);

                if (selectedIndex != -2)
                {
                    var selectedItem = items[selectedIndex];

                    Console.Clear();
                    Layout.DisplayTital("ENTER ID DETAILS");
                    Layout.ShowMessageLine("================================", ConsoleColor.Magenta);
                    Layout.ShowMessage($"{itemName}", ConsoleColor.Cyan);
                    Layout.ShowMessage($"[", ConsoleColor.Green);

                    // Use reflection to get the Name property
                    var nameProperty = typeof(T).GetProperty("Name");
                    if (nameProperty != null)
                    {
                        var name = nameProperty.GetValue(selectedItem)?.ToString();
                        Layout.ShowMessage($"{name}", ConsoleColor.Yellow);
                    }

                    Layout.ShowMessageLine($"]", ConsoleColor.Green);
                    Layout.ShowMessageLine("================================", ConsoleColor.Magenta);

                    // Use reflection to get the Id property
                    var idProperty = typeof(T).GetProperty("Id");
                    if (idProperty != null)
                    {
                        return (int)idProperty.GetValue(selectedItem);
                    }
                }
            }
            return -1;
        }

        #region Dep
        //////////////////////  Level Section  \\\\\\\\\\\\\\\\\\\\\\\\\ 
        public static void Dispaly<T>(List<T> Entity , string msg) where T:class
        {
            var nameProperty = typeof(T).GetProperty("Name");
            var idProperty = typeof(T).GetProperty("Id");

            if (nameProperty == null || idProperty == null)
            {
                throw new InvalidOperationException("T must have 'Name' and 'Id' properties.");
            }

            // Display header with message
            ShowMessageLine($"\nAvailable {msg}:", ConsoleColor.Cyan);

     

            // Display table header
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"╔═════╦═════════════════╗");
            Console.WriteLine($"║ ID  ║ Name            ║");
            Console.WriteLine($"╠═════╬═════════════════╣");

            // Display each entity's information
            if (Entity.Count == 0)
            {
                ShowMessageLine($"No {msg} available.", ConsoleColor.Red);
                Console.WriteLine($"╚═════╩═════════════════╝");
            }
            else
            {
                foreach (var entity in Entity)
                {
                    DisplayEntityInfo(entity, nameProperty, idProperty);
                }

                // Display table footer
                Console.WriteLine($"╚═════╩═════════════════╝");
            }

            Console.ResetColor(); // Reset the console color to default
        }

        private static void DisplayEntityInfo<T>(T entity, PropertyInfo nameProperty, PropertyInfo idProperty) where T : class
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"║ {idProperty.GetValue(entity),-3} ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"║ {nameProperty.GetValue(entity),-16}║");

            Console.ResetColor(); // Reset color after each row
            Console.WriteLine();
        }


        #endregion


        #region Validation
        //////////////////// Validation Input \\\\\\\\\\\\\\\\\\\\

        public static int ValidationSelectInput<T>(List<T> model, string msg) where T : class
        {
            // Get user input with enhanced instructions
            int selectedLevelIndex = -1;
            bool isValidInput = false;
            while (!isValidInput)
            {

                ShowMessageLine($"\nEnter the number corresponding to the {msg}: ", ConsoleColor.Green);

                selectedLevelIndex = PromptForIntInput(Console.ReadLine(), msg) - 1;

                if (selectedLevelIndex >= 0 && selectedLevelIndex < model.Count)
                {
                    isValidInput = true;
                }
                // Option Esc
                else if (selectedLevelIndex == -2)
                {
                    return selectedLevelIndex;

                }
                else
                {
                    ShowMessageLine("Invalid selection. Please try again.", ConsoleColor.Red);
                }
            }

            return selectedLevelIndex;
        }
        public static int PromptForIntInput(string input, string message)
        {
            int result;

            while (true)
            {
                Console.WriteLine($"Are you done with this {message} : {input}? If not done, enter [X] to try again or press [Enter] to confirm , To Exit Press [Esc].");
                var keyInput = Console.ReadKey(intercept: true);

                if (keyInput.Key == ConsoleKey.Escape)
                {
                    ShowMessageLine("Escape key detected. Exiting input.", ConsoleColor.Yellow);
                    return -1; // Return -1 or another value to indicate the escape action
                }

                if (keyInput.Key == ConsoleKey.Enter)
                {
                    // Validate if the input is an integer
                    if (int.TryParse(input, out result))
                    {
                        return result;
                    }
                    ShowMessageLine("Invalid input. Please enter a valid integer.", ConsoleColor.Red);
                    ShowMessageLine("Enter again: ", ConsoleColor.Green);
                }

                if (keyInput.Key == ConsoleKey.X)
                {
                    ShowMessageLine($"Please enter the {message} again.", ConsoleColor.Yellow);
                    input = Console.ReadLine(); // Get new input from the user
                }
            }
        }

        public static string PromptFoStringInput(string input, string message)
        {
            string result;

            while (true)
            {
                Console.WriteLine($"Are you done with this ID {input}? If not done, enter [X] to try again or press [Enter] to confirm , To Exit Press [Esc].");
                var keyInput = Console.ReadKey(intercept: true);

                if (keyInput.Key == ConsoleKey.Escape)
                {
                    ShowMessageLine("Escape key detected. Exiting input.", ConsoleColor.Yellow);
                    return "EXITE"; // Return -1 or another value to indicate the escape action
                }

                if (keyInput.Key == ConsoleKey.Enter)
                {
                    if (input.Length >=1 )
                    {
                        return input;
                    }

                    ShowMessageLine("Invalid input. Please enter a valid integer.", ConsoleColor.Red);
                    ShowMessageLine("Enter again: ", ConsoleColor.Green);
                }

                if (keyInput.Key == ConsoleKey.X)
                {
                    ShowMessageLine("Please enter the Name again.", ConsoleColor.Yellow);
                    input = Console.ReadLine(); // Get new input from the user
                }
            }
        }

        #endregion







    }
}
