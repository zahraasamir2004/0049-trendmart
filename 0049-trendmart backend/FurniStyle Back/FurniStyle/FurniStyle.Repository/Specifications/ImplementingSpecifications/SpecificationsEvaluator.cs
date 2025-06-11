using FurniStyle.Core.Entities;
using FurniStyle.Core.ISpecifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Repository.Specifications.ImplementingSpecifications
{
    public static class SpecificationsEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> CreateQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity, TKey> specifications)
        {
            var query = inputQuery;
            if (specifications.Criteria is not null) query = query.Where(specifications.Criteria);
            if (specifications.OrderBy is not null) query = query.OrderBy(specifications.OrderBy);
            if (specifications.OrderByDescending is not null) query = query.OrderByDescending(specifications.OrderByDescending);
            if (specifications.IsPaginationEnabled) query = query.Skip(specifications.Skip).Take(specifications.Take);
            query = specifications.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            return query;
        }
    }
}
