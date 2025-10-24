using CMCS.Models;
using CMCS.Services;
using System.Windows;
using System.Windows.Controls;

namespace CMCS.Views
{
    public partial class LoginPage : Page
    {
        private MainWindow _mainWindow;

        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            // Validate login
            var loginValidation = ValidationService.ValidateLogin(username, password);
            if (!loginValidation.IsValid)
            {
                ErrorTextBlock.Text = loginValidation.ErrorMessage;
                return;
            }

            // Get selected role
            UserRole role = UserRole.Lecturer;
            if (RoleComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string roleText = selectedItem.Content.ToString();
                if (roleText == "Coordinator")
                    role = UserRole.Coordinator;
                else if (roleText == "Academic Manager")
                    role = UserRole.AcademicManager;
            }

            // Create user
            var user = new User
            {
                Username = username,
                Password = password,
                Role = role,
                FullName = username
            };

            // Navigate to appropriate page
            switch (role)
            {
                case UserRole.Lecturer:
                    _mainWindow.mainFrame.Navigate(new LecturerPage(_mainWindow, user));
                    break;
                case UserRole.Coordinator:
                    _mainWindow.mainFrame.Navigate(new CoordinatorPage(_mainWindow, user));
                    break;
                case UserRole.AcademicManager:
                    _mainWindow.mainFrame.Navigate(new ManagerPage(_mainWindow, user));
                    break;
            }
        }
    }
}