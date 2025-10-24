namespace CMCS.Models
{
    public class Claim
    {
        public string StaffNumber { get; set; } = string.Empty;
        public double HoursWorked { get; set; }
        public double HourlyRate { get; set; }
        public ClaimStatus Status { get; set; }
        public System.DateTime SubmissionDate { get; set; } = System.DateTime.Now;

        public decimal TotalAmount => (decimal)HoursWorked * (decimal)HourlyRate;
    }
}