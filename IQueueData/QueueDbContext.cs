using IQueueData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQueueData
{
    public class QueueDbContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }

        public DbSet<Queue> Queues { get; set; }

        public DbSet<Record> Records { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserGroup> UserGroups { get; set; }
        
        public DbSet<UserInQueue> UsersInQueues { get; set; }
        
        public DbSet<SwitchRequest> SwitchRecords { get; set; }

        public QueueDbContext(DbContextOptions<QueueDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Group>();

            modelBuilder.Entity<Record>();

            modelBuilder.Entity<Queue>();

            modelBuilder.Entity<User>();

            modelBuilder.Entity<UserGroup>();

            modelBuilder.Entity<UserInQueue>();

            modelBuilder.ApplyConfiguration(new SwitchRequestConfiguration());
        }
    }
    
    public class SwitchRequestConfiguration : IEntityTypeConfiguration<SwitchRequest>
    {
        public void Configure(EntityTypeBuilder<SwitchRequest> builder)
        {
            builder
                .HasOne(x => x.Record)
                .WithMany(x => x.SwitchRequests)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder
                .HasOne(x => x.SwitchWithRecord)
                .WithMany(x => x.MentionedInRequests)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
