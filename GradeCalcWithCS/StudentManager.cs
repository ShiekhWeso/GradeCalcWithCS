using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using GradeCalcWithCS;

namespace GradeCalcWithCS
{
    public static class StudentManager
    {
        public static List<Student> LoadStudents(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
            }
            return new List<Student>();
        }

        public static void SaveStudents(List<Student> students, string filePath)
        {
            string json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}