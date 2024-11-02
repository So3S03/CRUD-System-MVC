using Karim.CRUD.DAL.Entities;
using Karim.CRUD.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.DAL.Persistence.Repository._GenaricRepository
{
    public class GenaricRepository<T>(ApplicationDbContext _dbContext) : IGenaricRepository<T> where T : BaseModel
    {
        #region GetAll
        public async Task<IEnumerable<T>> GetAllAsync(bool AsNoTracking = true)
        {
            if(AsNoTracking) return await _dbContext.Set<T>().Where(Ele => !Ele.IsDeleted).AsNoTracking().ToListAsync();
            return await _dbContext.Set<T>().Where(Ele => !Ele.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllDeletedAsync(bool AsNoTracking = true)
        {
            if (AsNoTracking) return await _dbContext.Set<T>().Where(Ele => Ele.IsDeleted).AsNoTracking().ToListAsync();

            return await _dbContext.Set<T>().Where(Ele => Ele.IsDeleted).ToListAsync();
        }

        public IQueryable<T> GetAllIQueryable(bool AsNoTracking = true)
        {
            if (AsNoTracking) return  _dbContext.Set<T>().Where(Ele => !Ele.IsDeleted).AsNoTracking();
            return _dbContext.Set<T>().Where(Ele => !Ele.IsDeleted);
        }

        public IQueryable<T> GetAllDeletedIQueryable(bool AsNoTracking = true)
        {
            if (AsNoTracking) return _dbContext.Set<T>().Where(Ele => Ele.IsDeleted).AsNoTracking();

            return _dbContext.Set<T>().Where(Ele => Ele.IsDeleted);
        }
        #endregion

        #region GetById
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        #endregion

        #region Create
        public void Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }
        #endregion

        #region Update
        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
        #endregion

        #region Delete
        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
        }

        public void ResotoringSoftDeletedEntities(T entity)
        {
            entity.IsDeleted = false;
            _dbContext.Set<T>().Update(entity);
        }

        public void HardDelete(T entity) 
        {
            _dbContext.Set<T>().Remove(entity);
        }
        #endregion

    }
}
