using System;
using System.Collections.Generic;
namespace GradeCalcWithCS
{
    class Program
    {
        static void Main()
        {   
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            List<Student> students = new List<Student>
            {
                new Student
                {
                    Name = "Mohamed Wesam Mohamed",
                    Subjects = new List<Subject>
                    {
                        new Subject { Name = "discrete math", Mark = 245, CreditHours = 3},
                        new Subject { Name = "intro to programming", Mark = 180, CreditHours = 3},
                        new Subject { Name = "math 1", Mark = 208, CreditHours = 3},
                        new Subject { Name = "Analytic geometry", Mark = 158, CreditHours = 2.5},
                        new Subject { Name = "algebra", Mark = 202, CreditHours = 2.5},
                        new Subject { Name = "shit 1", Mark = 171, CreditHours = 2},
                        new Subject { Name = "shit 2", Mark = 150, CreditHours = 2},
                    }
                }
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
                            Console.WriteLine("\n===============================================");
                            Console.WriteLine($"Total Students: {students.Count}");
                            Console.WriteLine("Students Details: ");
                            Console.WriteLine("===============================================");

                            Console.WriteLine("Sort students by:");
                            Console.WriteLine("1. GPA (highest first)");
                            Console.WriteLine("2. Name (A–Z)");
                            Console.Write("Choose an option: ");
                            string sortChoice = Console.ReadLine()?.Trim() ?? "1";
                            
                            Console.WriteLine($"\n{"#", -3} {"Name",-25} {"GPA",-6} {"Percentage",-10}");
                            Console.WriteLine("-----------------------------------------------");


                            if (sortChoice == "1")
                            {
                                students = students.OrderByDescending(s => s.GetGPA()).ToList();
                            }
                            else if (sortChoice == "2")
                            {
                                students = students.OrderBy(s => s.Name).ToList();
                            }

                            int count = 1;

                            foreach (var student in students)
                            {
                                string gpaFormated = student.GetGPA().ToString("F2");
                                string percentageFormated = student.GetTotalPercentage().ToString("F2");

                                Console.WriteLine($"{count,-3} {student.Name,-25} {gpaFormated,-6} {percentageFormated}%");
                                count++;
                            }
                        }
                        Console.WriteLine("\nPress any key to return to the menu.");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Write("Enter your name: ");
                        string name = Console.ReadLine()?.Trim() ?? string.Empty;

                        bool found = false;
                        foreach (var student in students)
                        {
                            if (student.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine($"\nStudent: {student.Name}");
                                Console.WriteLine($"Gpa: {student.GetGPA():F2}");
                                Console.WriteLine($"Percentage: {student.GetTotalPercentage():F3}%");
                                Console.WriteLine($"Subjects: ");
                                foreach (var subject in student.Subjects)
                                {
                                    Console.WriteLine("-------------------------------------");
                                    Console.WriteLine($"→ {subject.Name}");
                                    Console.WriteLine($"    Mark: {subject.Mark}");
                                    Console.WriteLine($"    Percentage: {subject.GetPercentage():F2}%");
                                    Console.WriteLine($"    Letter Grade: {subject.GetLetterGrade()}");
                                    Console.WriteLine($"    Credit Hours: {subject.CreditHours}");
                                }
                                found = true;
                                Console.WriteLine("-------------------------------------");
                                Console.WriteLine("\nPress any key to continue.");
                                Console.ReadKey();
                                break;
                            }
                        }
                        if (found == false)
                        {
                        Console.WriteLine("Student doesn't exist. Press any key to return to the main menu.");
                        Console.ReadKey();
                        }    
                        break;
                    case "3":
                        Console.WriteLine("Please enter you full name: ");
                        string Name = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Please enter the number of subjects: ");
                        int subnums = int.Parse(Console.ReadLine() ?? "0");

                        List<Subject> subjects = new List<Subject>();

                        for (int i = 0; i < subnums; i++)
                        {
                            Console.WriteLine("\nEnter subject name:");
                            string subName = Console.ReadLine()?.Trim() ?? string.Empty;

                            Console.WriteLine("Enter mark:");
                            double mark = double.Parse(Console.ReadLine() ?? "0");

                            Console.WriteLine("Enter credit hours:");
                            double credit = double.Parse(Console.ReadLine() ?? "0");

                            subjects.Add(new Subject { Name = subName, Mark = mark, CreditHours = credit });

                        }

                        Student newStudent = new Student { Name = Name, Subjects = subjects };
                        students.Add(newStudent);

                        Console.WriteLine("\nStudent added successfully!");
                        Console.WriteLine("Press any key to return to the menu.");
                        Console.ReadKey();
                        break;
                        // handling the invalid inputs
                        // name and subject duplication check
                        // capilzlize subjects and student names auto
                    case "4":
                        Console.WriteLine("Thank you for using the GPA Calculator. Goodbye!");
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();
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

    class Subject
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
// file handling