using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GradeCalcWithCS.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ViewAll_Click(object sender, RoutedEventArgs e)
        {
            var window = new ViewAllStudentsWindow();
            window.Show();
        }

        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            var window = new ViewDetailsWindow();
            window.Show();
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddStudentWindow();
            window.Show();
        }

        private void EditStudent_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditStudentWindow();
            window.Show();
        }

        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            var window = new DeleteStudentWindow();
            window.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
