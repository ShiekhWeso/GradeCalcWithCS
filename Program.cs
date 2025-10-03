using System;
namespace GradeCalcWithCS
{
    class Program
    {
        static void Main()
        {
            string[] students = { "Sophia", "Andrew", "Emma", "Logan" };
            double[][] grades = {
                new double[] { 90.5, 66, 98},
                new double[] { 88.9, 90, 92.5},
                new double[] { 100, 90, 95.6},
                new double[] { 70, 80, 77.9}
            };

            Console.WriteLine("Student        Grade\n");

            for (int i = 0; i < students.Length; i++)
            {
                string name = students[i];
                double average = CalculateAverage(grades[i]);
                string letterGrade = GetLetterGrade(average);

                Console.WriteLine($"{name,-15}{average,5:F2}     {letterGrade}");
            }
        }

        static double CalculateAverage(double[] grades)
        {
            double total = 0;
            foreach (double grade in grades)
            {
                total += grade;
            }
            return total / grades.Length;
        }

        static string GetLetterGrade(double average)
        {
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