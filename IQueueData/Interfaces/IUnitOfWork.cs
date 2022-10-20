namespace IQueueData.Interfaces;

public interface IUnitOfWork
{
    public IGroupRepository GroupRepository { get; }
    
    public IQueueRepository QueueRepository { get; }
    
    public IRecordRepository RecordRepository { get; }
    
    public IUserGroupRepository UserGroupRepository { get; }
    
    public IUserRepository UserRepository { get; }
    
    Task SaveAsync();
}