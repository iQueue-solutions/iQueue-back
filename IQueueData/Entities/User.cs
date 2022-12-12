
namespace IQueueData.Entities
{
    /// <summary>
    /// This class represents User object.
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// First name of the user.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Last name of the user.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Email of the user.
        /// </summary>
        public string? Email { get; set; }
        
        /// <summary>
        /// Hashed password.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Random password salt.
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// List of user groups.
        /// </summary>
        public IList<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

        /// <summary>
        /// List of records of queue changes.
        /// </summary>
        public IList<UserInQueue> UserInQueues { get; set; } = new List<UserInQueue>(); 
    }
}
