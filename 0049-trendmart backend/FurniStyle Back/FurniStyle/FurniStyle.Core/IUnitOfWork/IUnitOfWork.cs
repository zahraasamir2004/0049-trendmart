using FurniStyle.Core.Entities;
using FurniStyle.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.IUnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
        IBasketRepository Basket { get; set; }
        IGenericRepository<TEntity,TKey> Repository<TEntity,TKey>() where TEntity : BaseEntity<TKey>;  
    }
}
