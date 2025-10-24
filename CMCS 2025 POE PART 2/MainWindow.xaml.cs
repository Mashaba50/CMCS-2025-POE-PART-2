using System.Windows;
using System.Windows.Controls;
using CMCS.Views;

namespace CMCS
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new LoginPage(this));
        }
    }
}