
namespace IQueueData.Entities
{
    public class User : BaseEntity
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public IList<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

        public IList<QueueRecord> QueueRecords { get; set; } = new List<QueueRecord>(); 
    }
}
