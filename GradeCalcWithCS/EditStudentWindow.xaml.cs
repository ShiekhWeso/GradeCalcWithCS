using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace GradeCalcWithCS
{
    public partial class EditStudentWindow : Window
    {
        private List<Student> students = new List<Student>();
        private Student currentStudent;
        private int currentIndex;

        public EditStudentWindow()
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

        private void LoadStudent_Click(object sender, RoutedEventArgs e)
        {
            string name = SearchInput.Text.Trim().ToLower();
            currentIndex = students.FindIndex(s => s.Name.ToLower() == name);

            EditPanel.Children.Clear();

            if (currentIndex == -1)
            {
                MessageBox.Show("Student not found.");
                return;
            }

            currentStudent = students[currentIndex];

            EditPanel.Children.Add(new TextBlock
            {
                Text = $"Editing: {currentStudent.Name}",
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 10)
            });

            for (int i = 0; i < currentStudent.Subjects.Count; i++)
            {
                var subject = currentStudent.Subjects[i];
                var group = new StackPanel { Margin = new Thickness(0, 10, 0, 10) };

                group.Children.Add(new TextBlock { Text = $"Subject {i + 1} Name:" });
                group.Children.Add(new TextBox { Name = $"SubName{i}", Text = subject.Name });

                group.Children.Add(new TextBlock { Text = "Mark:" });
                group.Children.Add(new TextBox { Name = $"SubMark{i}", Text = subject.Mark.ToString() });

                group.Children.Add(new TextBlock { Text = "Credit Hours:" });
                group.Children.Add(new TextBox { Name = $"SubCredit{i}", Text = subject.CreditHours.ToString() });

                EditPanel.Children.Add(group);
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (currentStudent == null) return;

            var updatedSubjects = new List<Subject>();

            for (int i = 0; i < currentStudent.Subjects.Count; i++)
            {
                string subName = ((TextBox)FindName($"SubName{i}"))?.Text.Trim() ?? "";
                string markText = ((TextBox)FindName($"SubMark{i}"))?.Text.Trim() ?? "";
                string creditText = ((TextBox)FindName($"SubCredit{i}"))?.Text.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(subName) ||
                    !double.TryParse(markText, out double mark) ||
                    !double.TryParse(creditText, out double credit))
                {
                    MessageBox.Show($"Invalid input for subject {i + 1}. Please check all fields.");
                    return;
                }

                updatedSubjects.Add(new Subject { Name = subName, Mark = mark, CreditHours = credit });
            }

            currentStudent.Subjects = updatedSubjects;
            students[currentIndex] = currentStudent;

            File.WriteAllText("students.json", JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true }));

            MessageBox.Show("Student updated successfully!");
            this.Close();
        }
    }
}