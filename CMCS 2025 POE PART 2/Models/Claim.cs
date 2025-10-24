using System;

namespace CMCS.Models
{
    /// <summary>
    /// Represents a claim submitted by a staff member for payment 
    /// based on the hours they have worked.
    /// </summary>
    public class Claim
    {
        /// <summary>
        /// Gets or sets the unique staff number associated with the employee submitting the claim.
        /// </summary>
        /// <remarks>
        /// This value is typically used to identify the staff member in the system.
        /// </remarks>
        public string StaffNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total number of hours the staff member worked.
        /// </summary>
        /// <remarks>
        /// This value is used along with <see cref="HourlyRate"/> to calculate the total claim amount.
        /// </remarks>
        public double HoursWorked { get; set; }

        /// <summary>
        /// Gets or sets the rate of payment per hour for the staff member.
        /// </summary>
        /// <remarks>
        /// The hourly rate represents how much the staff member earns for each hour worked.
        /// </remarks>
        public double HourlyRate { get; set; }

        /// <summary>
        /// Gets or sets the current status of the claim.
        /// </summary>
        /// <remarks>
        /// The status indicates the stage of the claim process (e.g., Pending, Approved, Rejected).
        /// </remarks>
        public ClaimStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the claim was submitted.
        /// </summary>
        /// <remarks>
        /// This is automatically set to the current system date and time upon creation.
        /// </remarks>
        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets the total amount of the claim, calculated as <c>HoursWorked * HourlyRate</c>.
        /// </summary>
        /// <remarks>
        /// This is a read-only property that dynamically calculates the total 
        /// whenever it is accessed. The result is expressed as a decimal for precision.
        /// </remarks>
        public decimal TotalAmount => (decimal)HoursWorked * (decimal)HourlyRate;
    }
}
