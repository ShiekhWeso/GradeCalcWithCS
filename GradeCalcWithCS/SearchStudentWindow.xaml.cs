using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace GradeCalcWithCS
{
    public partial class SearchStudentWindow : Window
    {
        private List<Student> students = new List<Student>();

        public SearchStudentWindow()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            string filePath = "C:\\!\\Pr\\CS\\GradeCalcWithCS\\GradeCalcWithCS\\students.json";
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                students = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string name = SearchInput.Text.Trim().ToLower();
            var match = students.FirstOrDefault(s => s.Name.ToLower() == name);

            ResultPanel.Children.Clear();

            if (match == null)
            {
                ResultPanel.Children.Add(new TextBlock
                {
                    Text = "Student not found.",
                    FontWeight = FontWeights.Bold,
                    Foreground = System.Windows.Media.Brushes.Red
                });
                return;
            }

            ResultPanel.Children.Add(new TextBlock
            {
                Text = $"Name: {match.Name}",
                FontSize = 16,
                FontWeight = FontWeights.Bold
            });

            ResultPanel.Children.Add(new TextBlock
            {
                Text = $"GPA: {match.GetGPA():F2}",
                Margin = new Thickness(0, 5, 0, 0)
            });

            ResultPanel.Children.Add(new TextBlock
            {
                Text = $"Percentage: {match.GetTotalPercentage():F2}%",
                Margin = new Thickness(0, 2, 0, 10)
            });

            foreach (var subject in match.Subjects)
            {
                ResultPanel.Children.Add(new TextBlock
                {
                    Text = $"- {subject.Name}: {subject.Mark} marks, {subject.CreditHours} credit hours"
                });
            }
        }
    }
}