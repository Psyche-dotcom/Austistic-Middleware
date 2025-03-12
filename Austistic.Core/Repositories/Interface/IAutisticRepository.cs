﻿namespace AlpaStock.Core.Repositories.Interface
{
    public interface IAutisticRepository<TEntity>
    {
        Task<TEntity?> GetByIdAsync(string id);
        IQueryable<TEntity> GetQueryable();
        Task<TEntity> Add(TEntity entity);
        void Delete(List<TEntity> entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<int> SaveChanges();
    }
}
