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
                        new Subject { Name = "Math", Mark = 200, CreditHours = 2.5},
                        new Subject { Name = "Physics", Mark = 260, CreditHours = 3}
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
                                Console.WriteLine($"\nStudent: {student.Name}");
                                Console.WriteLine($"Gpa: {student.GetGPA()}");
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
                        // Formating the output with better spcaing and alignemnt
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
                        // name and subject duplication check
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
        public double CreditHours { get; set; }

        public string GetLetterGrade()
        {
            double percentage = GetPercentage();

            if (percentage >= 90) return "A+";
            else if (percentage >= 85) return "A";
            else if (percentage >= 80) return "B+";
            else if (percentage >= 70) return "B";
            else if (percentage >= 65) return "C+";
            else if (percentage >= 60) return "C";
            else if (percentage >= 55) return "D+";
            else if (percentage >= 50) return "D";
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
// file handling