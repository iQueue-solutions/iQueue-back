using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQueueData
{
    public interface IUnitOfWork
    {
        QueueDbContext Context { get; set; }
    }

    public class UnitOfWork : IUnitOfWork
    {

        public QueueDbContext Context { get; set; }


        public UnitOfWork(QueueDbContext context)
        {
            Context = context;
        }
    }
}
