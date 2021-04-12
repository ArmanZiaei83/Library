using Library.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence.EF
{
    public class EFUnitOfWork:UnitOfWork
    {
        private readonly EFDataContext _dataContext;

        public EFUnitOfWork(EFDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Begin()
        {
            _dataContext.Database.BeginTransaction();
        }

        public void CommitPartial()
        {
            _dataContext.SaveChanges();
        }

        public void Commit()
        {
            _dataContext.SaveChanges();
            _dataContext.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _dataContext.Database.RollbackTransaction();
        }

        public void Complete()
        {
            _dataContext.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
