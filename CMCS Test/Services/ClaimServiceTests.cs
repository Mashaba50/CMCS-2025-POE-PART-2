using CMCS.Models;      // ← THIS IS CRITICAL
using CMCS.Services;    // ← THIS IS CRITICAL  
using System;
using Xunit;

namespace CMCS.Tests
{
    public class ClaimServiceTests
    {
        private readonly ClaimService _claimService;

        public ClaimServiceTests()
        {
            _claimService = new ClaimService();
        }

        [Fact]
        public void AddClaim_ValidClaim_ShouldAddToCollection()
        {
            // Arrange
            var claim = new Claim
            {
                StaffNumber = "L005",
                HoursWorked = 40,
                HourlyRate = 250
            };

            // Act
            _claimService.AddClaim(claim);

            // Assert
            Assert.True(_claimService.GetClaimsCount() > 0);
        }

        [Fact]
        public void CalculateTotalAmount_ShouldReturnCorrectValue()
        {
            // Arrange
            var claim = new Claim { HoursWorked = 40, HourlyRate = 250 };

            // Act
            decimal total = _claimService.CalculateTotalAmount(claim);

            // Assert
            Assert.Equal(10000, total);
        }
    }
}