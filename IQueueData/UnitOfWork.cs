using IQueueData.Interfaces;

namespace IQueueData
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QueueDbContext _context;
        
        public IGroupRepository GroupRepository { get; }
        public IQueueRepository QueueRepository { get; }
        public IRecordRepository RecordRepository { get; }
        public IUserGroupRepository UserGroupRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserInQueueRepository UserInQueueRepository { get; }
        
        public ISwitchRequestRepository SwitchRequestRepository { get; }

        public UnitOfWork(QueueDbContext context,
            IGroupRepository groupRepository,
            IQueueRepository queueRepository,
            IRecordRepository recordRepository,
            IUserGroupRepository userGroupRepository,
            IUserRepository userRepository, 
            IUserInQueueRepository userInQueueRepository, ISwitchRequestRepository switchRequestRepository)
        {
            _context = context;
            GroupRepository = groupRepository;
            QueueRepository = queueRepository;
            RecordRepository = recordRepository;
            UserGroupRepository = userGroupRepository;
            UserRepository = userRepository;
            UserInQueueRepository = userInQueueRepository;
            SwitchRequestRepository = switchRequestRepository;
        }
        
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
