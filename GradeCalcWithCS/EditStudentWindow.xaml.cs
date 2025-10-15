using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace GradeCalcWithCS
{
    public partial class EditStudentWindow : Window
    {
        private List<Student> students = new List<Student>();
        private Student currentStudent;
        private bool changesSaved = false;

        private List<TextBox> markBoxes = new List<TextBox>();
        private List<TextBox> creditBoxes = new List<TextBox>();

        public EditStudentWindow()
        {
            InitializeComponent();
            this.Closing += EditStudentWindow_Closing;
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
            string name = SearchInput.Text.Trim();

            if (name.ToLower() == "cancel")
            {
                this.Close();
                return;
            }

            if (!Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Invalid name. Use letters only.");
                return;
            }

            name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());

            currentStudent = students.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (currentStudent == null)
            {
                MessageBox.Show($"Student '{name}' doesn't exist.");
                return;
            }

            DisplaySubjects();
        }

        private void DisplaySubjects()
        {
            EditPanel.Children.Clear();
            markBoxes.Clear();
            creditBoxes.Clear();

            for (int i = 0; i < currentStudent.Subjects.Count; i++)
            {
                var subject = currentStudent.Subjects[i];
                var group = new StackPanel { Margin = new Thickness(0, 10, 0, 10) };

                group.Children.Add(new TextBlock
                {
                    Text = $"{i + 1}. {subject.Name}",
                    FontWeight = FontWeights.Bold
                });

                group.Children.Add(new TextBlock { Text = "Mark:" });
                var markBox = new TextBox { Text = subject.Mark.ToString() };
                markBoxes.Add(markBox);
                group.Children.Add(markBox);

                group.Children.Add(new TextBlock { Text = "Credit Hours:" });
                var creditBox = new TextBox { Text = subject.CreditHours.ToString() };
                creditBoxes.Add(creditBox);
                group.Children.Add(creditBox);

                EditPanel.Children.Add(group);
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < currentStudent.Subjects.Count; i++)
            {
                string markText = markBoxes[i].Text.Trim();
                string creditText = creditBoxes[i].Text.Trim();

                if (!double.TryParse(creditText, out double credit) || credit <= 0 || credit > 4)
                {
                    MessageBox.Show($"Invalid credit hours for subject {currentStudent.Subjects[i].Name}. Must be between 1 and 4.");
                    return;
                }

                if (!double.TryParse(markText, out double mark) || mark < 0 || mark > credit * 100)
                {
                    MessageBox.Show($"Invalid mark for subject {currentStudent.Subjects[i].Name}. Must be between 0 and {credit * 100}.");
                    return;
                }

                currentStudent.Subjects[i].CreditHours = credit;
                currentStudent.Subjects[i].Mark = mark;
            }

            string filePath = "C:\\!\\Pr\\CS\\GradeCalcWithCS\\GradeCalcWithCS\\students.json";
            File.WriteAllText(filePath, JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true }));

            changesSaved = true;
            MessageBox.Show("Student updated successfully!");
            this.Close();
        }

        private void EditStudentWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!changesSaved && currentStudent != null)
            {
                var result = MessageBox.Show(
                    "You haven't saved your changes. Are you sure you want to exit?",
                    "Unsaved Changes",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}