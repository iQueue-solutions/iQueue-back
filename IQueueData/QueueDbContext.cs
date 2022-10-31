using IQueueData.Entities;
using Microsoft.EntityFrameworkCore;

namespace IQueueData
{
    public class QueueDbContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }

        public DbSet<Queue> Queues { get; set; }

        public DbSet<Record> Records { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserGroup> UserGroups { get; set; }
        
        public DbSet<UserInQueue> UserQueueCollection { get; set; }

        public QueueDbContext(DbContextOptions<QueueDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Group>();

            modelBuilder.Entity<Record>();

            modelBuilder.Entity<Queue>();

            modelBuilder.Entity<User>();

            modelBuilder.Entity<UserGroup>();

            modelBuilder.Entity<UserInQueue>();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
