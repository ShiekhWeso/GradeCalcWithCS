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
                        if (students.Count == 0)
                        {
                            Console.WriteLine("No students available. Press any key to return to the main menu.");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("=====================================");
                            Console.WriteLine("All Students:");
                            Console.WriteLine("=====================================");
                            foreach (var student in students)
                            {
                                Console.WriteLine($"→ {student.Name}    \nGPA: {student.GetGPA():F2}    \nTotal Percentage: {student.GetTotalPercentage():F2}%");
                                Console.WriteLine("-------------------------------------");
                            }
                        }
                        Console.WriteLine("Press any key to return to the menu.");
                        Console.ReadKey();
                        break;
                    // sort by gpa or name | include total number of students | students numbering (advanced features)
                    // Formating the output with better spcaing and alignemnt
                    case "2":
                        Console.Write("Enter your name: ");
                        string name = Console.ReadLine()?.Trim() ?? string.Empty;

                        bool found = false;
                        foreach (var student in students)
                        {
                            if (student.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine($"Student: {name}");
                                Console.WriteLine($"Gpa: {student.GetGPA()}");
                                Console.WriteLine($"Percentage: {student.GetTotalPercentage()}");
                                Console.WriteLine($"Subjects: ");
                                foreach (var subject in student.Subjects)
                                {
                                    Console.WriteLine($"→ {subject.Name}");
                                    Console.WriteLine($"    Mark: {subject.Mark}");
                                    Console.WriteLine($"    Percentage: {subject.GetPercentage():F2}%");
                                    Console.WriteLine($"    Letter Grade: {subject.GetLetterGrade()}");
                                    Console.WriteLine($"    Credit Hours: {subject.CreditHours}");
                                    Console.WriteLine("-------------------------------------");
                                }
                                found = true;
                                break;
                            }
                        }
                        if (found == false)
                        {
                        Console.WriteLine("Student doesn't exist. Press any key to return to the main menu.");
                        Console.ReadKey();
                        }    
                        // Formating the output with better spcaing and alignemnt
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
            return Subjects.Count > 0 ? Subjects.Average(s => s.GetPercentage()) : 0;
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
        public double Mark { get; set; }
        public int CreditHours { get; set; }

        public string GetLetterGrade()
        {
            if (Mark >= 90) return "A+";
            else if (Mark >= 85) return "A";
            else if (Mark >= 80) return "B+";
            else if (Mark >= 70) return "B";
            else if (Mark >= 65) return "C+";
            else if (Mark >= 60) return "C";
            else if (Mark >= 55) return "D+";
            else if (Mark >= 50) return "D";
            else return "F";

        }
        public double GetGPAvalue()
        {
            if (Mark >= 90) return 4.0;
            else if (Mark >= 85) return 3.7;
            else if (Mark >= 80) return 3.3;
            else if (Mark >= 70) return 3.0;
            else if (Mark >= 65) return 2.7;
            else if (Mark >= 60) return 2.0;
            else if (Mark >= 55) return 1.3;
            else if (Mark >= 50) return 1.0;
            else return 0.0;
        }
        public double GetPercentage()
        {
            return (Mark / (CreditHours * 100)) * 100;
        }
    }
}    
// user input
// file handling
// object-oriented approach