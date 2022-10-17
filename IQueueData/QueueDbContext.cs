using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace IQueueData
{
    public class QueueDbContext : DbContext
    {

        public virtual DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
        
            base.OnModelCreating(modelBuilder);
        }

        public QueueDbContext(DbContextOptions<QueueDbContext> options) : base(options)
        {
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        // Data Source=SQL8001.site4now.net;Initial Catalog=db_a8e4e7_iqueue001db;User Id=db_a8e4e7_iqueue001db_admin;Password=Kf6anz@nCwZZvwR
        // Server=(localdb)\mssqllocaldb;Database=Queue001Db;Trusted_Connection=True;
        // optionsBuilder.UseSqlServer(@"");
        // }


    }
}
