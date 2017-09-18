using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UruIT.GameOfDrones.Dal.Factory
{
    public interface IGenericRepository<Entity, EntityDTO> where Entity : class
                                                           where EntityDTO : class
    {
        IEnumerable<EntityDTO> GetAll(string includeProperties = "");
        IEnumerable<EntityDTO> FindBy(Expression<Func<Entity, bool>> predicate, string includeProperties = "");
        void Add(EntityDTO entity);
        void Delete(EntityDTO entity);
        void Edit(EntityDTO entity);
        Task Save();
    }
}