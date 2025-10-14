using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
            try
            {
                string filePath = "students.json";
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    students = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
                }
                else
                {
                    students = new List<Student>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message);
                students = new List<Student>();
            }
        }

        private void DisplayStudents()
        {
            if (SortOption == null)
            {
                MessageBox.Show("SortOption is not initialized.");
                return;
            }

            int sortIndex = SortOption.SelectedIndex;

            var sorted = sortIndex == 1
                ? students.OrderBy(s => s.Name).ToList()
                : students.OrderByDescending(s => SafeGPA(s)).ToList();

            var displayList = sorted.Select((s, i) => new
            {
                Index = i + 1,
                Name = s.Name,
                GPA = SafeGPA(s).ToString("F2"),
                Percentage = SafePercentage(s).ToString("F2") + "%"
            }).ToList();

            //StudentListView.ItemsSource = displayList;
        }

        private double SafeGPA(Student s)
        {
            return s?.Subjects?.Count > 0 ? s.GetGPA() : 0;
        }

        private double SafePercentage(Student s)
        {
            return s?.Subjects?.Count > 0 ? s.GetTotalPercentage() : 0;
        }

        private void SortOption_Changed(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DisplayStudents();
        }
    }
}