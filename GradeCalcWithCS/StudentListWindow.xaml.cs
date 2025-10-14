using GradeCalcWithCS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
        private readonly ICollectionView _view;
        private ObservableCollection<Student> students = new ObservableCollection<Student>();

        public StudentListWindow()
        {
            InitializeComponent();
            LoadStudents();
            DisplayStudents();
            for (int i = 0; i < students.Count(); i++)
            {
                students[i] = new Student
                {
                    Name = students[i].Name,
                    GPA = SafeGPA(students[i]),
                    Percentage = SafePercentage(students[i]).ToString("F2") + "%"
                };
            }
            StudentListView.ItemsSource = students;

            _view = CollectionViewSource.GetDefaultView(StudentListView.ItemsSource);
            _view.SortDescriptions.Add(new SortDescription("GPA" , ListSortDirection.Descending));
        }

        private void LoadStudents()
        {
            string filePath = "C:\\!\\Pr\\CS\\GradeCalcWithCS\\GradeCalcWithCS\\students.json";
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                students = JsonSerializer.Deserialize<ObservableCollection<Student>>(json) ?? new ObservableCollection<Student>();
            }
        }

        private void DisplayStudents()
        {
            if (_view == null) return;

            if (SortOption == null)
            {
                MessageBox.Show("SortOption is not initialized.");
                return;
            }

            int sortIndex = SortOption.SelectedIndex;
            using (_view.DeferRefresh())
            {
                _view.SortDescriptions.Clear();
                _view.SortDescriptions.Add(new SortDescription(sortIndex == 1 ? "Name" : "GPA", sortIndex == 1 ? ListSortDirection.Ascending : ListSortDirection.Descending));
            }
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