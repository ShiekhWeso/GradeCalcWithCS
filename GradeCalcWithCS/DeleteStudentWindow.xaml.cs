using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;

namespace GradeCalcWithCS
{
    public partial class DeleteStudentWindow : Window
    {
        private List<Student> students = new List<Student>();
        private string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "students.json");

        public DeleteStudentWindow()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                students = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string delName = DeleteInput.Text.Trim();

            StatusMessage.Text = ""; // Clear previous message

            if (delName.ToLower() == "cancel")
            {
                this.Close();
                return;
            }

            if (!Regex.IsMatch(delName, @"^[a-zA-Z\s]+$"))
            {
                StatusMessage.Text = "Invalid name. Use letters only, no numbers or symbols.";
                StatusMessage.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            delName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(delName.ToLower());

            var student = students.FirstOrDefault(s => s.Name.Equals(delName, StringComparison.OrdinalIgnoreCase));
            if (student != null)
            {
                var confirm = MessageBox.Show(
                    $"Are you sure you want to delete '{delName}'?",
                    "Confirm Deletion",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (confirm == MessageBoxResult.Yes)
                {
                    students.Remove(student);
                    File.WriteAllText(filePath, JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true }));
                    StatusMessage.Text = $"Student '{delName}' has been deleted successfully.";
                    StatusMessage.Foreground = System.Windows.Media.Brushes.Green;
                    DeleteInput.Clear();
                }
            }
            else
            {
                StatusMessage.Text = $"Student '{delName}' doesn't exist.";
                StatusMessage.Foreground = System.Windows.Media.Brushes.Red;
            }
        }
    }
}