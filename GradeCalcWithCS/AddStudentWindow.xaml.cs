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
    public partial class AddStudentWindow : Window
    {
        private int subjectCount = 0;
        private bool studentSaved = false;

        private List<TextBox> nameBoxes = new List<TextBox>();
        private List<TextBox> creditBoxes = new List<TextBox>();
        private List<TextBox> markBoxes = new List<TextBox>();

        public AddStudentWindow()
        {
            InitializeComponent();
            this.Closing += AddStudentWindow_Closing;
        }

        private void GenerateFields_Click(object sender, RoutedEventArgs e)
        {
            SubjectFieldsPanel.Children.Clear();
            nameBoxes.Clear();
            creditBoxes.Clear();
            markBoxes.Clear();

            if (!int.TryParse(SubjectCountInput.Text.Trim(), out subjectCount) || subjectCount <= 0)
            {
                MessageBox.Show("Please enter a valid number of subjects.");
                return;
            }

            for (int i = 0; i < subjectCount; i++)
            {
                var group = new StackPanel { Margin = new Thickness(0, 10, 0, 10) };

                group.Children.Add(new TextBlock { Text = $"Subject {i + 1} Name:" });
                var nameBox = new TextBox();
                nameBoxes.Add(nameBox);
                group.Children.Add(nameBox);

                group.Children.Add(new TextBlock { Text = "Credit Hours:" });
                var creditBox = new TextBox();
                creditBoxes.Add(creditBox);
                group.Children.Add(creditBox);

                group.Children.Add(new TextBlock { Text = "Mark:" });
                var markBox = new TextBox();
                markBoxes.Add(markBox);
                group.Children.Add(markBox);

                SubjectFieldsPanel.Children.Add(group);
            }
        }

        private void SaveStudent_Click(object sender, RoutedEventArgs e)
        {
            string name = NameInput.Text.Trim();

            if (!Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Invalid name. Use letters only, no numbers or symbols.");
                return;
            }

            name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());

            string filePath = "C:\\!\\Pr\\CS\\GradeCalcWithCS\\GradeCalcWithCS\\students.json";
            List<Student> students = File.Exists(filePath)
                ? JsonSerializer.Deserialize<List<Student>>(File.ReadAllText(filePath)) ?? new List<Student>()
                : new List<Student>();

            if (students.Any(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Student already exists. Please use a different name.");
                return;
            }

            var subjects = new List<Subject>();

            for (int i = 0; i < subjectCount; i++)
            {
                string subName = nameBoxes[i].Text.Trim();
                string creditText = creditBoxes[i].Text.Trim();
                string markText = markBoxes[i].Text.Trim();

                if (!Regex.IsMatch(subName, @"^[a-zA-Z0-9\s]+$"))
                {
                    MessageBox.Show($"Invalid subject name at position {i + 1}. Use letters and numbers only.");
                    return;
                }

                subName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(subName.ToLower());

                if (subjects.Any(s => s.Name.Equals(subName, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show($"Duplicate subject name: {subName}. Please enter unique subjects.");
                    return;
                }

                if (!double.TryParse(creditText, out double credit) || credit <= 0 || credit > 4)
                {
                    MessageBox.Show($"Invalid Credit Hours for subject {subName}. Must be between 1 and 4.");
                    return;
                }

                if (!double.TryParse(markText, out double mark) || mark < 0 || mark > credit * 100)
                {
                    MessageBox.Show($"Invalid Marks for subject {subName}. Must be between 0 and {credit * 100}.");
                    return;
                }

                subjects.Add(new Subject { Name = subName, CreditHours = credit, Mark = mark });
            }

            var student = new Student { Name = name, Subjects = subjects };
            students.Add(student);

            File.WriteAllText(filePath, JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true }));

            studentSaved = true;
            MessageBox.Show("Student saved successfully!");
            this.Close();
        }

        private void AddStudentWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!studentSaved)
            {
                var result = MessageBox.Show(
                    "You haven't saved the student yet. Are you sure you want to exit?",
                    "Unsaved Data",
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