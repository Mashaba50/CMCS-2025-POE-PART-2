using System;
using System.Text.RegularExpressions;

namespace CMCS.Services
{
    public static class ValidationService
    {
        public static (bool IsValid, string ErrorMessage) ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return (false, "Email is required");

            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(email))
                return (false, "Invalid email format");

            return (true, string.Empty);
        }

        public static (bool IsValid, string ErrorMessage) ValidateHoursWorked(string hoursWorkedText)
        {
            if (string.IsNullOrWhiteSpace(hoursWorkedText))
                return (false, "Hours worked is required");

            if (!double.TryParse(hoursWorkedText, out double hoursWorked))
                return (false, "Hours worked must be a valid number");

            if (hoursWorked <= 0)
                return (false, "Hours worked must be greater than 0");

            if (hoursWorked > 168)
                return (false, "Hours worked cannot exceed 168");

            return (true, string.Empty);
        }

        public static (bool IsValid, string ErrorMessage) ValidateHourlyRate(string hourlyRateText)
        {
            if (string.IsNullOrWhiteSpace(hourlyRateText))
                return (false, "Hourly rate is required");

            if (!decimal.TryParse(hourlyRateText, out decimal hourlyRate))
                return (false, "Hourly rate must be a valid number");

            if (hourlyRate <= 0)
                return (false, "Hourly rate must be greater than 0");

            if (hourlyRate > 1000)
                return (false, "Hourly rate cannot exceed 1000");

            return (true, string.Empty);
        }

        public static (bool IsValid, string ErrorMessage) ValidateStaffNumber(string staffNumber)
        {
            if (string.IsNullOrWhiteSpace(staffNumber))
                return (false, "Staff number is required");

            if (staffNumber.Length < 3 || staffNumber.Length > 10)
                return (false, "Staff number must be between 3 and 10 characters");

            var staffNumberRegex = new Regex(@"^[A-Za-z]\d+$");
            if (!staffNumberRegex.IsMatch(staffNumber))
                return (false, "Staff number must start with a letter followed by numbers");

            return (true, string.Empty);
        }

        public static (bool IsValid, string ErrorMessage) ValidateLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                return (false, "Username is required");

            if (string.IsNullOrWhiteSpace(password))
                return (false, "Password is required");

            if (username.Length < 3)
                return (false, "Username must be at least 3 characters");

            if (password.Length < 4)
                return (false, "Password must be at least 4 characters");

            return (true, string.Empty);
        }
    }
}