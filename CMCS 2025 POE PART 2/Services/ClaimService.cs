using CMCS.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CMCS.Services
{
    /// <summary>
    /// Provides methods for managing staff claims including creation, verification, rejection, and retrieval.
    /// </summary>
    public class ClaimService : IClaimService
    {
        // A collection of all claims currently stored in memory.
        private ObservableCollection<Claim> _claims = new ObservableCollection<Claim>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimService"/> class and loads sample claim data.
        /// </summary>
        public ClaimService()
        {
            // Initialize with some sample data for demonstration or testing purposes.
            InitializeSampleData();
        }

        /// <summary>
        /// Populates the collection with example claim records.
        /// </summary>
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

        /// <summary>
        /// Adds a new claim to the system after validating input values.
        /// </summary>
        /// <param name="claim">The claim object to be added.</param>
        /// <exception cref="ArgumentNullException">Thrown when the claim object is null.</exception>
        /// <exception cref="ArgumentException">Thrown when a property such as StaffNumber, HoursWorked, or HourlyRate is invalid.</exception>
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

        /// <summary>
        /// Retrieves all claims that match a specific claim status.
        /// </summary>
        /// <param name="status">The claim status to filter by.</param>
        /// <returns>A collection of claims matching the given status.</returns>
        public ObservableCollection<Claim> GetClaimsByStatus(ClaimStatus status)
        {
            return new ObservableCollection<Claim>(_claims.Where(c => c.Status == status));
        }

        /// <summary>
        /// Verifies a claim based on the staff number and the role of the verifier.
        /// </summary>
        /// <param name="staffNumber">The staff number associated with the claim.</param>
        /// <param name="verifierRole">The role of the user verifying the claim.</param>
        /// <returns><c>true</c> if the claim status was successfully updated; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        /// Rejects a claim based on the staff number and the role of the user rejecting it.
        /// </summary>
        /// <param name="staffNumber">The staff number associated with the claim.</param>
        /// <param name="rejecterRole">The role of the user rejecting the claim.</param>
        /// <returns><c>true</c> if the claim status was successfully changed to a rejection state; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        /// Retrieves all claims currently stored in the system.
        /// </summary>
        public ObservableCollection<Claim> GetAllClaims() => _claims;

        /// <summary>
        /// Returns the total count of all claims.
        /// </summary>
        public int GetClaimsCount() => _claims.Count;

        /// <summary>
        /// Calculates the total payment amount for a given claim.
        /// </summary>
        /// <param name="claim">The claim for which to calculate the total amount.</param>
        /// <returns>The total amount as a decimal value.</returns>
        public decimal CalculateTotalAmount(Claim claim)
        {
            return (decimal)claim.HoursWorked * (decimal)claim.HourlyRate;
        }
    }
}
