using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace GradeCalcWithCS
{
    public partial class SearchStudentWindow : Window
    {
        private List<Student> students = new List<Student>();

        public SearchStudentWindow()
        {
            InitializeComponent();
            LoadStudents();
            if (students.Count == 0)
            {
                this.Close();
                return;
            }
        }

        private void LoadStudents()
        {
            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "students.json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    MessageBox.Show("Student file is empty. Please add a student first.");
                    students = new List<Student>();
                    return;
                }

                try
                {
                    students = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();

                    if (students.Count == 0)
                    {
                        MessageBox.Show("No students found. Please add a student first.");
                    }
                }
                catch (JsonException)
                {
                    MessageBox.Show("Student file is corrupted or invalid. Please fix or delete the file.");
                    students = new List<Student>();
                }
            }
            else
            {
                MessageBox.Show("Student file not found. Please add a student first.");
                students = new List<Student>();
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string name = SearchInput.Text.Trim();

            ResultPanel.Children.Clear();

            if (!System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
            {
                ResultPanel.Children.Add(new TextBlock
                {
                    Text = "Invalid name. Use letters only, no numbers or symbols.",
                    FontWeight = FontWeights.Bold,
                    Foreground = System.Windows.Media.Brushes.Red
                });
                return;
            }

            name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());

            var match = students.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (match == null)
            {
                ResultPanel.Children.Add(new TextBlock
                {
                    Text = "Student doesn't exist.",
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

            ResultPanel.Children.Add(new TextBlock
            {
                Text = $"Subjects:",
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 5, 0, 5)
            });

            foreach (var subject in match.Subjects)
            {
                ResultPanel.Children.Add(new TextBlock
                {
                    Text = $"→ {subject.Name}",
                    FontWeight = FontWeights.SemiBold
                });

                ResultPanel.Children.Add(new TextBlock { Text = $"    Mark: {subject.Mark}" });
                ResultPanel.Children.Add(new TextBlock { Text = $"    Percentage: {subject.GetPercentage():F2}%" });
                ResultPanel.Children.Add(new TextBlock { Text = $"    Letter Grade: {subject.GetLetterGrade()}" });
                ResultPanel.Children.Add(new TextBlock
                {
                    Text = $"    Credit Hours: {subject.CreditHours}",
                    Margin = new Thickness(0, 0, 0, 10)
                });
            }
        }
    }
}