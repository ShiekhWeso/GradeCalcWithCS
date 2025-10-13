using System.Windows;

namespace GradeCalcWithCS
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ViewAll_Click(object sender, RoutedEventArgs e)
        {
            var window = new StudentListWindow();
            window.ShowDialog();
        }


        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddStudentWindow();
            window.ShowDialog();
        }

        private void SearchStudent_Click(object sender, RoutedEventArgs e)
        {
            var window = new SearchStudentWindow();
            window.ShowDialog();
        }

        private void EditStudent_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditStudentWindow();
            window.ShowDialog();
        }

        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            var window = new DeleteStudentWindow();
            window.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}