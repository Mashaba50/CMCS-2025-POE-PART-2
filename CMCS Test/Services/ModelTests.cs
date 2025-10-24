using CMCS.Models;  // ← ADD THIS LINE
using Xunit;

namespace CMCS.Tests
{
    public class ModelTests
    {
        [Fact]
        public void Claim_TotalAmount_ShouldCalculateAutomatically()
        {
            // Arrange
            var claim = new Claim { HoursWorked = 40, HourlyRate = 250 };

            // Act & Assert
            Assert.Equal(10000, claim.TotalAmount);
        }

        [Fact]
        public void Claim_DefaultStatus_ShouldBeSubmitted()
        {
            // Arrange & Act
            var claim = new Claim();

            // Assert
            Assert.Equal(ClaimStatus.Submitted, claim.Status);
        }

        [Fact]
        public void User_DefaultValues_ShouldBeEmpty()
        {
            // Arrange & Act
            var user = new User();

            // Assert
            Assert.Equal(string.Empty, user.Username);
            Assert.Equal(string.Empty, user.Password);
            Assert.Equal(string.Empty, user.FullName);
            Assert.Equal(string.Empty, user.Email);
        }
    }
}