using IQueueData.Entities;
using Microsoft.EntityFrameworkCore;

namespace IQueueData
{
    public class QueueDbContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }

        public DbSet<Queue> Queues { get; set; }

        public DbSet<QueueRecord> QueueRecords { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserGroup> UserGroups { get; set; }

        public QueueDbContext(DbContextOptions<QueueDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Group>();

            modelBuilder.Entity<QueueRecord>();

            modelBuilder.Entity<Queue>();

            modelBuilder.Entity<User>();

            modelBuilder.Entity<UserGroup>();
        
            base.OnModelCreating(modelBuilder);
        }
    }
}
