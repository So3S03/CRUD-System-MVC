using Karim.CRUD.DAL.Entities;
using Karim.CRUD.DAL.Persistence.Data;
using Karim.CRUD.DAL.Persistence.Repository._GenaricRepository;
using Karim.CRUD.DAL.Persistence.Repository.DepartmentRepository;
using Karim.CRUD.DAL.Persistence.Repository.EmployeeRepository;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.DAL.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ConcurrentDictionary<string, object> _repository;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _repository = new ConcurrentDictionary<string, object>();
        }

		public IGenaricRepository<TEntity> GetRepository<TEntity>()
	        where TEntity : BaseModel
		{
			return (IGenaricRepository<TEntity>)_repository.GetOrAdd(typeof(TEntity).Name, new GenaricRepository<TEntity>(_dbContext));
		}

		public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
