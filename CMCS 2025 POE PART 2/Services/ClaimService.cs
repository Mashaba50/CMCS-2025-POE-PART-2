using CMCS.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CMCS.Services
{
    public class ClaimService : IClaimService
    {
        private ObservableCollection<Claim> _claims = new ObservableCollection<Claim>();

        public ClaimService()
        {
            // Initialize with sample data
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            _claims.Add(new Claim
            {
                StaffNumber = "L001",
                HoursWorked = 40,
                HourlyRate = 250,
                Status = ClaimStatus.Submitted
            });

            _claims.Add(new Claim
            {
                StaffNumber = "L002",
                HoursWorked = 35,
                HourlyRate = 260,
                Status = ClaimStatus.VerifiedByCoordinator
            });
        }

        public void AddClaim(Claim claim)
        {
            if (claim == null)
                throw new ArgumentNullException(nameof(claim));

            if (string.IsNullOrWhiteSpace(claim.StaffNumber))
                throw new ArgumentException("Staff number is required");

            if (claim.HoursWorked <= 0)
                throw new ArgumentException("Hours worked must be greater than 0");

            if (claim.HourlyRate <= 0)
                throw new ArgumentException("Hourly rate must be greater than 0");

            _claims.Add(claim);
        }

        public ObservableCollection<Claim> GetClaimsByStatus(ClaimStatus status)
        {
            return new ObservableCollection<Claim>(_claims.Where(c => c.Status == status));
        }

        public bool VerifyClaim(string staffNumber, UserRole verifierRole)
        {
            var claim = _claims.FirstOrDefault(c => c.StaffNumber == staffNumber);
            if (claim == null) return false;

            if (verifierRole == UserRole.Coordinator && claim.Status == ClaimStatus.Submitted)
            {
                claim.Status = ClaimStatus.VerifiedByCoordinator;
                return true;
            }
            else if (verifierRole == UserRole.AcademicManager && claim.Status == ClaimStatus.VerifiedByCoordinator)
            {
                claim.Status = ClaimStatus.ApprovedByManager;
                return true;
            }

            return false;
        }

        public bool RejectClaim(string staffNumber, UserRole rejecterRole)
        {
            var claim = _claims.FirstOrDefault(c => c.StaffNumber == staffNumber);
            if (claim == null) return false;

            if (rejecterRole == UserRole.Coordinator && claim.Status == ClaimStatus.Submitted)
            {
                claim.Status = ClaimStatus.RejectedByCoordinator;
                return true;
            }
            else if (rejecterRole == UserRole.AcademicManager && claim.Status == ClaimStatus.VerifiedByCoordinator)
            {
                claim.Status = ClaimStatus.RejectedByManager;
                return true;
            }

            return false;
        }

        public ObservableCollection<Claim> GetAllClaims() => _claims;
        public int GetClaimsCount() => _claims.Count;

        public decimal CalculateTotalAmount(Claim claim)
        {
            return (decimal)claim.HoursWorked * (decimal)claim.HourlyRate;
        }
    }
}