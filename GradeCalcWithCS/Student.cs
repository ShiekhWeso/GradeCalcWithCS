using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeCalcWithCS
{
    public class Student
    {
        public  string Name { get; set; } = string.Empty;
        public double GPA { get; set; } = 0.00;
        public string Percentage { get; set; } = "0.00%";
        public List<Subject> Subjects { get; set; } = new List<Subject>();

        public double GetTotalPercentage()
        {
            double totalMarks = 0;
            double totalCredits = 0;

            foreach (var subject in Subjects)
            {
                totalMarks += subject.Mark;
                totalCredits += subject.CreditHours;
            }

            return totalCredits > 0 ? totalMarks / totalCredits : 0;
        }
        public double GetGPA()
        {
            double totalPoints = 0;
            double totalCredits = 0;

            foreach (var subject in Subjects)
            {
                double gpa = subject.GetGPAvalue();
                totalPoints += gpa * subject.CreditHours;
                totalCredits += subject.CreditHours;
            }

            return totalCredits > 0 ? totalPoints / totalCredits : 0;

        }
    }
}
