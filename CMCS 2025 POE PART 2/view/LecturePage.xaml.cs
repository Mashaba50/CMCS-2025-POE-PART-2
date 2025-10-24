using CMCS.Models;
using CMCS.Services;
using System.Windows;
using System.Windows.Controls;

namespace CMCS.Views
{
    public partial class LecturerPage : Page
    {
        private MainWindow _mainWindow;
        private User _currentUser;
        private IClaimService _claimService;

        public LecturerPage(MainWindow mainWindow, User user)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _currentUser = user;
            _claimService = new ClaimService();

            // Set welcome message
            ResultTextBlock.Text = $"Welcome, {_currentUser.FullName}!";
            StaffNumberTextBox.Text = "L001"; // Default staff number
        }

        private void SubmitClaim_Click(object sender, RoutedEventArgs e)
        {
            string staffNumber = StaffNumberTextBox.Text.Trim();
            string hoursWorkedText = HoursWorkedTextBox.Text.Trim();
            string hourlyRateText = HourlyRateTextBox.Text.Trim();

            // Validate inputs
            var staffValidation = ValidationService.ValidateStaffNumber(staffNumber);
            if (!staffValidation.IsValid)
            {
                MessageBox.Show(staffValidation.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var hoursValidation = ValidationService.ValidateHoursWorked(hoursWorkedText);
            if (!hoursValidation.IsValid)
            {
                MessageBox.Show(hoursValidation.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var rateValidation = ValidationService.ValidateHourlyRate(hourlyRateText);
            if (!rateValidation.IsValid)
            {
                MessageBox.Show(rateValidation.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Parse values
            double hoursWorked = double.Parse(hoursWorkedText);
            decimal hourlyRate = decimal.Parse(hourlyRateText);

            // Create claim
            var claim = new Claim
            {
                StaffNumber = staffNumber,
                HoursWorked = hoursWorked,
                HourlyRate = (double)hourlyRate,
                Status = ClaimStatus.Submitted
            };

            try
            {
                _claimService.AddClaim(claim);
                decimal totalAmount = _claimService.CalculateTotalAmount(claim);

                ResultTextBlock.Text = $"Claim Submitted!\nTotal: R{totalAmount}";

                // Clear form
                HoursWorkedTextBox.Clear();
                HourlyRateTextBox.Clear();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.mainFrame.Navigate(new LoginPage(_mainWindow));
        }
    }
}