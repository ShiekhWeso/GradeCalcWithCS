using System;
using System.Collections.Generic;
namespace GradeCalcWithCS
{
    class Program
    {
        static void Main()
        {
            List<Student> students = new List<Student>
            {
                new Student { Name = "Alice", Grades = new double[] { 99, 80.9, 90}},
                new Student { Name = "Bob", Grades = new double[] { 70, 60.5, 75}},
                new Student { Name = "Weso", Grades = new double[] { 85, 90.9, 88}}
            };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("GPA Calculator Menu:");
                Console.WriteLine("1. View all students:");
                Console.WriteLine("2. View your details:");
                Console.WriteLine("3. Add a new student");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        // show all students
                        // will be shown with gpa
                        break;
                    case "2":
                        // view your grades
                        // subject name and each grade with percentage and letter grade and the credit hours
                        break;
                    case "3":
                        // add new student 
                        // with each subject and each grade and credit hours
                        break;
                    case "4":
                        Console.WriteLine("Thank you for using the GPA Calculator. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }

            }
        }
    }

    class Student
    {
        public required string Name { get; set; }
        public List<Subject> Subjects { get; set; } = new List<Subject>();

        public double GetTotalPercentage()
        {
            return Subjects.Count > 0 ? Subjects.Average(s => s.Grade) : 0;
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

    class Subject
    {
        public string Name { get; set; } = string.Empty;
        public double Grade { get; set; }
        public int CreditHours { get; set; }

        public string GetLetterGrade()
        {
            if (Grade >= 90) return "A+";
            else if (Grade >= 85) return "A";
            else if (Grade >= 80) return "B+";
            else if (Grade >= 70) return "B";
            else if (Grade >= 65) return "C+";
            else if (Grade >= 60) return "C";
            else if (Grade >= 55) return "D+";
            else if (Grade >= 50) return "D";
            else return "F";

        }
        public double GetGPAvalue()
        {
            if (Grade >= 90) return 4.0;
            else if (Grade >= 85) return 3.7;
            else if (Grade >= 80) return 3.3;
            else if (Grade >= 70) return 3.0;
            else if (Grade >= 65) return 2.7;
            else if (Grade >= 60) return 2.0;
            else if (Grade >= 55) return 1.3;
            else if (Grade >= 50) return 1.0;
            else return 0.0;
        }
    }
}    
// user input
// file handling
// object-oriented approach