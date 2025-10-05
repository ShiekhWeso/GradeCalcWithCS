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
                new Student { Name = "Nigga", Grades = new double[] { 85, 90.9, 88}}
            };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("GPA Calculator Menu:");
                Console.WriteLine("1. View all students:");
                Console.WriteLine("2. View your grades");
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
            Console.WriteLine("Student        Grade\n");

            foreach (Student s in students)
            {
                try
                {
                    double average = s.GetAverage();
                    string letterGrade = s.GetLetterGrade();
                    Console.WriteLine($"{s.Name,-15}{average,5:F2}     {letterGrade}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{s.Name,-15} Error: {ex.Message}");
                }
            }
            Console.ReadKey();
        }
    }

    class Student
    {
        public required string Name { get; set; }
        public double[] Grades { get; set; } = Array.Empty<double>();
        public double GetAverage()
        {
            if (Grades == null || Grades.Length == 0)
            {
                throw new InvalidOperationException($"Grades are missing for {Name}.");
            }
            double total = 0;
            foreach (double grade in Grades)
            {
                if (grade < 0 || grade > 100)
                {
                    throw new ArgumentOutOfRangeException("Grade must be between 0 and 100.");
                }
                total += grade;
            }
            return total / Grades.Length;
        }
        public string GetLetterGrade()
        {
            double average = GetAverage();
            if (average >= 97) return "A+";
            else if (average >= 93) return "A";
            else if (average >= 90) return "A-";
            else if (average >= 87) return "B+";
            else if (average >= 83) return "B";
            else if (average >= 80) return "B-";
            else if (average >= 77) return "C+";
            else if (average >= 73) return "C";
            else if (average >= 70) return "C-";
            else if (average >= 67) return "D+";
            else if (average >= 63) return "D";
            else if (average >= 60) return "D-";
            else return "F";
        }
    }
}    
// add simple menu
// error handling for invalid inputs
// user input
// file handling
// object-oriented approach