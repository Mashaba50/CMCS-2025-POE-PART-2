namespace CMCS.Models
{
    /// <summary>
    /// Represents a system user with login credentials and role information.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the username used for logging into the system.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password associated with the user account.
        /// </summary>
        /// <remarks>
        /// In a real application, passwords should be stored securely using hashing, not as plain text.
        /// </remarks>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the role assigned to the user (e.g., Manager, Lecturer, or Admin).
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
