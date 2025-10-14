using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace GradeCalcWithCS
{
    public partial class AddStudentWindow : Window
    {
        private int subjectCount = 0;

        public AddStudentWindow()
        {
            InitializeComponent();
        }

        private void GenerateFields_Click(object sender, RoutedEventArgs e)
        {
            SubjectFieldsPanel.Children.Clear();

            if (!int.TryParse(SubjectCountInput.Text.Trim(), out subjectCount) || subjectCount <= 0)
            {
                MessageBox.Show("Please enter a valid number of subjects.");
                return;
            }

            for (int i = 0; i < subjectCount; i++)
            {
                var group = new StackPanel { Margin = new Thickness(0, 10, 0, 10) };

                group.Children.Add(new TextBlock { Text = $"Subject {i + 1} Name:" });
                group.Children.Add(new TextBox { Name = $"SubName{i}" });

                group.Children.Add(new TextBlock { Text = "Mark:" });
                group.Children.Add(new TextBox { Name = $"SubMark{i}" });

                group.Children.Add(new TextBlock { Text = "Credit Hours:" });
                group.Children.Add(new TextBox { Name = $"SubCredit{i}" });

                SubjectFieldsPanel.Children.Add(group);
            }
        }

        private void SaveStudent_Click(object sender, RoutedEventArgs e)
        {
            string name = NameInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter a valid name.");
                return;
            }

            var subjects = new List<Subject>();

            for (int i = 0; i < subjectCount; i++)
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

                subjects.Add(new Subject { Name = subName, Mark = mark, CreditHours = credit });
            }

            var student = new Student { Name = name, Subjects = subjects };

            string filePath = "C:\\!\\Pr\\CS\\GradeCalcWithCS\\GradeCalcWithCS\\students.json";
            List<Student> students = File.Exists(filePath)
                ? JsonSerializer.Deserialize<List<Student>>(File.ReadAllText(filePath)) ?? new List<Student>()
                : new List<Student>();

            students.Add(student);
            File.WriteAllText(filePath, JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true }));

            MessageBox.Show("Student saved successfully!");
            this.Close();
        }
    }
}