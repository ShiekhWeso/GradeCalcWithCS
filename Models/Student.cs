using System.Collections.Generic;

namespace GradeCalcWithCS.Models
{
    public class Student
    {
        public required string Name { get; set; }
        public List<Subject> Subjects { get; set; } = new List<Subject>();
        public double GPA => GetGPA();
        public double TotalPercentage => GetTotalPercentage();

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