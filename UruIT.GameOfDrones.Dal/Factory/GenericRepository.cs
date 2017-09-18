using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UruIT.GameOfDrones.Dal.Factory
{
    public abstract class GenericRepository<Context, Entity, EntityDTO> : IGenericRepository<Entity, EntityDTO>,
                                                IDisposable
                                                where Entity : class
                                                where EntityDTO : class
                                                where Context : DbContext, new()
    {
        #region Properties
        private readonly Context _entities;
        private DbContextTransaction Transaction { get; set; }

        public Context eContext { get; set; }
        #endregion

        #region Constructors 


        protected GenericRepository()
        {
            _entities = new Context();
            _entities.Configuration.LazyLoadingEnabled = false;
        }

        public GenericRepository<Context, Entity, EntityDTO> BeginTransaction()
        {
            Transaction = this.eContext.Database.BeginTransaction();
            return this;
        }

        #endregion

        #region Public Methods

        public void SetLazyLoading(bool bLazyLoading)
        {
            _entities.Configuration.LazyLoadingEnabled = bLazyLoading;
        }

        #endregion

        #region Public overridable Methods

        public virtual IEnumerable<EntityDTO> GetAll(string includeProperties = "")
        {
            IQueryable<Entity> query = _entities.Set<Entity>();

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return Mapper.Map<IEnumerable<EntityDTO>>(query);
        }

        public IEnumerable<EntityDTO> FindBy(Expression<Func<Entity, bool>> predicate, string includeProperties = "")
        {

            IQueryable<Entity> query = _entities.Set<Entity>().Where(predicate).AsNoTracking();

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return Mapper.Map<IEnumerable<EntityDTO>>(query);
        }

        public virtual void Add(EntityDTO entity)
        {
            Entity eMapped = Mapper.Map<Entity>(entity);
            _entities.Set<Entity>().Add(eMapped);
        }

        public virtual void Delete(EntityDTO entity)
        {
            Entity eMapped = Mapper.Map<Entity>(entity);
            _entities.Set<Entity>().Attach(eMapped);
            _entities.Set<Entity>().Remove(eMapped);
        }

        public virtual void Edit(EntityDTO entity)
        {
            Entity eMapped = Mapper.Map<Entity>(entity);
            _entities.Entry(eMapped).State = EntityState.Modified;
        }

        public async virtual Task Save()
        {
            await _entities.SaveChangesAsync();
        }

        public bool EndTransaction()
        {
            this.eContext.SaveChanges();
            Transaction.Commit();
            return true;
        }

        public void RollBack()
        {
            Transaction.Rollback();
        }

        #region IDisposable Support

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
          if(eContext != null)
            {
                eContext.Dispose();
            }
        }

        ~GenericRepository()
        {
            Dispose(false);
        }

        #endregion

        #endregion
    }
}
