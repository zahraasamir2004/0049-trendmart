using FurniStyle.Core.Entities;
using FurniStyle.Core.IRepositories;
using FurniStyle.Core.IUnitOfWork;
using FurniStyle.Repository.Data.Context;
using FurniStyle.Repository.Repositories;
using OnlineStore.Repository.Repositories;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FurniStyleDbContext _furniStyleDbContext;
        private Hashtable _repositories;
        public IBasketRepository Basket { get; set; }
        public UnitOfWork(FurniStyleDbContext furniStyleDbContext, IConnectionMultiplexer redix)
        {
            _furniStyleDbContext = furniStyleDbContext;
            _repositories = new Hashtable();
            Basket = new BasketRepository(redix);

        }
        public async Task<int> CompleteAsync()=> await _furniStyleDbContext.SaveChangesAsync();
        

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity, TKey>(_furniStyleDbContext);
                _repositories.Add(type, repository);
            }
            return _repositories[type] as IGenericRepository<TEntity, TKey>;
        }
    }
}
