using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
        public UnitOfWork(QueueDbContext context,
            IGroupRepository groupRepository,
            IQueueRepository queueRepository,
            IRecordRepository recordRepository,
            IUserGroupRepository userGroupRepository,
            IUserRepository userRepository)
        {
            _context = context;
            GroupRepository = groupRepository;
            QueueRepository = queueRepository;
            RecordRepository = recordRepository;
            UserGroupRepository = userGroupRepository;
            UserRepository = userRepository;
        }
        
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
