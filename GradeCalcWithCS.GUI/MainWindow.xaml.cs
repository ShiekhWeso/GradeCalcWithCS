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
        private void CalculateGPA_Click(object sender, RoutedEventArgs e)
        {
            string name = StudentNameBox.Text;
            string gradeText = GradeBox.Text;

            if (double.TryParse(gradeText, out double grade))
            {
                double gpa = grade / 25;
                ResultText.Text = $"{name}'s GPA is {gpa:F2}";
            }
            else
            {
                ResultText.Text = "Please enter a valid numeric grade.";
            }
        }
    }
}
