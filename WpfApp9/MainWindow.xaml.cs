using System.Windows;
using System.Windows.Navigation;
using MultiStepForm.Pages;

namespace MultiStepForm
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new PersonalDataPage());
        }
    }
}
