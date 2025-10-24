using CMCS.Models;
using CMCS.Services;
using System.Windows;
using System.Windows.Controls;

namespace CMCS.Views
{
    public partial class ManagerPage : Page
    {
        private MainWindow _mainWindow;
        private User _currentUser;
        private IClaimService _claimService;

        public ManagerPage(MainWindow mainWindow, User user)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _currentUser = user;
            _claimService = new ClaimService();

            LoadClaims();
        }

        private void LoadClaims()
        {
            var verifiedClaims = _claimService.GetClaimsByStatus(ClaimStatus.VerifiedByCoordinator);
            ClaimsListView.ItemsSource = verifiedClaims;
        }

        private void ApproveClaim_Click(object sender, RoutedEventArgs e)
        {
            if (ClaimsListView.SelectedItem is Claim selectedClaim)
            {
                bool success = _claimService.VerifyClaim(selectedClaim.StaffNumber, _currentUser.Role);
                if (success)
                {
                    MessageBox.Show($"Claim approved: {selectedClaim.StaffNumber}", "Success");
                    LoadClaims();
                }
            }
            else
            {
                MessageBox.Show("Please select a claim", "Warning");
            }
        }

        private void RejectClaim_Click(object sender, RoutedEventArgs e)
        {
            if (ClaimsListView.SelectedItem is Claim selectedClaim)
            {
                bool success = _claimService.RejectClaim(selectedClaim.StaffNumber, _currentUser.Role);
                if (success)
                {
                    MessageBox.Show($"Claim rejected: {selectedClaim.StaffNumber}", "Success");
                    LoadClaims();
                }
            }
            else
            {
                MessageBox.Show("Please select a claim", "Warning");
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.mainFrame.Navigate(new LoginPage(_mainWindow));
        }
    }
}