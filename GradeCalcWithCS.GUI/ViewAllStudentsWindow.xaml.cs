using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using GradeCalcWithCS.Models;

namespace GradeCalcWithCS.GUI
{
    public partial class ViewAllStudentsWindow : Window
    {
        private const string FilePath = "students.json";

        public ViewAllStudentsWindow()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                var students = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
                StudentsGrid.ItemsSource = students;
            }
        }
    }
}