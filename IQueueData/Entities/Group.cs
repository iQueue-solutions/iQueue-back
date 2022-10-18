
namespace IQueueData.Entities
{
    public class Group : BaseEntity
    {
        public string? Name { get; set; }

        public IList<UserGroup> UserGroups { get; set; } = new List<UserGroup>(); 
    }
}
