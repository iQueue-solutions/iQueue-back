namespace IQueueData.Entities
{
    /// <summary>
    /// This class represents UserGroup object.
    /// </summary>
    public class UserGroup : BaseEntity
    {
        /// <summary>
        /// Identificator of user in group.
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Entity of user in group.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Identificator of group.
        /// </summary>
        public Guid GroupId { get; set; }
        /// <summary>
        /// Entity of group.
        /// </summary>
        public Group? Group { get; set; }
    }
}
