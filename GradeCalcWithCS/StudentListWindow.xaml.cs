using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace GradeCalcWithCS
{
    public partial class StudentListWindow : Window
    {
        private List<Student> students = new List<Student>();

        public StudentListWindow()
        {
            InitializeComponent();
            LoadStudents();
            DisplayStudents();
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

        private void DisplayStudents()
        {
            var sorted = SortOption.SelectedIndex == 1
                ? students.OrderBy(s => s.Name).ToList()
                : students.OrderByDescending(s => s.GetGPA()).ToList();

            var displayList = sorted.Select((s, i) => new
            {
                Index = i + 1,
                Name = s.Name,
                GPA = s.GetGPA().ToString("F2"),
                Percentage = s.GetTotalPercentage().ToString("F2") + "%"
            }).ToList();

            StudentListView.ItemsSource = displayList;
        }

        private void SortOption_Changed(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DisplayStudents();
        }
    }
}