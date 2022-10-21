
namespace IQueueData.Entities
{
    /// <summary>
    /// This class represents Group object.
    /// </summary>
    public class Group : BaseEntity
    {
        /// <summary>
        /// Name of the group.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// List of user groups.
        /// </summary>
        public IList<UserGroup> UserGroups { get; set; } = new List<UserGroup>(); 
    }
}
