using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FurniStyle.Core.IServices.Caching
{
    public interface ICacheService
    {
        Task SetCacheKeyAsync(string cacheKey,object response,TimeSpan expireTime);
        Task<string> GetCacheKeyAsync(string cacheKey);
    }
}
