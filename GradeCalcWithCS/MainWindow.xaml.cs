using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Shapes;



namespace GradeCalcWithCS
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "students.json");
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }
            InitializeComponent();
        }
        private void ViewAll_Click(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.HasStudents())
            {
                MessageBox.Show("No students found. Please add a student first.");
                return;
            }

            var window = new StudentListWindow();
            window.ShowDialog();
        }

        private void SearchStudent_Click(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.HasStudents())
            {
                MessageBox.Show("No students found. Please add a student first.");
                return;
            }

            var window = new SearchStudentWindow();
            window.ShowDialog();
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddStudentWindow();
            window.ShowDialog();
        }

        private void EditStudent_Click(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.HasStudents())
            {
                MessageBox.Show("No students found. Please add a student first.");
                return;
            }

            var window = new EditStudentWindow();
            window.ShowDialog();
        }

        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.HasStudents())
            {
                MessageBox.Show("No students found. Please add a student first.");
                return;
            }
            var window = new DeleteStudentWindow();
            window.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        public static bool HasStudents()
        {
            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "students.json");

            if (!File.Exists(filePath)) return false;

            string json = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(json)) return false;

            try
            {
                var students = JsonSerializer.Deserialize<List<Student>>(json);
                return students != null && students.Count > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}