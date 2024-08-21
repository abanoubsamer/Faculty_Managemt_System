using Program.Applicaction.Controller;
using Program.Applicaction.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Applicaction
{
       public class App
        {

        private readonly StudentView _studentView;
        private readonly LevelView _levelView;
        private readonly EnrrolemntView _entrrolemntView;
        private readonly SectionView _SectionView;
        private readonly DepartmentView _DepartmentView;
        private readonly ProfessorView _ProfessorView;
        private readonly CouresView _CouresView;

        public App(StudentView studentView,
            LevelView levelView ,
            EnrrolemntView entrrolemntView ,
            SectionView SectionView,
            DepartmentView departmentView,
            ProfessorView professorView,
            CouresView CouresView)
        {
            _studentView = studentView;
            _levelView = levelView;
            _entrrolemntView = entrrolemntView;
            _SectionView = SectionView;
            _DepartmentView = departmentView;
            _ProfessorView = professorView;
            _CouresView = CouresView;
        }

        public async Task Run()
        {
           
            while (true)
            {
                int selectedOption = Layout.DispalyStartApp();
                switch (selectedOption)
                {
                    case (int)ConsoleKey.NumPad1:
                       await _studentView.Main();
                        break;
                    case (int)ConsoleKey.NumPad2:
                      await  _levelView.Main();
                        break;
                    case (int)ConsoleKey.NumPad3:
                        await _entrrolemntView.Main();
                        break;
                    case (int)ConsoleKey.NumPad4://Section
                        await _SectionView.Main();
                        break;
                    case (int)ConsoleKey.NumPad5://professor
                        await _ProfessorView.Main();
                        break;
                    case (int)ConsoleKey.NumPad6://coures
                        await _CouresView.Main();
                        break;
                    case (int)ConsoleKey.NumPad7://dep
                        await _DepartmentView.Main();
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            } 

        }
    }

}
