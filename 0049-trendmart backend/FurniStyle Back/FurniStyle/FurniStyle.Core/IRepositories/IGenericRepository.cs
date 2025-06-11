using FurniStyle.Core.Entities;
using FurniStyle.Core.ISpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.IRepositories
{
    public interface IGenericRepository <TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specifications);
        Task<TEntity> GetAsync(ISpecification<TEntity, TKey> specifications);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
