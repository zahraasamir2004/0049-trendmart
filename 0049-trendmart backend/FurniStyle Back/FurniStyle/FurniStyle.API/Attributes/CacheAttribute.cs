using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using FurniStyle.Core.IServices.Caching;

namespace FurniStyle.API.Attributes
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _expire;
        public CacheAttribute(int expire)
        {
            _expire = expire;

        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cachedService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheResponse = await cachedService.GetCacheKeyAsync(cacheKey);
            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentReselt = new ContentResult()
                {
                    Content = cacheResponse,
                    ContentType="application/json",
                    StatusCode=200
                };
                context.Result= contentReselt;
                return;
            }
            else
            {
                var executedContext = await next();
                if(executedContext.Result is OkObjectResult response)
                {
                  await cachedService.SetCacheKeyAsync(cacheKey, response.Value, TimeSpan.FromSeconds(_expire));
                }
            }
        }

        private string GenerateCacheKeyFromRequest( HttpRequest request)
        {
            var cacheKey = new StringBuilder();
            cacheKey.Append($"{request.Path}"); 
            foreach(var (key,value) in request.Query.OrderBy(x => x.Key))
            {
                cacheKey.Append($"|{key}-{value}");
                 
            }
            return cacheKey.ToString();
        }

    }
}
