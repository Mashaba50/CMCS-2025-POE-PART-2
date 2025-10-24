using CMCS.Services;
using Xunit;

namespace CMCS.Tests
{
    public class ValidationServiceTests
    {
        [Theory]
        [InlineData("test@example.com", true)]
        [InlineData("invalid-email", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("user@domain.co.uk", true)]
        public void ValidateEmail_ShouldReturnCorrectResult(string email, bool expectedIsValid)
        {
            // Act
            var result = ValidationService.ValidateEmail(email);

            // Assert
            Assert.Equal(expectedIsValid, result.IsValid);
        }

        [Theory]
        [InlineData("40", true)]
        [InlineData("0", false)]
        [InlineData("-10", false)]
        [InlineData("abc", false)]
        [InlineData("", false)]
        [InlineData("169", false)]
        [InlineData("168", true)]
        public void ValidateHoursWorked_ShouldReturnCorrectResult(string hours, bool expectedIsValid)
        {
            // Act
            var result = ValidationService.ValidateHoursWorked(hours);

            // Assert
            Assert.Equal(expectedIsValid, result.IsValid);
        }

        [Theory]
        [InlineData("250", true)]
        [InlineData("0", false)]
        [InlineData("-50", false)]
        [InlineData("abc", false)]
        [InlineData("", false)]
        [InlineData("1001", false)]
        [InlineData("1000", true)]
        public void ValidateHourlyRate_ShouldReturnCorrectResult(string rate, bool expectedIsValid)
        {
            // Act
            var result = ValidationService.ValidateHourlyRate(rate);

            // Assert
            Assert.Equal(expectedIsValid, result.IsValid);
        }

        [Theory]
        [InlineData("L001", true)]
        [InlineData("A123", true)]
        [InlineData("", false)]
        [InlineData("AB", false)]
        [InlineData("A1234567890", false)]
        [InlineData("123", false)]
        [InlineData("AB-C", false)]
        public void ValidateStaffNumber_ShouldReturnCorrectResult(string staffNumber, bool expectedIsValid)
        {
            // Act
            var result = ValidationService.ValidateStaffNumber(staffNumber);

            // Assert
            Assert.Equal(expectedIsValid, result.IsValid);
        }

        [Theory]
        [InlineData("user", "password", true)]
        [InlineData("", "password", false)]
        [InlineData("user", "", false)]
        [InlineData("ab", "password", false)]
        [InlineData("user", "abc", false)]
        public void ValidateLogin_ShouldReturnCorrectResult(string username, string password, bool expectedIsValid)
        {
            // Act
            var result = ValidationService.ValidateLogin(username, password);

            // Assert
            Assert.Equal(expectedIsValid, result.IsValid);
        }
    }
}