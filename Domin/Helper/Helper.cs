using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Helper
{
    public static class Helper
    {
        public enum GenericLayOut
        {
            ENTER_DATA_DETAILS,
            ENTER_ID_DETALIS
        };

        public enum StudentLayOut {

            MANAGEMNT_STUDENT_SYSTEM,

            DATA_STUDENT_DETALIS,
            
        };
        public enum DepartmentLayOut
        {

            MANAGEMNT_DEPARTMENT_SYSTEM,

            DATA_DEPARTMENT_DETALIS,

        };
        public enum ProfessoresLayOut
        {

            MANAGEMNT_PROFESSORS_SYSTEM,

            DATA_PROFESSORS_DETALIS,

        };
        public enum EnrrolemntLayOut
        {

            MANAGEMNT_ENRROLEMNT_SYSTEM,
            DATA_ENRROLEMNT_DETALIS,

        };
        public enum SectionLayOut
        {

            MANAGEMNT_SECTION_SYSTEM,
            DATA_SECTION_DETALIS,

        };
        public enum LevelLayOut
        {
            
            MANAGEMNT_LEVEL_SYSTEM,
            DATA_LEVEL_DETALIS,
            
        };
        public enum CouresLayOut
        {

            MANAGEMNT_COURES_SYSTEM,
            DATA_COURES_DETALIS,

        };

    }
}
