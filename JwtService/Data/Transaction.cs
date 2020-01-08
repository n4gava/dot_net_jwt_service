using JwtService.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace JwtService.Data
{
    public class Transaction : ITransaction, IDisposable
    {
        DbContext _dbContext;
        IDbContextTransaction _transaction;
        public Transaction(DbContext dbContext)
        {
            _dbContext = dbContext;
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}
