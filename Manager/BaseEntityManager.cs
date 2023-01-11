using Microsoft.EntityFrameworkCore;
using PaylocityDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Manager
{
    public abstract class BaseEntityManager<TEntity> : IDisposable
        where TEntity : class
    {
        private IRepository<TEntity> repository;

        public BaseEntityManager(IRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public IRepository<TEntity> Repository
        {
            get
            {
                return repository;
            }
        }

        public virtual TEntity InitateNewDto()
        {
            return default(TEntity);
        }

        public void Dispose()
        {
            repository.Dispose();
        }
    }
}
