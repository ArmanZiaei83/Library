using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Application
{
    public interface UnitOfWork
    {
        void Begin();
        void CommitPartial();
        void Commit();
        void Rollback();
        void Complete();
        Task CompleteAsync();
    }
}
