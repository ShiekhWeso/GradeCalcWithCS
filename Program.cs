using System;
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
                new Student { Name = "Manar", Grades = new double[] { 85, 90.9, 88}}
            };
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
                throw new InvalidOperationException("Grades are missing.");
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