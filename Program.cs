using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using System.Text.Json; 

namespace GradeCalcWithCS
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string filePath = "students.json";

            List<Student> students = LoadStudents(filePath);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("GPA Calculator Menu:");
                Console.WriteLine("1. View all students:");
                Console.WriteLine("2. View your details:");
                Console.WriteLine("3. Add a new student");
                Console.WriteLine("4. Edit a student");
                Console.WriteLine("5. Delete a student");
                Console.WriteLine("6. Exit");
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

                            Console.WriteLine("\nSort students by:");
                            Console.WriteLine("1. GPA (highest first)");
                            Console.WriteLine("2. Name (A–Z)");

                            string sortChoice;
                            while (true)
                            {
                                Console.Write("Choose an option (1 or 2): ");
                                sortChoice = Console.ReadLine()?.Trim() ?? "";
                                if (sortChoice == "1" || sortChoice == "2") break;
                                Console.WriteLine("Invalid choice. Please enter 1 or 2.\n");
                            }

                            Console.WriteLine($"\n{"#",-3} {"Name",-25} {"GPA",-6} {"Percentage",-10}");
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
                        string name;
                        while (true)
                        {
                            Console.WriteLine("Enter your name (or type 'cancel' to leave): ");
                            name = Console.ReadLine()?.Trim() ?? "";

                            if (Regex.IsMatch(name, @"[a-zA-Z\s]+$"))
                            {
                                name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid name. Use letters only, no numbers or symbols.\n");
                            }
                        }

                        if (name.ToLower() == "cancel") break;

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
                        string studentName;
                        while (true)
                        {
                            Console.WriteLine("Please enter your full name: ");
                            studentName = Console.ReadLine()?.Trim() ?? "";

                            if (Regex.IsMatch(studentName, @"[a-zA-Z\s]+$"))
                            {
                                studentName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(studentName.ToLower());
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid name. Use letters only, no numbers or symbols.\n");
                            }
                        }
                        if (students.Any(s => s.Name.Equals(studentName, StringComparison.OrdinalIgnoreCase)))
                        {
                            Console.WriteLine("Student already exists. Please use a different name.\n");
                            Console.ReadKey();
                            break;
                        }

                        int subnums;
                        while (true)
                        {
                            Console.WriteLine("Enter number of subject: ");
                            if (int.TryParse(Console.ReadLine(), out subnums) && subnums > 0) break;
                            Console.WriteLine("Invalid input. Please enter a number greater than 0.\n");
                        }

                        List<Subject> subjects = new List<Subject>();

                        for (int i = 0; i < subnums; i++)
                        {
                            string subName;
                            while (true)
                            {
                                Console.WriteLine("\nEnter subject name:");
                                subName = Console.ReadLine()?.Trim() ?? string.Empty;
                                if (!Regex.IsMatch(subName, @"^[a-zA-Z0-9\s]+$"))
                                {
                                    Console.WriteLine("Invalid subject name. Use letters only.\n");
                                    i--;
                                    continue;
                                }
                                break;
                            }
                            subName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(subName.ToLower());
                            if (subjects.Any(s => s.Name.Equals(subName, StringComparison.OrdinalIgnoreCase)))
                            {
                                Console.WriteLine("Subject already added. Please enter a different subject.\n");
                                i--;
                                continue;
                            }

                            double credit;
                            while (true)
                            {
                                Console.WriteLine("Enter credit hours:");
                                if (double.TryParse(Console.ReadLine(), out credit) && credit > 0 && credit <= 4) break;
                                Console.WriteLine("Invalid input. Please enter a positive number between 1 and 4.\n");
                            }

                            double mark;
                            while (true)
                            {
                                Console.Write("Enter mark: ");
                                if (double.TryParse(Console.ReadLine(), out mark) && mark >= 0 && mark <= credit * 100) break;
                                Console.WriteLine($"Invalid input. Please enter a positive number between 0 and {credit * 100}.\n");
                            }

                            subjects.Add(new Subject { Name = subName, Mark = mark, CreditHours = credit });
                        }

                        Student newStudent = new Student { Name = studentName, Subjects = subjects };
                        students.Add(newStudent);
                        SaveStudents(students, filePath);

                        Console.WriteLine("\nStudent added successfully!");
                        Console.WriteLine("Press any key to return to the menu.");
                        Console.ReadKey();
                        break;
                    case "4":
                        while (true)
                        {
                            string editName;
                            while (true)
                            {
                                Console.WriteLine("Enter your name (or type 'cancel' to leave): ");
                                editName = Console.ReadLine()?.Trim() ?? "";

                                if (Regex.IsMatch(editName, @"[a-zA-Z\s]+$"))
                                {
                                    editName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(editName.ToLower());
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid name. Use letters only, no numbers or symbols.\n");
                                }
                            }

                            if (editName.ToLower() == "cancel") break;

                            var student = students.FirstOrDefault(s => s.Name.Equals(editName, StringComparison.OrdinalIgnoreCase));
                            if (student != null)
                            {
                                while (true)
                                {
                                    Console.WriteLine("-----------------------------------------------------");
                                    for (int i = 0; i < student.Subjects.Count; i++)
                                    {
                                        var subj = student.Subjects[i];
                                        Console.WriteLine($"{i + 1}. {subj.Name} - Mark: {subj.Mark}, Credit Hours: {subj.CreditHours}");
                                    }
                                    Console.WriteLine("-----------------------------------------------------");
                                    
                                    int subjectIndex;
                                    while (true)
                                    {
                                        Console.WriteLine("Enter the number of the subject to edit: ");
                                        if (int.TryParse(Console.ReadLine(), out subjectIndex) && subjectIndex >= 1 && subjectIndex <= student.Subjects.Count) break;
                                        Console.WriteLine("Invalid input. Please enter a valid number.\n");
                                    }
                            
                                    double newHours;
                                    while (true)
                                    {
                                        Console.WriteLine("Enter newHours hours:");
                                        if (double.TryParse(Console.ReadLine(), out newHours) && newHours > 0 && newHours <= 4) break;
                                        Console.WriteLine("Invalid input. Please enter a positive number between 1 and 4.\n");
                                    }

                                    double newMark;
                                    while (true)
                                    {
                                        Console.WriteLine($"Enter the new mark of subject '{student.Subjects[subjectIndex - 1].Name}': ");
                                        if (double.TryParse(Console.ReadLine(), out newMark) && newMark >= 0 && newMark <= newHours * 100) break;
                                        Console.WriteLine($"Invalid input. Please enter a positive number between 0 and {newHours * 100}.\n");
                                    }

                                    student.Subjects[subjectIndex - 1].Mark = newMark;
                                    student.Subjects[subjectIndex - 1].CreditHours = newHours;

                                    SaveStudents(students, filePath);
                                    Console.WriteLine("Student updated successfully.\n");
                                    Console.WriteLine($"{editName}'s GPA is now {student.GetGPA():F2}, and their percentage is {student.GetTotalPercentage():F2}%");
                                    Console.WriteLine("\nDo you want to edit another subject? (yes/no): ");
                                    string response = Console.ReadLine()?.Trim().ToLower() ?? "";
                                    if (response != "yes") break;
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Student '{editName}' doesn't exist. Try again.\n");
                            }
                        }
                        break;
                    case "5":
                        while (true)
                        {
                            string delName;
                            while (true)
                            {
                                Console.WriteLine("Enter the name to be deleted (or type 'cancel' to leave): ");
                                delName = Console.ReadLine()?.Trim() ?? "";

                                if (Regex.IsMatch(delName, @"[a-zA-Z\s]+$"))
                                {
                                    delName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(delName.ToLower());
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid name. Use letters only, no numbers or symbols.\n");
                                }
                            }

                            if (delName.ToLower() == "cancel") break;

                            var student = students.FirstOrDefault(s => s.Name.Equals(delName, StringComparison.OrdinalIgnoreCase));
                            if (student != null)
                            {
                                while (true)
                                {
                                    students.Remove(student);
                                    SaveStudents(students, filePath);
                                    Console.WriteLine($"Student '{delName}' has been deleted successfully.\n");
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Student '{delName}' doesn't exist. Try again.\n");
                            }
                        }
                        break;
                    case "6":
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
        static void SaveStudents(List<Student> students, string filePath)
        {
            string json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
        static List<Student> LoadStudents(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
            }
            return new List<Student>();
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
// GUI