using AutenticacaoJWT.Domain.Interfaces;
using AutenticacaoJWT.Domain.Pagination;
using AutenticacaoJWT.Infra.Data.Context;
using AutenticacaoJWT.Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoJWT.Infra.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public readonly ContextApp _context;
        private readonly DbSet<TEntity> _entities;

        public GenericRepository(ContextApp context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }


        public async Task<TEntity> Create(TEntity entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<PagedList<TEntity>> GetAllPagination(int pageNumber, int pageSize)
        {
            var query = _entities.AsQueryable();
            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<TEntity> Remove(TEntity entity)
        {
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
