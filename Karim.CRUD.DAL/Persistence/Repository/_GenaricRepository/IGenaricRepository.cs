using Karim.CRUD.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.DAL.Persistence.Repository._GenaricRepository
{
    public interface IGenaricRepository<T> where T : BaseModel
    {
        Task<IEnumerable<T>> GetAllAsync(bool AsNoTracking = true);
        Task<IEnumerable<T>> GetAllDeletedAsync(bool AsNoTracking = true);
        IQueryable<T> GetAllIQueryable(bool AsNoTracking = true);
        IQueryable<T> GetAllDeletedIQueryable(bool AsNoTracking = true);
        Task<T?> GetByIdAsync(int id);
        void Create(T entity);
        void Update(T entity);
        void SoftDelete(T entity);
        void HardDelete(T entity);
        void ResotoringSoftDeletedEntities(T entity);
    }
}
