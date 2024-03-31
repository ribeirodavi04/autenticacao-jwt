using AutenticacaoJWT.Domain.Interfaces;
using AutenticacaoJWT.Infra.Data.Context;
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


        public void Create(TEntity entity)
        {
            _entities.Add(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public TEntity GetById(int id)
        {
            return _entities.Find(id);
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _entities.Update(entity);
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
