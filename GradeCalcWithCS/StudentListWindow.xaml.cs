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
        private ICollectionView _view;
        private ObservableCollection<Student> students = new ObservableCollection<Student>();

        public StudentListWindow()
        {
            InitializeComponent();
            LoadStudents();
            if (students.Count == 0)
            {
                this.Close();
                return;
            }

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
            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "students.json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    MessageBox.Show("Student file is empty. Please add a student first.");
                    students = new ObservableCollection<Student>();
                    return;
                }

                try
                {
                    students = JsonSerializer.Deserialize<ObservableCollection<Student>>(json) ?? new ObservableCollection<Student>();

                    if (students.Count == 0)
                    {
                        MessageBox.Show("No students found. Please add a student first.");
                    }
                }
                catch (JsonException)
                {
                    MessageBox.Show("Student file is corrupted or invalid. Please fix or delete the file.");
                    students = new ObservableCollection<Student>();
                }
            }
            else
            {
                MessageBox.Show("Student file not found. Please add a student first.");
                students = new ObservableCollection<Student>();
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

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadStudents();

            for (int i = 0; i < students.Count; i++)
            {
                students[i].GPA = SafeGPA(students[i]);
                students[i].Percentage = SafePercentage(students[i]).ToString("F2") + "%";
            }

            StudentListView.ItemsSource = students;
            _view = CollectionViewSource.GetDefaultView(StudentListView.ItemsSource);
            DisplayStudents();
        }

        private void SortOption_Changed(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DisplayStudents();
        }
    }
}