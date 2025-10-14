using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace GradeCalcWithCS
{
    public partial class DeleteStudentWindow : Window
    {
        private List<Student> students = new List<Student>();

        public DeleteStudentWindow()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            string filePath = "students.json";
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                students = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string name = DeleteInput.Text.Trim().ToLower();
            int index = students.FindIndex(s => s.Name.ToLower() == name);

            if (index == -1)
            {
                StatusMessage.Text = "Student not found.";
                StatusMessage.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            students.RemoveAt(index);
            File.WriteAllText("students.json", JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true }));

            StatusMessage.Text = "Student deleted successfully.";
            StatusMessage.Foreground = System.Windows.Media.Brushes.Green;
        }
    }
}