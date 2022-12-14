
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
        /// List of user groups.
        /// </summary>
        public IList<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

        /// <summary>
        /// List of records of queue changes.
        /// </summary>
        public IList<QueueRecord> QueueRecords { get; set; } = new List<QueueRecord>(); 
    }
}
