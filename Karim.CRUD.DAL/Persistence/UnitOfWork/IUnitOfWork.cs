using Karim.CRUD.DAL.Entities;
using Karim.CRUD.DAL.Persistence.Repository._GenaricRepository;
using Karim.CRUD.DAL.Persistence.Repository.DepartmentRepository;
using Karim.CRUD.DAL.Persistence.Repository.EmployeeRepository;

namespace Karim.CRUD.DAL.Persistence.UnitOfWork
{
	public interface IUnitOfWork : IAsyncDisposable
    {
        public IGenaricRepository<TEntity> GetRepository<TEntity>()
            where TEntity : BaseModel;

        Task<int> CompleteAsync();
    }
}
