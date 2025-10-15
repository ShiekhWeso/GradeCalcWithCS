using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeCalcWithCS
{
    public class Subject
    {
        public string Name { get; set; } = string.Empty;
        public double Mark { get; set; }
        public double CreditHours { get; set; }

        public string GetLetterGrade()
        {
            double percentage = GetPercentage();

            if (percentage >= 90) return "A+";
            else if (percentage >= 85) return "A";
            else if (percentage >= 80) return "B+";
            else if (percentage >= 75) return "B";
            else if (percentage >= 70) return "C+";
            else if (percentage >= 65) return "C";
            else if (percentage >= 60) return "D";
            else return "F";

        }
        public double GetGPAvalue()
        {
            double percentage = GetPercentage();

            if (percentage >= 90) return 4.0;
            else if (percentage >= 85) return 3.7;
            else if (percentage >= 80) return 3.3;
            else if (percentage >= 75) return 3.0;
            else if (percentage >= 70) return 2.7;
            else if (percentage >= 65) return 2.3;
            else if (percentage >= 60) return 2.0;
            else return 0.0;
        }
        public double GetPercentage()
        {
            return (Mark / (CreditHours * 100)) * 100;
        }
    }
}
